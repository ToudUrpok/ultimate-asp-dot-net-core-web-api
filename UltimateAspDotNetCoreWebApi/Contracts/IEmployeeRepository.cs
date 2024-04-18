using Entities.Models;

namespace Contracts;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetEmployeesByCompanyAsync(Guid companyId, bool trackChanges);
    Task<Employee?> GetEmployeeByIdAsync(Guid companyId, Guid id, bool trackChanges);
    void CreateEmployeeForCompany(Guid companyId, Employee employee);
    void DeleteEmployee(Employee employee);
}
