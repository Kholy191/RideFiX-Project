using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedData.Enums;

namespace Domain.Entities.CoreEntites.EmergencyEntities
{
   public class EmergencyRequest : BaseEntity<int>
    {
        public RequestState CallState { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime TimeStamp { get; set; }
        public DateTime? EndTimeStamp { get; set; }
        public string Description { get; set; } 
        public Double Latitude { get; set; }
        public Double Longitude { get; set; }

        //navigations
        public int TechnicainId { get; set; }
        public Technician Technician { get; set; }
        public int CarOwnerId { get; set; }
        public CarOwner CarOwner { get; set; }
        public ICollection<RequestAttachment>? requestAttachments { get; set; } = new HashSet<RequestAttachment>();
    }
}
