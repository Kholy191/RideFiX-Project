using Domain.Entities.CoreEntites.EmergencyEntities;
using Services.Specification_Implementation;
using SharedData.Enums;

namespace Service.Specification_Implementation
{
    public class RequestsAssignedToTechnicianSpecification : Specification<EmergencyRequest, int>
    {
        public RequestsAssignedToTechnicianSpecification(int technicianId, RequestState requestState) : base(req =>
                 req.TechnicainId == technicianId && req.CallState == requestState)
        {
           // AddInclude(req => req.CarOwner);
            AddInclude(req => req.CarOwner.ApplicationUser);
            AddInclude(req => req.Technician);
           

            
        }
    }
}
