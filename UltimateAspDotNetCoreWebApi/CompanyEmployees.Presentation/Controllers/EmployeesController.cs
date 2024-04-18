using CompanyEmployees.Presentation.ActionFilters;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects.Employee;
using Shared.QueryParameters.Employee;
using System.Text.Json;

namespace CompanyEmployees.Presentation.Controllers;

[Route("api/companies/{companyId}/employees")]
[ApiController]
public class EmployeesController(IServiceManager serviceManager) : ControllerBase
{
    private readonly IServiceManager _serviceManager = serviceManager;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployeesForCompany(
        Guid companyId, [FromQuery] GetEmployeesForCompanyParams queryParams)
    {
        var (employees, pagingData) = await _serviceManager.EmployeeService
            .GetEmployeesForCompanyAsync(companyId, queryParams, trackChanges: false);
        
        Response.Headers["X-Pagination"] = JsonSerializer.Serialize(pagingData);

        return Ok(employees);
    }

    [HttpGet("{id:guid}", Name = nameof(GetEmployeeById))]
    public async Task<ActionResult<EmployeeDto>> GetEmployeeById(Guid companyId, Guid id)
    {
        var employee = await _serviceManager.EmployeeService
            .GetEmployeeByIdAsync(companyId, id, trackChanges: false);

        return Ok(employee);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateEmployeeForCompany(Guid companyId, [FromBody] CreateEmployeeDto data)
    {
        var createdEmployee = await _serviceManager.EmployeeService
            .CreateEmployeeForCompanyAsync(companyId, data, trackChanges: false);

        return CreatedAtRoute(
            nameof(GetEmployeeById),
            new {
                companyId,
                id = createdEmployee.Id
            },
            createdEmployee
        );
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteEmployee(Guid companyId, Guid id)
    {
        await _serviceManager.EmployeeService.DeleteEmployeeAsync(companyId, id, trackChanges: false);

        return NoContent();
    }

    [HttpPut("{id:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateEmployee(Guid companyId, Guid id, [FromBody] UpdateEmployeeDto data)
    {
        await _serviceManager.EmployeeService
            .UpdateEmployeeAsync(companyId, id, data, trackCompanyChanges: false, trackEmployeeChanges: true);

        return NoContent();
    }

    // !!! set Content-Type header to 'application/json-patch+json' value
    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> PatchEmployee(Guid companyId, Guid id,
        [FromBody] JsonPatchDocument<UpdateEmployeeDto> patchDoc)
    {
        if (patchDoc is null)
            return BadRequest($"{nameof(JsonPatchDocument<UpdateEmployeeDto>)} object sent from client is null.");

        var (employeeDto, employeeEntry) = await _serviceManager.EmployeeService
            .GetEmployeeDtoAndEntryTupleAsync(companyId, id, trackCompanyChanges: false, trackEmployeeChanges: true);

        patchDoc.ApplyTo(employeeDto, ModelState);
        TryValidateModel(employeeDto);
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        await _serviceManager.EmployeeService
            .PatchEmployeeAsync(employeeDto, employeeEntry);

        return NoContent();
    }
}
