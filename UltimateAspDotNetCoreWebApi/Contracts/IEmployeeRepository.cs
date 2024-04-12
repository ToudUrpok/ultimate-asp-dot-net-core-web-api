using Entities.Models;

namespace Contracts;

public interface IEmployeeRepository
{
    IEnumerable<Employee> GetEmployeesByCompany(Guid companyId, bool trackChanges);
}
