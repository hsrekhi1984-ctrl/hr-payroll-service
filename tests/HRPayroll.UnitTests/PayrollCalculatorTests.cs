using Xunit;
using FluentAssertions;
using HRPayroll.Application.Services;

namespace HRPayroll.UnitTests;

public sealed class PayrollCalculatorTests
{
    [Fact]
    public void Calculate_ShouldComputeGrossTaxAndNet()
    {
        var calculator = new PayrollCalculator();

        var result = calculator.Calculate(10000m, 0.2m);

        result.grossPay.Should().Be(10000m);
        result.taxAmount.Should().Be(2000m);
        result.netPay.Should().Be(8000m);
    }

    [Fact]
    public void Calculate_ShouldRoundTo2DecimalPlaces()
    {
        var calculator = new PayrollCalculator();

        var result = calculator.Calculate(1234.567m, 0.1765m);

        result.grossPay.Should().Be(1234.57m);
        result.taxAmount.Should().Be(217.9m);
        result.netPay.Should().Be(1016.67m);
    }
}
