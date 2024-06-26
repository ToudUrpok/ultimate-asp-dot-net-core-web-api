﻿using Entities.Models;
using Shared.DataTransferObjects.Employee;
using Shared.MetaData;
using Shared.QueryParameters.Employee;

namespace Service.Contracts;

public interface IEmployeeService
{
    Task<(IEnumerable<EmployeeDto> employees, ListPagingData pagingData)> GetEmployeesForCompanyAsync(
        Guid companyId, GetEmployeesForCompanyParams parameters, bool trackChanges);
    Task<EmployeeDto> GetEmployeeByIdAsync(Guid companyId, Guid id, bool trackChanges);
    Task<EmployeeDto> CreateEmployeeForCompanyAsync(Guid companyId, CreateEmployeeDto data, bool trackChanges);
    Task DeleteEmployeeAsync(Guid companyId, Guid id, bool trackChanges);
    Task UpdateEmployeeAsync(Guid companyId, Guid id, UpdateEmployeeDto data, bool trackCompanyChanges, bool trackEmployeeChanges);
    Task<(UpdateEmployeeDto employeeDto, Employee employeeEntry)> GetEmployeeDtoAndEntryTupleAsync(
        Guid companyId, Guid id, bool trackCompanyChanges, bool trackEmployeeChanges);
    Task PatchEmployeeAsync(UpdateEmployeeDto srcEmployeeDto, Employee destEmployeeEntry);
}
