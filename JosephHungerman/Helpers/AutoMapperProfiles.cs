using AutoMapper;
using JosephHungerman.Models;
using JosephHungerman.Models.Dtos;

namespace JosephHungerman.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Message, MessageDto>().ReverseMap();
        }
    }
}
