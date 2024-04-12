using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects.Company;
using Shared.DataTransferObjects.Employee;

namespace CompanyEmployees;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Company, CompanyDto>()
            .ForCtorParam(nameof(CompanyDto.FullAddress),
                opt => opt.MapFrom(src => string.Join(", ", src.Country, src.Address)));

        CreateMap<Employee, EmployeeDto>();
    }
}
