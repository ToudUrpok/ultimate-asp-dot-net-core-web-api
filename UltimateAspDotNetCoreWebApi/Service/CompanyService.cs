using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects.Company;
using System.ComponentModel.Design;

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

    public IEnumerable<CompanyDto> GetByIds(IEnumerable<Guid> ids, bool trackChanges)
    {
        if (ids is null)
            throw new InvalidParameterValueException<IEnumerable<Guid>?>(nameof(ids), ids);

        var companyEntries = _repository.Company.GetByIds(ids, trackChanges);

        if (ids.Count() != companyEntries.Count())
            throw new InvalidResultCollectionLengthException(ids.Count(), companyEntries.Count());

        return _mapper.Map<IEnumerable<CompanyDto>>(companyEntries);
    }


    public CompanyDto CreateCompany(CreateCompanyDto data)
    {
        var companyEntry = _mapper.Map<Company>(data);

        _repository.Company.CreateCompany(companyEntry);
        _repository.Save();

        return _mapper.Map<CompanyDto>(companyEntry);
    }

    public (IEnumerable<CompanyDto> companies, string ids) CreateCompanies(IEnumerable<CreateCompanyDto> data)
    {
        if (data is null)
            throw new InvalidParameterValueException<IEnumerable<CreateCompanyDto>?>(nameof(data), data);

        var companyEntries = _mapper.Map<IEnumerable<Company>>(data);

        foreach (var company in companyEntries)
        {
            _repository.Company.CreateCompany(company);
        }
        _repository.Save();

        var resultCompanyDtos = _mapper.Map<IEnumerable<CompanyDto>>(companyEntries);
        var resultIds = string.Join(",", resultCompanyDtos.Select(c => c.Id));

        return (companies: resultCompanyDtos, ids: resultIds);
    }

    public void DeleteCompany(Guid companyId, bool trackChanges)
    {
        var companyEntry = _repository.Company.GetById(companyId, trackChanges) ??
            throw new CompanyNotFoundException(companyId);

        _repository.Company.DeleteCompany(companyEntry);
        _repository.Save();
    }

    public void UpdateCompany(Guid companyId, UpdateCompanyDto data, bool trackChanges)
    {
        var companyEntry = _repository.Company.GetById(companyId, trackChanges) ??
            throw new CompanyNotFoundException(companyId);

        _mapper.Map(data, companyEntry);
        _repository.Save();
    }
}
