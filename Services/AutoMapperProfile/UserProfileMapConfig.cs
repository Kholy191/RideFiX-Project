using AutoMapper;
using Domain.Entities.CoreEntites.EmergencyEntities;
using SharedData.DTOs.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.AutoMapperProfile
{
    public class UserProfileMapConfig : Profile
    {
        public UserProfileMapConfig()
        {

            CreateMap<ReadUserDetailsDTO, Technician>()
              .ForMember(dest => dest.ApplicationUser.FaceImageUrl, opt => opt.MapFrom(src => src.FaceImageUrl))
              .ForMember(dest => dest.ApplicationUser.Name, opt => opt.MapFrom(src => src.Name))
              .ForMember(dest => dest.ApplicationUser.Email, opt => opt.MapFrom(src => src.Email))
              .ReverseMap();
        }
    }
}
