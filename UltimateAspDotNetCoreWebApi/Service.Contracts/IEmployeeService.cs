using Shared.DataTransferObjects.Employee;

namespace Service.Contracts;

public interface IEmployeeService
{
    IEnumerable<EmployeeDto> GetEmployeesByCompany(Guid companyId, bool trackChanges);
}
