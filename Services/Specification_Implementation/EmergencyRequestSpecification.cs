using Domain.Entities.CoreEntites.EmergencyEntities;
using Services.Specification_Implementation;
using SharedData.Enums;
using SharedData.QueryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specification_Implementation
{
    public class EmergencyRequestSpecification : Specification<EmergencyRequest, int>
    {
        public EmergencyRequestSpecification(RequestQueryData requestQueryData)
            : base(r =>
            (requestQueryData.CallState.HasValue || requestQueryData.TechnicainId.HasValue))

        {
            AddInclude(r => r.CarOwner);
            AddInclude(r => r.category);
            AddInclude(r => r.Technician);
        }
    }
}
