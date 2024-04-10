using Contracts;
using Entities.Models;

namespace Repository;

public class EmployeeRepository(RepositoryContext repositoryContext) :
    RepositoryBase<Employee>(repositoryContext), IEmployeeRepository
{
}
