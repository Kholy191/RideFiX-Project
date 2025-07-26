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

            CreateMap<Technician, ReadUserDetailsDTO>()
              .ForMember(dest => dest.FaceImageUrl, opt => opt.MapFrom(src => src.ApplicationUser.FaceImageUrl))
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ApplicationUser.Name))
              .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.ApplicationUser.Email));
             
        }
    }
}
