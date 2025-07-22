﻿
using Domain.Entities.CoreEntites.EmergencyEntities;
using Services.Specification_Implementation;
using SharedData.DTOs.RequestsDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specification_Implementation
{
    public class CarOwnerSpecification : Specification<CarOwner, int>
    {
        public CarOwnerSpecification(CreatePreRequestDTO request)
            : base(co => co.Id == request.CarOwnerId)
        {
            AddInclude(co => co.ApplicationUser);
        }
    }

}
