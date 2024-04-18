using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class CompanyRepository(RepositoryContext repositoryContext) : 
    RepositoryBase<Company>(repositoryContext), ICompanyRepository
{
    public async Task<IEnumerable<Company>> GetAllAsync(bool trackChanges) =>
        await FindAll(trackChanges)
            .OrderBy(c => c.Name)
            .ToListAsync();
    
    public async Task<Company?> GetByIdAsync(Guid id, bool trackChanges) =>
        await FindByCondition(c => c.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();

    public async Task<IEnumerable<Company>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) =>
        await FindByCondition(x => ids.Contains(x.Id), trackChanges)
            .ToListAsync();

    public void CreateCompany(Company company) => Create(company);

    public void DeleteCompany(Company company) => Delete(company);
}
