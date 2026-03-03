using System.Text;
using HRPayroll.Application.Abstractions;
using HRPayroll.Infrastructure.Persistence;
using HRPayroll.Infrastructure.Repositories;
using HRPayroll.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace HRPayroll.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PayrollDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("PayrollDb")));

        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
        var jwtOptions = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>() ?? new JwtOptions();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey))
                };
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("CanViewPayroll", policy => policy.RequireRole("HRAdmin", "PayrollAdmin", "Manager"));
            options.AddPolicy("CanManageEmployees", policy => policy.RequireRole("HRAdmin"));
            options.AddPolicy("CanProcessPayroll", policy => policy.RequireRole("PayrollAdmin"));
        });

        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IPayrollRepository, PayrollRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
