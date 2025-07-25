using Domain.Entities.CoreEntites.EmergencyEntities;
using Services.Specification_Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specification_Implementation
{
    public class EmergencyRequestWithTechnicianLinkSpec : Specification<EmergencyRequest, int>
    {
        public EmergencyRequestWithTechnicianLinkSpec(int requestId) : base(req => req.Id == requestId)
        {
            AddInclude(req => req.EmergencyRequestTechnicians);
            AddInclude(req => req.TechReverseRequests);


        }
    }
}
