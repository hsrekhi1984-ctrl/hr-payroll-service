using HRPayroll.Application.Interfaces;
using HRPayroll.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HRPayroll.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<EmployeeService>();
        services.AddScoped<PayrollService>();
        services.AddSingleton<IPayrollCalculator, PayrollCalculator>();
        return services;
    }
}
