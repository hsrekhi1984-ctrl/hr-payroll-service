using HRPayroll.Domain.Enums;

namespace HRPayroll.Application.DTOs;

public sealed record CreateEmployeeRequest(
    string EmployeeNumber,
    string FirstName,
    string LastName,
    decimal BaseSalary,
    decimal TaxRate,
    EmployeeRole Role);

public sealed record EmployeeResponse(
    Guid Id,
    string EmployeeNumber,
    string FirstName,
    string LastName,
    decimal BaseSalary,
    decimal TaxRate,
    EmployeeRole Role,
    bool IsActive);
