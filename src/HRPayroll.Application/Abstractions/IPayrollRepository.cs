using HRPayroll.Domain.Entities;

namespace HRPayroll.Application.Abstractions;

public interface IPayrollRepository
{
    Task<PayrollRecord?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<PayrollRecord>> GetByEmployeeIdAsync(Guid employeeId, CancellationToken cancellationToken = default);
    Task AddAsync(PayrollRecord record, CancellationToken cancellationToken = default);
}
