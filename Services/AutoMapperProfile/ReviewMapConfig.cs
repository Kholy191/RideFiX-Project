using AutoMapper;
using Domain.Entities.CoreEntites.EmergencyEntities;
using SharedData.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.AutoMapperProfile
{
    public class ReviewMapConfig :Profile
    {
        public ReviewMapConfig()
        {
            CreateMap<Review, ReviewDTO>();
        }
    }
}
