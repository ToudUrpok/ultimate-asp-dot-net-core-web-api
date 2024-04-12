using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace CompanyEmployees.Presentation.Controllers;

[Route("api/companies")]
[ApiController]
public class CompaniesController(IServiceManager serviceManager) : ControllerBase
{
    private readonly IServiceManager _serviceManager = serviceManager;

    [HttpGet]
    public ActionResult<IEnumerable<Company>> GetCompanies()
    {
        var companies = _serviceManager.CompanyService.GetAll(trackChanges: false);

        return Ok(companies);
    }

    [HttpGet("{id:guid}")]
    public ActionResult<Company> GetCompany(Guid id)
    {
        var company = _serviceManager.CompanyService.GetById(id, trackChanges: false);

        return Ok(company);
    }

}

