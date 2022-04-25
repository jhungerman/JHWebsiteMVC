using AutoMapper;
using JosephHungerman.Data.Models;
using JosephHungerman.Services.Models.Dtos;

namespace JosephHungerman.Services.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Message, MessageDto>().ReverseMap();
        }
    }
}
