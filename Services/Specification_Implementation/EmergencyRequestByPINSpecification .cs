using Domain.Entities.CoreEntites.EmergencyEntities;
using Services.Specification_Implementation;
using SharedData.DTOs.RequestsDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specification_Implementation
{
    public class EmergencyRequestByPINSpecification : Specification<EmergencyRequest, int>
    {
        public EmergencyRequestByPINSpecification(CreatePreRequestDTO request) : base(r => r.CarOwner.ApplicationUser.PIN == request.PIN)
        {
            AddInclude(r => r.CarOwner);
            AddInclude(r => r.CarOwner.ApplicationUser);

        }
    }
}
