namespace HRPayroll.Application.DTOs;

public sealed record CreatePayrollRequest(Guid EmployeeId, DateOnly PeriodStart, DateOnly PeriodEnd);

public sealed record PayrollResponse(
    Guid Id,
    Guid EmployeeId,
    DateOnly PeriodStart,
    DateOnly PeriodEnd,
    decimal GrossPay,
    decimal TaxAmount,
    decimal NetPay,
    string Currency);
