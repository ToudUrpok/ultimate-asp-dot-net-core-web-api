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
            return BadRequest($"{nameof(CreateEmployeeDto)} object is null");

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
}
