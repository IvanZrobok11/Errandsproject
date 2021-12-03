using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Errands.Domain.Models;

namespace Errands.Application.Common.Services
{
    public class ServiceMappingProfile : Profile
    {
        public ServiceMappingProfile()
        {
            this.CreateMap<Errand, GetErrandToDoServiceModel>()
               
                .ForMember(errandDto => errandDto.NeedlyUserId,
                    opt => opt.MapFrom(errand => errand.UserId));

            this.CreateMap<Errand, GetMyErrandServiceModel>();

            this.CreateMap<Errand, CreateErrandModel>()
                .ForMember(errandDto => errandDto.Desc,
                    opt => opt.MapFrom(errand => errand.Description))
                .ForMember(errandDto => errandDto.Title,
                    opt => opt.MapFrom(errand => errand.Title))
                .ForMember(errandDto => errandDto.Cost,
                    opt => opt.MapFrom(errand => errand.Cost))
                .ForMember(errandDto => errandDto.Files,
                    opt => opt.Ignore())
                .ReverseMap()
                ;

            this.CreateMap<Errand, EditErrandModel>()
                .ForMember(errandDto => errandDto.Desc,
                    opt => opt.MapFrom(errand => errand.Description))
                .ForMember(errandDto => errandDto.File,
                    opt => opt.MapFrom(errand => errand.FileModels))
                .ReverseMap();

            this.CreateMap<Errand, ListErrandsServiceModel>()
                .ForMember(errandDto => errandDto.FileModels,
                    opt => opt.MapFrom(errand => errand.FileModels))
                .ReverseMap();


        }
    }
}
