using Domain.Contracts.SpecificationContracts;
using Domain.Entities.CoreEntites.EmergencyEntities;
using Services.Specification_Implementation;
using SharedData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specification_Implementation
{
    public class RequestsAssignedToTechnicianSpecification : Specification<EmergencyRequest, int>
    {
        public RequestsAssignedToTechnicianSpecification(int technicianId, RequestState requestState) : base(req =>
                 req.TechnicainId == technicianId && req.CallState == requestState)
        {
        }
    }
}
