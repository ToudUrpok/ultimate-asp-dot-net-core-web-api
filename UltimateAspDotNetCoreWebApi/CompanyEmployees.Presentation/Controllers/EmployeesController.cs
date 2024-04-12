using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace CompanyEmployees.Presentation.Controllers;

[Route("api/companies/{companyId}/employees")]
[ApiController]
public class EmployeesController(IServiceManager serviceManager) : ControllerBase
{
    private readonly IServiceManager _serviceManager = serviceManager;

    [HttpGet]
    public ActionResult<IEnumerable<Employee>> GetEmployeesForCompany(Guid companyId)
    {
        var employees = _serviceManager.EmployeeService
            .GetEmployeesByCompany(companyId, trackChanges: false);

        return Ok(employees);
    }
}
