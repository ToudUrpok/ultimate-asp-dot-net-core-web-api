using Contracts;
using Entities.Models;

namespace Repository;

public class CompanyRepository(RepositoryContext repositoryContext) : 
    RepositoryBase<Company>(repositoryContext), ICompanyRepository
{
    public IEnumerable<Company> GetAll(bool trackChanges) =>
        [.. FindAll(trackChanges).OrderBy(c => c.Name)];
}
