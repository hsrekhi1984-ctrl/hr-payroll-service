using HRPayroll.Domain.Common;
using HRPayroll.Domain.Enums;

namespace HRPayroll.Domain.Entities;

public sealed class Employee : BaseEntity
{
    public string EmployeeNumber { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public decimal BaseSalary { get; set; }
    public decimal TaxRate { get; set; }
    public bool IsActive { get; set; } = true;
    public EmployeeRole Role { get; set; } = EmployeeRole.Employee;

    public ICollection<PayrollRecord> PayrollRecords { get; set; } = new List<PayrollRecord>();
}
