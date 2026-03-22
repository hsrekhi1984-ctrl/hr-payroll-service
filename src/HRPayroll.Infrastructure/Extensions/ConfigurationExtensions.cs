using Microsoft.Extensions.Configuration;

namespace HRPayroll.Infrastructure.Extensions;

public static class ConfigurationExtensions
{
    public static string GetPayrollConnectionString(this IConfiguration configuration)
    {
        var connectionString =
            configuration.GetConnectionString("PayrollDb") ??
            configuration.GetConnectionString("PostgreSql");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "Database connection string is not configured. Set ConnectionStrings:PayrollDb or ConnectionStrings:PostgreSql.");
        }

        return connectionString;
    }
}
