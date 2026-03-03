using HRPayroll.Domain.Common;

namespace HRPayroll.Domain.Entities;

public sealed class PayrollRecord : BaseEntity
{
    public Guid EmployeeId { get; set; }
    public Employee Employee { get; set; } = default!;
    public DateOnly PeriodStart { get; set; }
    public DateOnly PeriodEnd { get; set; }
    public decimal GrossPay { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal NetPay { get; set; }
    public string Currency { get; set; } = "USD";
}
