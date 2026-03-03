# HR Payroll Service (.NET 8)

Production-ready starter for an HR Payroll Web API using Clean Architecture principles.

## Implemented
- .NET 8 Web API
- Clean architecture (`Domain`, `Application`, `Infrastructure`, `Api`)
- PostgreSQL via EF Core + Npgsql
- OpenTelemetry tracing/metrics (OTLP exporter)
- Health checks (`/health/live`, `/health/ready`)
- Serilog structured logging
- RBAC support with JWT + policy-based authorization
- Multi-stage Dockerfile
- Unit tests (xUnit + FluentAssertions)

## Solution structure
- `src/HRPayroll.Domain`: Entities and core types
- `src/HRPayroll.Application`: Use-case services, DTOs, interfaces
- `src/HRPayroll.Infrastructure`: EF Core persistence, repositories, auth wiring
- `src/HRPayroll.Api`: API host, controllers, telemetry, health checks
- `tests/HRPayroll.UnitTests`: Unit tests

## Running locally
```bash
dotnet restore HRPayroll.sln
dotnet test HRPayroll.sln
dotnet run --project src/HRPayroll.Api/HRPayroll.Api.csproj
```

## Running with Docker
```bash
docker build -t hr-payroll-api .
docker run --rm -p 8080:8080 hr-payroll-api
```

## Authentication / RBAC
JWT bearer auth is configured with role-based policies:
- `CanManageEmployees`: `HRAdmin`
- `CanProcessPayroll`: `PayrollAdmin`
- `CanViewPayroll`: `HRAdmin`, `PayrollAdmin`, `Manager`

Set production-safe values for `Jwt:SigningKey`, database credentials, and OTLP endpoint through environment variables or secret stores.
