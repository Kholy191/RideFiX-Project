using Domain.Entities.CoreEntites.EmergencyEntities;

using Services.Specification_Implementation;
using SharedData.Enums;
namespace Service.Specification_Implementation;
public class TechnicianUpdateRequestSpec : Specification<EmergencyRequest, int>
{
    public TechnicianUpdateRequestSpec(int requestId, int technicianId)
        : base(r =>
            r.Id == requestId &&
            r.TechnicainId == technicianId &&
            r.CallState == RequestState.Waiting)
    {
    }
}
