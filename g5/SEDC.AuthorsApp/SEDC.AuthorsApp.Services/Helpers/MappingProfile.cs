using AutoMapper;
using SEDC.AuthorsApp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.AuthorsApp.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Author, AuthorDTO>()
                .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))
                .ForMember(dest => dest.ID, src => src.MapFrom(x => x.ID))
                .ForMember(dest => dest.DateOfBirth, src => src.MapFrom(x => x.DateOfBirth.ToString("yyyy/M/dd")))
                .ForMember(dest => dest.DateOfDeath, src => src.MapFrom(x => x.DateOfDeath == null ? "" : x.DateOfDeath.Value.ToString("yyyy/M/dd")))
                .ForMember(dest => dest.Novels, src => src.MapFrom(x => x.Novels))
                .ReverseMap()
                .ForMember(dest => dest.DateOfBirth, src => src.MapFrom(x => DateTime.Parse(x.DateOfBirth)))
                .ForMember(dest => dest.DateOfDeath, src => src.MapFrom(x => x.DateOfDeath == "" ? (DateTime?)null : DateTime.Parse(x.DateOfDeath)))
                .IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Nomination, NominationDTO>()
                .ForMember(dest => dest.ID, src => src.MapFrom(x => x.ID))
                .ForMember(dest => dest.Award, src => src.MapFrom(x => x.Award.ToString()))
                .ForMember(dest => dest.Book, src => src.MapFrom(x => x.Book))
                .ForMember(dest => dest.IsWinner, src => src.MapFrom(x => x.IsWinner))
                .ForMember(dest => dest.YearNominated, src => src.MapFrom(x => x.YearNominated))
                .ReverseMap()
                .ForMember(dest => dest.Award, src => src.MapFrom(x => Helper.GetAwardEnum(x.Award)))
                .IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Novel, NovelDTO>()
                .ForMember(dest => dest.ID, src => src.MapFrom(x => x.ID))
                .ForMember(dest => dest.Title, src => src.MapFrom(x => x.Title))
                .ForMember(dest => dest.Nominations, src => src.MapFrom(x => x.Nominations))
                .ForMember(dest => dest.IsRead, src => src.MapFrom(x => x.IsRead))
                .ForMember(dest => dest.Author, src => src.MapFrom(x => x.Author))
                .ReverseMap()
                .IgnoreAllPropertiesWithAnInaccessibleSetter();
        }
    }
}
