using AutoMapper;
using store.api.src.Dto.Employee;
using store.core.src.Domain.Entity.User;
namespace store.api.src.Mapper.Profiles;

public class EmployeeMappingProfile : Profile
{
    public EmployeeMappingProfile()
    {
        CreateMap<Employee, EmployeeCreateRequest>().ReverseMap();
        CreateMap<Employee, EmployeeUpdateRequest>().ReverseMap();
    }
}