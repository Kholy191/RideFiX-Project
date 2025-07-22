using AutoMapper;
using Domain.Entities.CoreEntites.EmergencyEntities;
using SharedData.DTOs.RequestsDTOs;
using SharedData.DTOs.TechnicianEmergencyRequestDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.AutoMapperProfile
{
    public class PreRequestMapConfig : Profile
    {
        public PreRequestMapConfig()
        {

            CreateMap<CreatePreRequestDTO, EmergencyRequest>()
            .AfterMap((src, dest) =>
            {
                if (dest.CarOwner?.ApplicationUser != null)
                {
                    dest.CarOwner.ApplicationUser.PIN = src.PIN;
                }
            }).ReverseMap();


            //CreateMap<CreatePreRequestDTO, EmergencyRequest>()
            //    .ForMember(des => des.CarOwner.ApplicationUser.PIN, opt => opt.MapFrom(src => src.PIN));
            CreateMap<EmergencyRequest, EmergencyRequestDetailsDTO>().
                ForMember(des => des.RequestId, opt => opt.MapFrom(src => src.Id));
            CreateMap<EmergencyRequest, EmergencyRequestDetailsDTO>()
    .ForMember(dest => dest.RequestId, opt => opt.MapFrom(src => src.Id));
            //CreateMap<IEnumerable<EmergencyRequest>, List<EmergencyRequestDetailsDTO>>().
            //    ForMember(des => des.RequestId, opt => opt.MapFrom(src => src.Id));

        }


    }
}
