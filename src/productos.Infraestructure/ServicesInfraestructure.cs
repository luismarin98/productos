using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using productos.Application.Contracts;
using productos.Infraestructure.Context;
using productos.Infraestructure.Repository;

namespace productos.Infraestructure;

public static class ServicesInfraestructure
{
    public static IServiceCollection AddServicesInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUsuarioContract, UserRepository>();
        services.AddScoped<IProductosContract, PorductoRepository>();

        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opt =>
        {
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
            };
        });

        services.AddDbContext<DB>(opts => opts.UseSqlServer(configuration.GetConnectionString("pruebas")));

        return services;
    }
}
