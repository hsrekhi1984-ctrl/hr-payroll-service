using HRPayroll.Application.DTOs;
using HRPayroll.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRPayroll.Api.Controllers;

[ApiController]
[Route("api/payroll")]
public sealed class PayrollController(PayrollService payrollService) : ControllerBase
{
    [HttpGet("employee/{employeeId:guid}")]
    [Authorize(Policy = "CanViewPayroll")]
    public async Task<ActionResult<IReadOnlyCollection<PayrollResponse>>> GetForEmployee(Guid employeeId, CancellationToken cancellationToken)
    {
        var records = await payrollService.GetByEmployeeAsync(employeeId, cancellationToken);
        return Ok(records);
    }

    [HttpPost]
    [Authorize(Policy = "CanProcessPayroll")]
    public async Task<ActionResult<PayrollResponse>> Create(CreatePayrollRequest request, CancellationToken cancellationToken)
    {
        var record = await payrollService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetForEmployee), new { employeeId = record.EmployeeId }, record);
    }
}
