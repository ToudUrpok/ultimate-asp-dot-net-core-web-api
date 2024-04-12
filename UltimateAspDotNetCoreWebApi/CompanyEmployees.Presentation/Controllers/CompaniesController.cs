using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects.Company;

namespace CompanyEmployees.Presentation.Controllers;

[Route("api/companies")]
[ApiController]
public class CompaniesController(IServiceManager serviceManager) : ControllerBase
{
    private readonly IServiceManager _serviceManager = serviceManager;

    [HttpGet]
    public ActionResult<IEnumerable<CompanyDto>> GetCompanies()
    {
        var companies = _serviceManager.CompanyService.GetAll(trackChanges: false);

        return Ok(companies);
    }

    [HttpGet("{id:guid}")]
    public ActionResult<CompanyDto> GetCompany(Guid id)
    {
        var company = _serviceManager.CompanyService.GetById(id, trackChanges: false);

        return Ok(company);
    }

}

