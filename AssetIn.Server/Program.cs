using System.Text;
using AssetIn.Server.Data;
using AssetIn.Server.Models;
using AssetIn.Server.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
    options.Tokens.EmailConfirmationTokenProvider = Convert.ToString(TimeSpan.FromHours(24))!;
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
    };
});

// Add Role Based Policies for Authorization 
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("OrganizationOwnerPolicy", policy => policy.RequireRole("OrganizationOwner"));
    options.AddPolicy("OrganizationOwnerOrganizationAssetManagerPolicy", policy => policy.RequireRole("OrganizationOwner", "OrganizationAssetManager"));
    options.AddPolicy("OrganizationOwnerOrganizationAssetManagerOrganizationEmployeePolicy", policy => policy.RequireRole("OrganizationOwner", "OrganizationAssetManager", "OrganizationEmployee"));
    options.AddPolicy("VendorPolicy", policy => policy.RequireRole("Vendor"));
});

// Add Cors to accep requests  
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
builder.Services.AddSwaggerGen();

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
