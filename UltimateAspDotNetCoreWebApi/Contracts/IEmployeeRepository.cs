using Entities.Models;
using Shared.QueryParameters.Employee;

namespace Contracts;

public interface IEmployeeRepository
{
    Task<(IEnumerable<Employee> employees, int totalCount)> GetEmployeesForCompanyAsync(
        Guid companyId, GetEmployeesForCompanyParams parameters, bool trackChanges);
    Task<Employee?> GetEmployeeByIdAsync(Guid companyId, Guid id, bool trackChanges);
    void CreateEmployeeForCompany(Guid companyId, Employee employee);
    void DeleteEmployee(Employee employee);
}
