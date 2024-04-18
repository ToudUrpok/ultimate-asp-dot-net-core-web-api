using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared.QueryParameters.Employee;

namespace Repository;

public class EmployeeRepository(RepositoryContext repositoryContext) :
    RepositoryBase<Employee>(repositoryContext), IEmployeeRepository
{
    public async Task<(IEnumerable<Employee> employees, int totalCount)> GetEmployeesForCompanyAsync(
        Guid companyId, GetEmployeesForCompanyParams parameters, bool trackChanges)
    {
        var employees = await FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges)
            .OrderBy(e => e.Name)
            .Skip((parameters.Page - 1) * parameters.Limit)
            .Take(parameters.Limit)
            .ToListAsync();

        var totalCount = await FindByCondition(e => e.CompanyId.Equals(companyId), false)
            .CountAsync();

        return (employees, totalCount);
    }

    public async Task<Employee?> GetEmployeeByIdAsync(Guid companyId, Guid id, bool trackChanges) =>
        await FindByCondition(e => e.CompanyId.Equals(companyId) && e.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();

    public void CreateEmployeeForCompany(Guid companyId, Employee employee)
    {
        employee.CompanyId = companyId;
        Create(employee);
    }

    public void DeleteEmployee(Employee employee) => Delete(employee);
}
