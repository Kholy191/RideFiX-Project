using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.CoreEntites.EmergencyEntities
{
   public class EmergencyRequest : BaseEntity<int>
    {
        public string CallState { get; set; }
        public bool IsCompleted { get; set; }

        public DateTime TimeStamp { get; set; }
        public DateTime? EndTimeStamp { get; set; }

        //navigations
        public Technician Technician { get; set; }
        public CarOwner CarOwner { get; set; }
    }
}
