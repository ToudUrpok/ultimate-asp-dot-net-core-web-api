using Shared.DataTransferObjects.Company;

namespace Service.Contracts;

public interface ICompanyService
{
    IEnumerable<CompanyDto> GetAll(bool trackChanges);
    CompanyDto GetById(Guid id, bool trackChanges);
    IEnumerable<CompanyDto> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
    CompanyDto CreateCompany(CreateCompanyDto data);
    (IEnumerable<CompanyDto> companies, string ids) CreateCompanies(IEnumerable<CreateCompanyDto> data);
    void DeleteCompany(Guid companyId, bool trackChanges);
    void UpdateCompany(Guid companyid, UpdateCompanyDto data, bool trackChanges);
}
