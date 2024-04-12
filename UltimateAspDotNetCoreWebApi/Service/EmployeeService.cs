using AutoMapper;
using Contracts;
using Entities.Exceptions;
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
}
