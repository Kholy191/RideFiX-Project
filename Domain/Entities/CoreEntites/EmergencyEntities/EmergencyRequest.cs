using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedData.Enums;

namespace Domain.Entities.CoreEntites.EmergencyEntities
{
    public class EmergencyRequest : BaseEntity<int>
    {
        public bool IsCompleted { get; set; }
        public DateTime TimeStamp { get; set; }
        public DateTime? EndTimeStamp { get; set; }
        public string Description { get; set; }
        public Double Latitude { get; set; }
        public Double Longitude { get; set; }
        [ForeignKey("category")]
        public int categoryId { get; set; }

        //navigations
        public int TechnicainId { get; set; }
        public Technician Technician { get; set; }
        public ICollection<EmergencyRequestTechnicians> EmergencyRequestTechnicians { get; set; } = new HashSet<EmergencyRequestTechnicians>();


        //when car owner create request he can select multiple technicians
        public ICollection<Technician> Technicians { get; set; } = new HashSet<Technician>();
        // this is for technician to make reverse request to car owner
        public ICollection<TechReverseRequest> TechReverseRequests { get; set; } = new HashSet<TechReverseRequest>();   
        public int CarOwnerId { get; set; }
        public CarOwner CarOwner { get; set; }
        public ICollection<RequestAttachment>? requestAttachments { get; set; } = new HashSet<RequestAttachment>();
        public TCategory category { get; set; }
    }
}
