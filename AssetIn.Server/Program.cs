using System.Text;
using AssetIn.Server.Data;
using AssetIn.Server.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine(ConnectionString);
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(
   ConnectionString, new MySqlServerVersion(new Version(8, 0, 2))
));
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

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("OrganizationOwnerPolicy", policy => policy.RequireRole("OrganizationOwner"));
    options.AddPolicy("OrganizationOwnerOrganizationAssetManagerPolicy", policy => policy.RequireRole("OrganizationOwner", "OrganizationAssetManager"));
    options.AddPolicy("OrganizationOwnerOrganizationAssetManagerOrganizationEmployeePolicy", policy => policy.RequireRole("OrganizationOwner", "OrganizationAssetManager", "OrganizationEmployee"));
    options.AddPolicy("VendorPolicy", policy => policy.RequireRole("Vendor"));
});
builder.Services.AddCors(options =>
       {
           options.AddPolicy("AllowSepcificOrigin", builder => builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod());
       });
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
