using AutoMapper;
using Domain.Entities.CoreEntites.EmergencyEntities;
using SharedData.DTOs.ReviewsDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.AutoMapperProfile
{
    public class ReviewDetailsMapConfig : Profile
    {
        public ReviewDetailsMapConfig() 
        {
            CreateMap<Review, ReviewDetailsDTO>().ReverseMap();
                

        }

    }
}
