using Domain.Entities.CoreEntites.EmergencyEntities;
using Services.Specification_Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specification_Implementation
{
    internal class EmergencyRequestDetailsByIdSpecification : Specification<EmergencyRequest, int>

    {
        public EmergencyRequestDetailsByIdSpecification(int requestId) : base(req => req.Id == requestId)
        {
            AddInclude(req => req.CarOwner.ApplicationUser);
        }
    }
}
