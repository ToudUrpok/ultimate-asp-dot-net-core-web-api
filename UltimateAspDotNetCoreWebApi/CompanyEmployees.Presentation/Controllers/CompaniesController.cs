using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects.Company;
using CompanyEmployees.Presentation.ModelBinders;

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

    [HttpGet("collection/({ids})", Name = nameof(GetCompaniesCollectionById))]
    public ActionResult<IEnumerable<CompanyDto>> GetCompaniesCollectionById(
        [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
    {
        var companies = _serviceManager.CompanyService.GetByIds(ids, trackChanges: false);

        return Ok(companies);
    }

    [HttpPost]
    public IActionResult CreateCompany([FromBody] CreateCompanyDto data)
    {
        if (data is null)
            return BadRequest($"{nameof(CreateCompanyDto)} object is null.");

        var createdCompany = _serviceManager.CompanyService.CreateCompany(data);

        return CreatedAtRoute(nameof(GetCompanyById), new { id = createdCompany.Id }, createdCompany);
    }

    [HttpPost("collection")]
    public IActionResult CreateCompanyCollection([FromBody] IEnumerable<CreateCompanyDto> data)
    {
        var (companies, ids) = _serviceManager.CompanyService.CreateCompanies(data);

        return CreatedAtRoute(nameof(GetCompaniesCollectionById), new { ids }, companies);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteCompany(Guid id)
    {
        _serviceManager.CompanyService.DeleteCompany(id, trackChanges: false);

        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpdateCompany(Guid id, [FromBody] UpdateCompanyDto data)
    {
        if (data is null)
            return BadRequest("CompanyForUpdateDto object is null");

        _serviceManager.CompanyService.UpdateCompany(id, data, trackChanges: true);

        return NoContent();
    }
}

