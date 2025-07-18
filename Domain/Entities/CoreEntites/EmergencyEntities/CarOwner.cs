using Domain.Entities.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.CoreEntites.EmergencyEntities
{
  public  class CarOwner : IcanOrder
    {
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<ChatSession> ChatSessions { get; set; } = new List<ChatSession>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<EmergencyRequest> EmergencyRequests { get; set; } = new List<EmergencyRequest>();

    }
}
