namespace HRPayroll.Application.Interfaces;

public interface IPayrollCalculator
{
    (decimal grossPay, decimal taxAmount, decimal netPay) Calculate(decimal baseSalary, decimal taxRate);
}
