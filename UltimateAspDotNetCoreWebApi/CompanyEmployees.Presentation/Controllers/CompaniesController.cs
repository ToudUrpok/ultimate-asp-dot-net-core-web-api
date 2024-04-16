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

    [HttpGet("{id:guid}", Name = nameof(GetCompanyById))]
    public ActionResult<CompanyDto> GetCompanyById(Guid id)
    {
        var company = _serviceManager.CompanyService.GetById(id, trackChanges: false);

        return Ok(company);
    }

    [HttpPost]
    public IActionResult CreateCompany([FromBody] CreateCompanyDto data)
    {
        if (data is null)
            return BadRequest($"{nameof(CreateCompanyDto)} object is null.");

        var createdCompany = _serviceManager.CompanyService.CreateCompany(data);

        return CreatedAtRoute(nameof(GetCompanyById), new { id = createdCompany.Id }, createdCompany);
    }
}

