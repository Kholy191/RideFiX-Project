using Domain.Entities.CoreEntites.EmergencyEntities;
using Services.Specification_Implementation;

namespace Service.Specification_Implementation
{
    public class EmergencyRequestWithTechnicianLinkSpec : Specification<EmergencyRequest, int>
    {
        public EmergencyRequestWithTechnicianLinkSpec(int requestId) : base(req => req.Id == requestId)
        {
            AddInclude(req => req.EmergencyRequestTechnicians);



        }
    }
}
