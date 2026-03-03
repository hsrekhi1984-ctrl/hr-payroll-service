using HRPayroll.Application.Abstractions;

namespace HRPayroll.Infrastructure.Persistence;

public sealed class UnitOfWork(PayrollDbContext dbContext) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => dbContext.SaveChangesAsync(cancellationToken);
}
