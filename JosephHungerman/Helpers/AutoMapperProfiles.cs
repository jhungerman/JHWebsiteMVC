using AutoMapper;
using JosephHungerman.Models;
using JosephHungerman.Models.Contact;
using JosephHungerman.Models.Dtos;
using JosephHungerman.Models.Dtos.Contact;

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
