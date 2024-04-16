using Contracts;
using Entities.Models;

namespace Repository;

public class CompanyRepository(RepositoryContext repositoryContext) : 
    RepositoryBase<Company>(repositoryContext), ICompanyRepository
{
    public IEnumerable<Company> GetAll(bool trackChanges) =>
        [.. FindAll(trackChanges).OrderBy(c => c.Name)];
    
    public Company? GetById(Guid id, bool trackChanges) =>
        FindByCondition(c => c.Id.Equals(id), trackChanges)
            .SingleOrDefault();

    public void CreateCompany(Company company) => Create(company);
}
