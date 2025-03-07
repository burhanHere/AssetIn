using System.Security.Claims;
using System.Text;
using AssetIn.Server.Data;
using AssetIn.Server.Models;
using AssetIn.Server.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Add DbContext
var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(
   ConnectionString, new MySqlServerVersion(new Version(8, 0, 2))
));

// Add Identity framework
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequiredLength = 11;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = true;
}).AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Add JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]!)),
        RoleClaimType = ClaimTypes.Role
    };
    // Log JWT claims to confirm role is being recognized
    options.Events = new JwtBearerEvents
    {
        OnTokenValidated = context =>
        {
            var claims = context.Principal?.Claims;
            Console.WriteLine("✅ JWT Claims:");
            foreach (var claim in claims)
            {
                Console.WriteLine($"{claim.Type}: {claim.Value}");
            }
            return Task.CompletedTask;
        },
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine("❌ JWT Authentication Failed: " + context.Exception.Message);
            return Task.CompletedTask;
        }
    };
});

// Add Role Based Policies for Authorization 
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("OrganizationOwnerPolicy", policy => policy.RequireClaim("Role", ["OrganizationOwner"]));
    options.AddPolicy("OrganizationOwnerOrganizationAssetManagerPolicy", policy => policy.RequireClaim("Role", ["OrganizationOwner", "OrganizationAssetManager"]));
    options.AddPolicy("OrganizationOwnerOrganizationAssetManagerOrganizationEmployeePolicy", policy => policy.RequireClaim("Role", ["OrganizationOwner", "OrganizationAssetManager", "OrganizationEmployee"]));
    options.AddPolicy("VendorPolicy", policy => policy.RequireClaim("Role", ["Vendor"]));
});

// Add Cors to accep requests from only our webpages  
// var allowedOrigen = builder.Configuration["AllowedOrigen"]!;
// builder.Services.AddCors(options =>
//        {
//            options.AddPolicy("AllowSepcificOrigin", builder => builder.WithOrigins(allowedOrigen).AllowAnyHeader().AllowAnyMethod());
//        });
// Add Cors to accep requests from any where  
builder.Services.AddCors(options =>
       {
           options.AddPolicy("AllowSepcificOrigin", builder => builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod());
       });


// Add Email Service
builder.Services.AddTransient<EmailService>();

// Add Iconfigurations
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
     {
         option.SwaggerDoc("v1", new OpenApiInfo { Title = "User Management API", Version = "v1" });
         option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
         {
             In = ParameterLocation.Header,
             Description = "Please enter a valid token...",
             Name = "Authorization", // Corrected Name
             Type = SecuritySchemeType.Http,
             BearerFormat = "JWT",
             Scheme = "Bearer"
         });
         option.AddSecurityRequirement(new OpenApiSecurityRequirement
         {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer" // Corrected ID closing
                    }
                },
                new string[] { }
            }
        });
     });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowSepcificOrigin");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
