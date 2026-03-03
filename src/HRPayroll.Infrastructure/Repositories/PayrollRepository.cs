using HRPayroll.Application.Abstractions;
using HRPayroll.Domain.Entities;
using HRPayroll.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HRPayroll.Infrastructure.Repositories;

public sealed class PayrollRepository(PayrollDbContext dbContext) : IPayrollRepository
{
    public async Task<PayrollRecord?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await dbContext.PayrollRecords.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

    public async Task<IReadOnlyCollection<PayrollRecord>> GetByEmployeeIdAsync(Guid employeeId, CancellationToken cancellationToken = default) =>
        await dbContext.PayrollRecords
            .AsNoTracking()
            .Where(p => p.EmployeeId == employeeId)
            .OrderByDescending(p => p.PeriodEnd)
            .ToListAsync(cancellationToken);

    public async Task AddAsync(PayrollRecord record, CancellationToken cancellationToken = default) =>
        await dbContext.PayrollRecords.AddAsync(record, cancellationToken);
}
