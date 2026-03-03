using HRPayroll.Application.Abstractions;
using HRPayroll.Application.DTOs;
using HRPayroll.Domain.Entities;

namespace HRPayroll.Application.Services;

public sealed class EmployeeService(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
{
    public async Task<EmployeeResponse> CreateAsync(CreateEmployeeRequest request, CancellationToken cancellationToken = default)
    {
        var employee = new Employee
        {
            EmployeeNumber = request.EmployeeNumber,
            FirstName = request.FirstName,
            LastName = request.LastName,
            BaseSalary = request.BaseSalary,
            TaxRate = request.TaxRate,
            Role = request.Role
        };

        await employeeRepository.AddAsync(employee, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new EmployeeResponse(employee.Id, employee.EmployeeNumber, employee.FirstName, employee.LastName, employee.BaseSalary, employee.TaxRate, employee.Role, employee.IsActive);
    }

    public async Task<IReadOnlyCollection<EmployeeResponse>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var employees = await employeeRepository.GetAllAsync(cancellationToken);
        return employees.Select(e => new EmployeeResponse(e.Id, e.EmployeeNumber, e.FirstName, e.LastName, e.BaseSalary, e.TaxRate, e.Role, e.IsActive)).ToArray();
    }
}
