using HRPayroll.Application.Interfaces;

namespace HRPayroll.Application.Services;

public sealed class PayrollCalculator : IPayrollCalculator
{
    public (decimal grossPay, decimal taxAmount, decimal netPay) Calculate(decimal baseSalary, decimal taxRate)
    {
        var grossPay = decimal.Round(baseSalary, 2, MidpointRounding.AwayFromZero);
        var taxAmount = decimal.Round(grossPay * taxRate, 2, MidpointRounding.AwayFromZero);
        var netPay = grossPay - taxAmount;
        return (grossPay, taxAmount, netPay);
    }
}
