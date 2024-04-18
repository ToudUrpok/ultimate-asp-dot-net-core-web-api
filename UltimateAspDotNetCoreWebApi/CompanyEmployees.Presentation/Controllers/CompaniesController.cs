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
    public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompanies()
    {
        var companies = await _serviceManager.CompanyService
            .GetAllAsync(trackChanges: false);

        return Ok(companies);
    }

    [HttpGet("{id:guid}", Name = nameof(GetCompanyById))]
    public async Task<ActionResult<CompanyDto>> GetCompanyById(Guid id)
    {
        var company = await _serviceManager.CompanyService
            .GetByIdAsync(id, trackChanges: false);

        return Ok(company);
    }

    [HttpGet("collection/({ids})", Name = nameof(GetCompaniesCollectionById))]
    public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompaniesCollectionById(
        [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
    {
        var companies = await _serviceManager.CompanyService
            .GetByIdsAsync(ids, trackChanges: false);

        return Ok(companies);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCompany([FromBody] CreateCompanyDto data)
    {
        if (data is null)
            return BadRequest($"{nameof(CreateCompanyDto)} object is null.");

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        var createdCompany = await _serviceManager.CompanyService
            .CreateCompanyAsync(data);

        return CreatedAtRoute(nameof(GetCompanyById), new { id = createdCompany.Id }, createdCompany);
    }

    [HttpPost("collection")]
    public async Task<IActionResult> CreateCompanyCollection([FromBody] IEnumerable<CreateCompanyDto> data)
    {
        var (companies, ids) = await _serviceManager.CompanyService
            .CreateCompaniesAsync(data);

        return CreatedAtRoute(nameof(GetCompaniesCollectionById), new { ids }, companies);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCompany(Guid id)
    {
        await _serviceManager.CompanyService
            .DeleteCompanyAsync(id, trackChanges: false);

        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateCompany(Guid id, [FromBody] UpdateCompanyDto data)
    {
        if (data is null)
            return BadRequest("CompanyForUpdateDto object is null");

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        await _serviceManager.CompanyService
            .UpdateCompanyAsync(id, data, trackChanges: true);

        return NoContent();
    }
}

