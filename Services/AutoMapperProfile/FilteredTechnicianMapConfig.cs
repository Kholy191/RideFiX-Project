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
            CreateMap<Technician, FilteredTechniciansDTO>()
            .AfterMap((src, dest) => {
                dest.StartWorking = src.StartWorking.ToString();
                dest.EndWorking = src.EndWorking.ToString();
                dest.FaceImageURL = src.ApplicationUser.FaceImageUrl;
                dest.Name = src.ApplicationUser.Name;
            });
        }
    }
}
