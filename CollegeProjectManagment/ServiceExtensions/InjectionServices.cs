using CollegeProjectManagment.Core.Interfaces;
using CollegeProjectManagment.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;
using System.Text;

namespace CollegeProjectManagment.DI;

public static class InjectionServices
{
    public static void ConfigureServices(this IServiceCollection service)
    {
        // Services to inject into Program.cs
        service.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
    }

    public static void ConfigureAuthentication(this IServiceCollection service, WebApplicationBuilder builder)
    {
        var config = builder.Configuration;

        service.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = config["JwtSettings:Issuer"],
                ValidAudience = config["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"]!)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true
            };
        });

        service.AddAuthorization();
    }
}