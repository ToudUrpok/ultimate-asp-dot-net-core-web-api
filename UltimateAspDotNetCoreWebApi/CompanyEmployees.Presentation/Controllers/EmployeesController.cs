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
    public ActionResult<IEnumerable<EmployeeDto>> GetEmployeesForCompany(Guid companyId)
    {
        var employees = _serviceManager.EmployeeService
            .GetEmployeesByCompany(companyId, trackChanges: false);

        return Ok(employees);
    }

    [HttpGet("{id:guid}", Name = nameof(GetEmployeeById))]
    public ActionResult<EmployeeDto> GetEmployeeById(Guid companyId, Guid id)
    {
        var employee = _serviceManager.EmployeeService
            .GetEmployeeById(companyId, id, trackChanges: false);

        return Ok(employee);
    }

    [HttpPost]
    public IActionResult CreateEmployeeForCompany(Guid companyId, [FromBody] CreateEmployeeDto data)
    {
        if (data is null)
            return BadRequest($"{nameof(CreateEmployeeDto)} object is null.");

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        var createdEmployee = _serviceManager.EmployeeService
            .CreateEmployeeForCompany(companyId, data, trackChanges: false);

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
    public IActionResult DeleteEmployee(Guid companyId, Guid id)
    {
        _serviceManager.EmployeeService.DeleteEmployee(companyId, id, trackChanges: false);

        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpdateEmployee(Guid companyId, Guid id, [FromBody] UpdateEmployeeDto data)
    {
        if (data is null)
            return BadRequest($"{nameof(UpdateEmployeeDto)} object is null.");

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        _serviceManager.EmployeeService.UpdateEmployee(companyId, id, data,
            trackCompanyChanges: false, trackEmployeeChanges: true);

        return NoContent();
    }

    // !!! set Content-Type header to 'application/json-patch+json' value
    [HttpPatch("{id:guid}")]
    public IActionResult PatchEmployee(Guid companyId, Guid id,
        [FromBody] JsonPatchDocument<UpdateEmployeeDto> patchDoc)
    {
        if (patchDoc is null)
            return BadRequest($"{nameof(JsonPatchDocument<UpdateEmployeeDto>)} object sent from client is null.");

        var (employeeDto, employeeEntry) = _serviceManager.EmployeeService
            .GetEmployeeDtoAndEntryTuple(companyId, id,
                trackCompanyChanges: false, trackEmployeeChanges: true);

        patchDoc.ApplyTo(employeeDto);
        _serviceManager.EmployeeService.PatchEmployee(employeeDto, employeeEntry);

        return NoContent();
    }
}
