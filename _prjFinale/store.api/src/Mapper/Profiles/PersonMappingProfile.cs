using AutoMapper;
using store.api.src.Dto.Person;
using store.core.src.Domain.Entity.User;
namespace store.api.src.Mapper.Profiles;

public class PersonMappingProfile : Profile
{
    public PersonMappingProfile()
    {
        CreateMap<Person, PersonCreateRequest>().ReverseMap();
        CreateMap<Person, PersonUpdateRequest>().ReverseMap();
    }
}