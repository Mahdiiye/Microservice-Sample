using AutoMapper;
using CommandsService.Dtos;
using CommandsService.Models;

namespace CommandsService.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            //Source -> Target
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<Command, CommandReadDto>();
            CreateMap<CommandCreateDto, Command>();
            CreateMap<PlatformPublishedDto, Platform>()
                //برای مپ کردن ExternalID 
                .ForMember(dest => dest.ExternalID, opt => opt.MapFrom(src => src.Id));
        }
    }
}
