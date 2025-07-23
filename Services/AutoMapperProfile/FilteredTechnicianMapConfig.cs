using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities.CoreEntites.EmergencyEntities;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Service.HelperMethods;
using SharedData.DTOs;
using SharedData.DTOs.ReviewsDTOs;
using SharedData.DTOs.TechnicianDTOs;

namespace Service.AutoMapperProfile
{
    public class FilteredTechnicianMapConfig : Profile
    {
        public FilteredTechnicianMapConfig()
        {
            CreateMap<Technician, FilteredTechniciansDTO>()
            .ForMember(dest => dest.TCategories, opt => opt.MapFrom(src => src.TCategories))
            .ForMember(dest => dest.reviews, opt => opt.MapFrom(src => new ReviewDTO())) 
            .AfterMap((src, dest) =>
            {
                dest.StartWorking = src.StartWorking.ToString();
                dest.EndWorking = src.EndWorking.ToString();
                dest.FaceImageURL = src.ApplicationUser.FaceImageUrl;
                dest.Name = src.ApplicationUser.Name;

                if (src.reviews != null && src.reviews.Any())
                {
                    dest.reviews.Rate = HelpingMethods.GetAverage(src.reviews.Select(r => r.Rate));
                    dest.reviews.Count = src.reviews.Count;
                }
                else
                {
                    dest.reviews.Rate = 0;
                    dest.reviews.Count = 0;
                }
            });

        }
    }
}
