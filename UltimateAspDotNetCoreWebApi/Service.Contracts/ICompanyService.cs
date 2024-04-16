using Shared.DataTransferObjects.Company;

namespace Service.Contracts;

public interface ICompanyService
{
    IEnumerable<CompanyDto> GetAll(bool trackChanges);
    CompanyDto GetById(Guid id, bool trackChanges);
    CompanyDto CreateCompany(CreateCompanyDto data);
}
