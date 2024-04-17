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

    public IEnumerable<EmployeeDto> GetEmployeesByCompany(Guid companyId, bool trackChanges)
    {
        _ = _repository.Company.GetById(companyId, trackChanges) ??
            throw new CompanyNotFoundException(companyId);

        var employees = _repository.Employee
            .GetEmployeesByCompany(companyId, trackChanges);

        return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
    }

    public EmployeeDto GetEmployeeById(Guid companyId, Guid id, bool trackChanges)
    {
        _ = _repository.Company.GetById(companyId, trackChanges) ??
            throw new CompanyNotFoundException(companyId);

        var employee = _repository.Employee
            .GetEmployeeById(companyId, id, trackChanges) ??
                throw new EmployeeNotFoundException(id);

        return _mapper.Map<EmployeeDto>(employee);
    }

    public EmployeeDto CreateEmployeeForCompany(Guid companyId, CreateEmployeeDto data, bool trackChanges)
    {
        _ = _repository.Company.GetById(companyId, trackChanges) ??
            throw new CompanyNotFoundException(companyId);

        var employeeEntry = _mapper.Map<Employee>(data);

        _repository.Employee.CreateEmployeeForCompany(companyId, employeeEntry);
        _repository.Save();

        return _mapper.Map<EmployeeDto>(employeeEntry);
    }

    public void DeleteEmployee(Guid companyId, Guid id, bool trackChanges)
    {
        _ = _repository.Company.GetById(companyId, trackChanges) ??
            throw new CompanyNotFoundException(companyId);

        var employeeEntry = _repository.Employee
            .GetEmployeeById(companyId, id, trackChanges) ??
                throw new EmployeeNotFoundException(id);

        _repository.Employee.DeleteEmployee(employeeEntry);
        _repository.Save();
    }

    public void UpdateEmployee(Guid companyId, Guid id, UpdateEmployeeDto data, bool trackCompanyChanges, bool trackEmployeeChanges)
    {
        _ = _repository.Company.GetById(companyId, trackCompanyChanges) ??
            throw new CompanyNotFoundException(companyId);

        var employeeEntry = _repository.Employee
            .GetEmployeeById(companyId, id, trackEmployeeChanges) ??
                throw new EmployeeNotFoundException(id);

        _mapper.Map(data, employeeEntry);
        _repository.Save();
    }

    public (UpdateEmployeeDto employeeDto, Employee employeeEntry) GetEmployeeDtoAndEntryTuple(
        Guid companyId, Guid id, bool trackCompanyChanges, bool trackEmployeeChanges)
    {
        _ = _repository.Company.GetById(companyId, trackCompanyChanges) ??
            throw new CompanyNotFoundException(companyId);

        var employeeEntry = _repository.Employee
            .GetEmployeeById(companyId, id, trackEmployeeChanges) ??
                throw new EmployeeNotFoundException(id);

        return (_mapper.Map<UpdateEmployeeDto>(employeeEntry), employeeEntry);
    }

    public void PatchEmployee(UpdateEmployeeDto srcEmployeeDto, Employee destEmployeeEntry)
    {
        _mapper.Map(srcEmployeeDto, destEmployeeEntry);
        _repository.Save();
    }
}
