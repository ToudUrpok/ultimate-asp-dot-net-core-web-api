using Shared.DataTransferObjects.Company;

namespace Service.Contracts;

public interface ICompanyService
{
    Task<IEnumerable<CompanyDto>> GetAllAsync(bool trackChanges);
    Task<CompanyDto> GetByIdAsync(Guid id, bool trackChanges);
    Task<IEnumerable<CompanyDto>> GetByIdsAsync(
        IEnumerable<Guid> ids, bool trackChanges);
    Task<CompanyDto> CreateCompanyAsync(CreateCompanyDto data);
    Task<(IEnumerable<CompanyDto> companies, string ids)> CreateCompaniesAsync(
        IEnumerable<CreateCompanyDto> data);
    Task DeleteCompanyAsync(Guid companyId, bool trackChanges);
    Task UpdateCompanyAsync(Guid companyid, UpdateCompanyDto data, bool trackChanges);
}
