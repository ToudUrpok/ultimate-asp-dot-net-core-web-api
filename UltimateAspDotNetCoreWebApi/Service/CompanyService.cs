using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects.Company;

namespace Service;

internal sealed class CompanyService(IRepositoryManager repository, 
    ILoggerManager logger, IMapper mapper) : ICompanyService
{
    private readonly IRepositoryManager _repository = repository;
    private readonly ILoggerManager _logger = logger;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<CompanyDto>> GetAllAsync(bool trackChanges)
    {
        var companies = await _repository.Company.GetAllAsync(trackChanges);

        return _mapper.Map<IEnumerable<CompanyDto>>(companies);
    }

    public async Task<CompanyDto> GetByIdAsync(Guid id, bool trackChanges)
    {
        var company = await _repository.Company.GetByIdAsync(id, trackChanges) ??
            throw new CompanyNotFoundException(id);

        return _mapper.Map<CompanyDto>(company);
    }

    public async Task<IEnumerable<CompanyDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
    {
        if (ids is null)
            throw new InvalidParameterValueException<IEnumerable<Guid>?>(nameof(ids), ids);

        var companyEntries =await  _repository.Company.GetByIdsAsync(ids, trackChanges);

        if (ids.Count() != companyEntries.Count())
            throw new InvalidResultCollectionLengthException(ids.Count(), companyEntries.Count());

        return _mapper.Map<IEnumerable<CompanyDto>>(companyEntries);
    }


    public async Task<CompanyDto> CreateCompanyAsync(CreateCompanyDto data)
    {
        var companyEntry = _mapper.Map<Company>(data);

        _repository.Company.CreateCompany(companyEntry);
        await _repository.SaveAsync();

        return _mapper.Map<CompanyDto>(companyEntry);
    }

    public async Task<(IEnumerable<CompanyDto> companies, string ids)> CreateCompaniesAsync(
        IEnumerable<CreateCompanyDto> data)
    {
        if (data is null)
            throw new InvalidParameterValueException<IEnumerable<CreateCompanyDto>?>(nameof(data), data);

        var companyEntries = _mapper.Map<IEnumerable<Company>>(data);

        foreach (var company in companyEntries)
        {
            _repository.Company.CreateCompany(company);
        }
        await _repository.SaveAsync();

        var resultCompanyDtos = _mapper.Map<IEnumerable<CompanyDto>>(companyEntries);
        var resultIds = string.Join(",", resultCompanyDtos.Select(c => c.Id));

        return (companies: resultCompanyDtos, ids: resultIds);
    }

    public async Task DeleteCompanyAsync(Guid companyId, bool trackChanges)
    {
        var companyEntry = await _repository.Company.GetByIdAsync(companyId, trackChanges) ??
            throw new CompanyNotFoundException(companyId);

        _repository.Company.DeleteCompany(companyEntry);
        await _repository.SaveAsync();
    }

    public async Task UpdateCompanyAsync(Guid companyId, UpdateCompanyDto data, bool trackChanges)
    {
        var companyEntry = await _repository.Company.GetByIdAsync(companyId, trackChanges) ??
            throw new CompanyNotFoundException(companyId);

        _mapper.Map(data, companyEntry);
        await _repository.SaveAsync();
    }
}
