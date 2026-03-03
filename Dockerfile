FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY HRPayroll.sln ./
COPY src/HRPayroll.Domain/HRPayroll.Domain.csproj src/HRPayroll.Domain/
COPY src/HRPayroll.Application/HRPayroll.Application.csproj src/HRPayroll.Application/
COPY src/HRPayroll.Infrastructure/HRPayroll.Infrastructure.csproj src/HRPayroll.Infrastructure/
COPY src/HRPayroll.Api/HRPayroll.Api.csproj src/HRPayroll.Api/
COPY tests/HRPayroll.UnitTests/HRPayroll.UnitTests.csproj tests/HRPayroll.UnitTests/
RUN dotnet restore HRPayroll.sln

COPY . .
RUN dotnet publish src/HRPayroll.Api/HRPayroll.Api.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "HRPayroll.Api.dll"]
