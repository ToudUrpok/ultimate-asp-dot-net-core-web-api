using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects.Employee;

namespace CompanyEmployees.Presentation.Controllers;

[Route("api/companies/{companyId}/employees")]
[ApiController]
public class EmployeesController(IServiceManager serviceManager) : ControllerBase
{
    private readonly IServiceManager _serviceManager = serviceManager;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployeesForCompany(
        Guid companyId)
    {
        var employees = await _serviceManager.EmployeeService
            .GetEmployeesByCompanyAsync(companyId, trackChanges: false);

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
    public async Task<IActionResult> CreateEmployeeForCompany(Guid companyId, [FromBody] CreateEmployeeDto data)
    {
        if (data is null)
            return BadRequest($"{nameof(CreateEmployeeDto)} object is null.");

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

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
    public async Task<IActionResult> UpdateEmployee(Guid companyId, Guid id, [FromBody] UpdateEmployeeDto data)
    {
        if (data is null)
            return BadRequest($"{nameof(UpdateEmployeeDto)} object is null.");

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

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
