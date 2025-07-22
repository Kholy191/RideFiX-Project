using AutoMapper;
using Domain.Entities.CoreEntites.EmergencyEntities;
using SharedData.DTOs.TechnicianDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.AutoMapperProfile
{
    public class FilteredTechnicianMapConfig : Profile
    {
        public FilteredTechnicianMapConfig()
        {
            CreateMap< FilteredTechniciansDTO , Technician>()
                .AfterMap((src, dest) => {
                    src.StartWorking = dest.StartWorking.ToString();
                    src.EndWorking = dest.EndWorking.ToString();
                    src.FaceImageURL = dest.ApplicationUser.FaceImageUrl;
                    src.Name = dest.ApplicationUser.Name;
                }).ReverseMap();


        }
    }
}
