using HRPayroll.Application.Abstractions;
using HRPayroll.Domain.Entities;
using HRPayroll.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HRPayroll.Infrastructure.Repositories;

public sealed class EmployeeRepository(PayrollDbContext dbContext) : IEmployeeRepository
{
    public async Task<Employee?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await dbContext.Employees.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

    public async Task<IReadOnlyCollection<Employee>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await dbContext.Employees.AsNoTracking().OrderBy(e => e.LastName).ToListAsync(cancellationToken);

    public async Task AddAsync(Employee employee, CancellationToken cancellationToken = default) =>
        await dbContext.Employees.AddAsync(employee, cancellationToken);
}
