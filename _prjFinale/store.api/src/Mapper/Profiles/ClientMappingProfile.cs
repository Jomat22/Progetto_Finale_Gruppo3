using AutoMapper;
using store.api.src.Dto.Client;
using store.core.src.Domain.Entity.User;
namespace store.api.src.Mapper.Profiles;

public class ClientMappingProfile : Profile
{
    public ClientMappingProfile()
    {
        CreateMap<Client, ClientCreateRequest>().ReverseMap();
        CreateMap<Client, ClientUpdateRequest>().ReverseMap();
    }
}