using Entities.Models;

namespace Contracts;

public interface ICompanyRepository
{
    Task<IEnumerable<Company>> GetAllAsync(bool trackChanges);
    Task<Company?> GetByIdAsync(Guid id, bool trackChanges);
    Task<IEnumerable<Company>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
    void CreateCompany(Company company);
    void DeleteCompany(Company company);
}
