using AutoMapper;
using SEDC.AuthorsApp.Services;
using SEDC.AuthorsApp.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEDC.AuthorsApp.WebUI.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AuthorDTO, AuthorViewModel>()
                .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))
                .ForMember(dest => dest.ID, src => src.MapFrom(x => x.ID))
                .ForMember(dest => dest.DateOfBirth, src => src.MapFrom(x => x.DateOfBirth))
                .ForMember(dest => dest.DateOfDeath, src => src.MapFrom(x => x.DateOfDeath))
                .ForMember(dest => dest.Novels, src => src.MapFrom(x => x.Novels))
                .ReverseMap()
                .IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<NovelDTO, NovelViewModel>()
                .ForMember(dest => dest.ID, src => src.MapFrom(x => x.ID))
                .ForMember(dest => dest.Title, src => src.MapFrom(x => x.Title))
                .ForMember(dest => dest.IsRead, src => src.MapFrom(x => x.IsRead.ToString()))
                .ReverseMap()
                .ForMember(dest => dest.IsRead, src => src.MapFrom(x => bool.Parse(x.IsRead)))
                .IgnoreAllPropertiesWithAnInaccessibleSetter();
        }
    }
}
