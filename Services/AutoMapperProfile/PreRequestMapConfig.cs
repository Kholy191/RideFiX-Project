using AutoMapper;
using Domain.Entities.CoreEntites.EmergencyEntities;
using SharedData.DTOs.RequestsDTOs;
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
            });

        }


    }
}
