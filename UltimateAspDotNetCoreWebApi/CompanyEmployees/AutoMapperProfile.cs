using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects.Company;

namespace CompanyEmployees;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Company, CompanyDto>()
            .ForCtorParam(nameof(CompanyDto.FullAddress),
                opt => opt.MapFrom(src => string.Join(", ", src.Country, src.Address)));
    }
}
