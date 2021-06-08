using AutoMapper;
using Zadanie.API.Dtos;
using Zadanie.API.Models;

namespace Zadanie.API.Data
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Events, AddEventDto>();
            CreateMap<User, AddUserDto>();
            CreateMap<User, UserDto>();
            CreateMap<Events, EventDto>();
        }
    }
}