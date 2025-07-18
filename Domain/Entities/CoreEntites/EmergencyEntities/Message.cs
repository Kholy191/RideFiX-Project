using Domain.Entities.IdentityEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.CoreEntites.EmergencyEntities
{
   public class Message : BaseEntity<int>
    {
        public string Text { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsSeen { get; set; }
        [ForeignKey("ChatSession")]
        public int ChatSessionId { get; set; }
        public ChatSession ChatSession { get; set; }

        [ForeignKey("ApplicationUser")]

        public string ApplicationId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

    }
}
