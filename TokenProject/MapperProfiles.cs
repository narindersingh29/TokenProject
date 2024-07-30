
using AutoMapper;
using TokenProject.dtos;
using TokenProject.Entities;


namespace TokenProject
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {

            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
