using Shared.DataTransferObjects.Employee;

namespace Shared.DataTransferObjects.Company;

public record CreateCompanyDto(string Name, string Address, string Country,
    IEnumerable<CreateEmployeeDto> Employees);
