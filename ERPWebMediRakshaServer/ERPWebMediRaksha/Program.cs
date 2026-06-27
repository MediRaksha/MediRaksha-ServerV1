using ERP.RepositoriesCore.Customer;
using ERP.RepositoriesCore.Interfaces.Customers;
using ERP.RepositoriesCore.Interfaces.Login;
using ERP.RepositoriesCore.Services.Login;
using ERP.ServicesCore.Interfaces.Customers;
using ERP.ServicesCore.Interfaces.Login;
using ERP.ServicesCore.Services.Customer;
using ERP.ServicesCore.Services.Login;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ERP.DataCore.Interfaces;
using ERP.DataCore.Services;


var builder = WebApplication.CreateBuilder(args);

// Customer Services
builder.Services.AddScoped<IDbManager, DbManager>();
builder.Services.AddScoped<IMedicineRepository, MedicineRepository>();
builder.Services.AddScoped<IMedicineService, MedicineService>();

// Login Services
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<JwtTokenGenerator>();

builder.Services.AddAuthentication(
    JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer =
                    builder.Configuration["Jwt:Issuer"],

                ValidAudience =
                    builder.Configuration["Jwt:Audience"],

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            builder.Configuration["Jwt:Key"]))
            };
    });

builder.Services.AddAuthorization();
// Add services
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();