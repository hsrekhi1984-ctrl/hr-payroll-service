using HRPayroll.Application.Abstractions;
using HRPayroll.Application.DTOs;
using HRPayroll.Application.Interfaces;
using HRPayroll.Domain.Entities;

namespace HRPayroll.Application.Services;

public sealed class PayrollService(
    IEmployeeRepository employeeRepository,
    IPayrollRepository payrollRepository,
    IPayrollCalculator payrollCalculator,
    IUnitOfWork unitOfWork)
{
    public async Task<PayrollResponse> CreateAsync(CreatePayrollRequest request, CancellationToken cancellationToken = default)
    {
        var employee = await employeeRepository.GetByIdAsync(request.EmployeeId, cancellationToken)
                       ?? throw new InvalidOperationException("Employee does not exist.");

        var (grossPay, taxAmount, netPay) = payrollCalculator.Calculate(employee.BaseSalary, employee.TaxRate);

        var payrollRecord = new PayrollRecord
        {
            EmployeeId = employee.Id,
            PeriodStart = request.PeriodStart,
            PeriodEnd = request.PeriodEnd,
            GrossPay = grossPay,
            TaxAmount = taxAmount,
            NetPay = netPay
        };

        await payrollRepository.AddAsync(payrollRecord, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new PayrollResponse(payrollRecord.Id, payrollRecord.EmployeeId, payrollRecord.PeriodStart, payrollRecord.PeriodEnd, payrollRecord.GrossPay, payrollRecord.TaxAmount, payrollRecord.NetPay, payrollRecord.Currency);
    }

    public async Task<IReadOnlyCollection<PayrollResponse>> GetByEmployeeAsync(Guid employeeId, CancellationToken cancellationToken = default)
    {
        var records = await payrollRepository.GetByEmployeeIdAsync(employeeId, cancellationToken);
        return records
            .Select(p => new PayrollResponse(p.Id, p.EmployeeId, p.PeriodStart, p.PeriodEnd, p.GrossPay, p.TaxAmount, p.NetPay, p.Currency))
            .ToArray();
    }
}
