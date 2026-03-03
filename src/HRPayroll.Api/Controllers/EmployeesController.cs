using HRPayroll.Application.DTOs;
using HRPayroll.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRPayroll.Api.Controllers;

[ApiController]
[Route("api/employees")]
public sealed class EmployeesController(EmployeeService employeeService) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "CanViewPayroll")]
    public async Task<ActionResult<IReadOnlyCollection<EmployeeResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var employees = await employeeService.GetAllAsync(cancellationToken);
        return Ok(employees);
    }

    [HttpPost]
    [Authorize(Policy = "CanManageEmployees")]
    public async Task<ActionResult<EmployeeResponse>> Create(CreateEmployeeRequest request, CancellationToken cancellationToken)
    {
        var employee = await employeeService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetAll), new { id = employee.Id }, employee);
    }
}
