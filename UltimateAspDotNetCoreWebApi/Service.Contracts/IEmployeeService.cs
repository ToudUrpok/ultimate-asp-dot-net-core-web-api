using Entities.Models;
using Shared.DataTransferObjects.Employee;

namespace Service.Contracts;

public interface IEmployeeService
{
    IEnumerable<EmployeeDto> GetEmployeesByCompany(Guid companyId, bool trackChanges);
    EmployeeDto GetEmployeeById(Guid companyId, Guid id, bool trackChanges);
    EmployeeDto CreateEmployeeForCompany(Guid companyId, CreateEmployeeDto data, bool trackChanges);
    void DeleteEmployee(Guid companyId, Guid id, bool trackChanges);
    void UpdateEmployee(Guid companyId, Guid id, UpdateEmployeeDto data, bool trackCompanyChanges, bool trackEmployeeChanges);
    (UpdateEmployeeDto employeeDto, Employee employeeEntry) GetEmployeeDtoAndEntryTuple(
        Guid companyId, Guid id, bool trackCompanyChanges, bool trackEmployeeChanges);
    void PatchEmployee(UpdateEmployeeDto srcEmployeeDto, Employee destEmployeeEntry);
}
