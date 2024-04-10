using Contracts;
using Entities.Models;

namespace Repository;

public class CompanyRepository(RepositoryContext repositoryContext) : 
    RepositoryBase<Company>(repositoryContext), ICompanyRepository
{
}

