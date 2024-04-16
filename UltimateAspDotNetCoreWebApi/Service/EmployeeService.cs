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

        var employees = _repository.Employee.GetEmployeesByCompany(companyId, trackChanges);

        return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
    }

    public EmployeeDto GetEmployeeById(Guid companyId, Guid id, bool trackChanges)
    {
        _ = _repository.Company.GetById(companyId, trackChanges) ??
            throw new CompanyNotFoundException(companyId);

        var employee = _repository.Employee.GetEmployeeById(companyId, id, trackChanges) ??
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
}
