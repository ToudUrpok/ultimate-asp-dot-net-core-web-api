using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Service.Contracts;
using Shared.DataTransferObjects.Company;

namespace Service;

internal sealed class CompanyService(IRepositoryManager repository, 
    ILoggerManager logger, IMapper mapper) : ICompanyService
{
    private readonly IRepositoryManager _repository = repository;
    private readonly ILoggerManager _logger = logger;
    private readonly IMapper _mapper = mapper;

    public IEnumerable<CompanyDto> GetAll(bool trackChanges)
    {
        var companies = _repository.Company.GetAll(trackChanges);

        return _mapper.Map<IEnumerable<CompanyDto>>(companies);
    }

    public CompanyDto GetById(Guid id, bool trackChanges)
    {
        var company = _repository.Company.GetById(id, trackChanges) ??
            throw new CompanyNotFoundException(id);

        return _mapper.Map<CompanyDto>(company);
    }
}
