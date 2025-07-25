using AutoMapper;
using Domain.Entities.CoreEntites.EmergencyEntities;
using SharedData.DTOs.RequestsDTOs;
using SharedData.DTOs.TechnicianEmergencyRequestDTOs;

namespace Service.AutoMapperProfile
{
    public class PreRequestMapConfig : Profile
    {
        public PreRequestMapConfig()
        {

            CreateMap<CreatePreRequestDTO, EmergencyRequest>().ReverseMap();


            //CreateMap<CreatePreRequestDTO, EmergencyRequest>()
            //    .ForMember(des => des.CarOwner.ApplicationUser.PIN, opt => opt.MapFrom(src => src.PIN));
            CreateMap<EmergencyRequest, EmergencyRequestDetailsDTO>().
                ForMember(des => des.RequestId, opt => opt.MapFrom(src => src.Id)).
               
                ForMember(des => des.FaceImageUrl, opt => opt.MapFrom(src => src.CarOwner.ApplicationUser.FaceImageUrl)).
                ForMember(des => des.CarOwnerName, opt => opt.MapFrom(src => src.CarOwner.ApplicationUser.Name))
                .ReverseMap();

            CreateMap<TechnicianUpdateEmergencyRequestDTO, EmergencyRequest>().
                ForMember(des => des.Id, opt => opt.MapFrom(src => src.RequestId)).
                ForMember(des => des.CallState, opt => opt.MapFrom(src => src.NewStatus)).
                AfterMap((src, dest) =>
                {
                    if (dest.Technician?.ApplicationUser != null)
                    {
                        dest.Technician.ApplicationUser.PIN = src.Pin;
                    }
                })
         .ReverseMap();


            //CreateMap<IEnumerable<EmergencyRequest>, List<EmergencyRequestDetailsDTO>>().
            //    ForMember(des => des.RequestId, opt => opt.MapFrom(src => src.Id));

        }


    }
}
