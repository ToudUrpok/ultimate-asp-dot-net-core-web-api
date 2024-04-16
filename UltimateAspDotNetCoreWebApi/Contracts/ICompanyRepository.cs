using Entities.Models;

namespace Contracts;

public interface ICompanyRepository
{
    IEnumerable<Company> GetAll(bool trackChanges);
    Company? GetById(Guid id, bool trackChanges);
    IEnumerable<Company> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
    void CreateCompany(Company company);
    void DeleteCompany(Company company);
}
