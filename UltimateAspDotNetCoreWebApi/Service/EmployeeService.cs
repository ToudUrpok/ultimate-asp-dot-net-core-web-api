using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects.Employee;

namespace Service;

internal sealed class EmployeeService(IRepositoryManager repository,
    ILoggerManager logger, IMapper mapper) : IEmployeeService
{
    private readonly IRepositoryManager _repository = repository;
    private readonly ILoggerManager _logger = logger;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<EmployeeDto>> GetEmployeesByCompanyAsync(
        Guid companyId, bool trackChanges)
    {
        _ = await _repository.Company.GetByIdAsync(companyId, trackChanges) ??
            throw new CompanyNotFoundException(companyId);

        var employees = await _repository.Employee
            .GetEmployeesByCompanyAsync(companyId, trackChanges);

        return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
    }

    public async Task<EmployeeDto> GetEmployeeByIdAsync(
        Guid companyId, Guid id, bool trackChanges)
    {
        _ = await _repository.Company.GetByIdAsync(companyId, trackChanges) ??
            throw new CompanyNotFoundException(companyId);

        var employee = await _repository.Employee
            .GetEmployeeByIdAsync(companyId, id, trackChanges) ??
                throw new EmployeeNotFoundException(id);

        return _mapper.Map<EmployeeDto>(employee);
    }

    public async Task<EmployeeDto> CreateEmployeeForCompanyAsync(Guid companyId, CreateEmployeeDto data, bool trackChanges)
    {
        _ = await _repository.Company.GetByIdAsync(companyId, trackChanges) ??
            throw new CompanyNotFoundException(companyId);

        var employeeEntry = _mapper.Map<Employee>(data);

        _repository.Employee.CreateEmployeeForCompany(companyId, employeeEntry);
        await _repository.SaveAsync();

        return _mapper.Map<EmployeeDto>(employeeEntry);
    }

    public async Task DeleteEmployeeAsync(Guid companyId, Guid id, bool trackChanges)
    {
        _ = await _repository.Company.GetByIdAsync(companyId, trackChanges) ??
            throw new CompanyNotFoundException(companyId);

        var employeeEntry = await _repository.Employee
            .GetEmployeeByIdAsync(companyId, id, trackChanges) ??
                throw new EmployeeNotFoundException(id);

        _repository.Employee.DeleteEmployee(employeeEntry);
        await _repository.SaveAsync();
    }

    public async Task UpdateEmployeeAsync(Guid companyId, Guid id, UpdateEmployeeDto data, bool trackCompanyChanges, bool trackEmployeeChanges)
    {
        _ = await _repository.Company.GetByIdAsync(companyId, trackCompanyChanges) ??
            throw new CompanyNotFoundException(companyId);

        var employeeEntry = await _repository.Employee
            .GetEmployeeByIdAsync(companyId, id, trackEmployeeChanges) ??
                throw new EmployeeNotFoundException(id);

        _mapper.Map(data, employeeEntry);
        await _repository.SaveAsync();
    }

    public async Task<(UpdateEmployeeDto employeeDto, Employee employeeEntry)> GetEmployeeDtoAndEntryTupleAsync(
        Guid companyId, Guid id, bool trackCompanyChanges, bool trackEmployeeChanges)
    {
        _ = await _repository.Company.GetByIdAsync(companyId, trackCompanyChanges) ??
            throw new CompanyNotFoundException(companyId);

        var employeeEntry = await _repository.Employee
            .GetEmployeeByIdAsync(companyId, id, trackEmployeeChanges) ??
                throw new EmployeeNotFoundException(id);

        return (_mapper.Map<UpdateEmployeeDto>(employeeEntry), employeeEntry);
    }

    public async Task PatchEmployeeAsync(UpdateEmployeeDto srcEmployeeDto, Employee destEmployeeEntry)
    {
        _mapper.Map(srcEmployeeDto, destEmployeeEntry);
        await _repository.SaveAsync();
    }
}
