using Azure.Core;
using Azure.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Npgsql;
using System.Data.Common;

namespace HRPayroll.Infrastructure.Persistence.Authentication;

public sealed class AzurePostgresPasswordInterceptor : DbConnectionInterceptor
{
    private static readonly TokenRequestContext TokenRequestContext =
        new(["https://ossrdbms-aad.database.windows.net/.default"]);

    private readonly DefaultAzureCredential credential = new();

    public override InterceptionResult ConnectionOpening(
        DbConnection connection,
        ConnectionEventData eventData,
        InterceptionResult result)
    {
        SetPassword(connection);
        return result;
    }

    public override async ValueTask<InterceptionResult> ConnectionOpeningAsync(
        DbConnection connection,
        ConnectionEventData eventData,
        InterceptionResult result,
        CancellationToken cancellationToken = default)
    {
        await SetPasswordAsync(connection, cancellationToken);
        return result;
    }

    public async Task SetPasswordAsync(DbConnection connection, CancellationToken cancellationToken = default)
    {
        if (connection is not NpgsqlConnection npgsqlConnection)
        {
            return;
        }

        var token = await credential.GetTokenAsync(TokenRequestContext, cancellationToken);
        npgsqlConnection.Password = token.Token;
    }

    public void SetPassword(DbConnection connection)
    {
        if (connection is not NpgsqlConnection npgsqlConnection)
        {
            return;
        }

        var token = credential.GetToken(TokenRequestContext, CancellationToken.None);
        npgsqlConnection.Password = token.Token;
    }
}
