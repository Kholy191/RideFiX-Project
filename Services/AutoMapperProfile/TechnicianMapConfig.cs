using AutoMapper;
using Domain.Entities.CoreEntites.EmergencyEntities;
using SharedData.DTOs.TechnicianDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Service.AutoMapperProfile
{
    public class TechnicianMapConfig : Profile
    {
        public TechnicianMapConfig()
        {
            CreateMap<Technician, TechnicianDTO>()
              .ForMember(dest => dest.FaceImageUrl, opt => opt.MapFrom(src => src.ApplicationUser.FaceImageUrl))
              .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.TCategories))
              .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.reviews))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ApplicationUser.Name))
                .ForMember(dest => dest.government, opt => opt.MapFrom(src => src.government.ToString()))
              .ReverseMap();
        }
    }
}
