using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.CoreEntites.EmergencyEntities
{
    public class MessageAttachment
    {
        public string AttachmentUrl { get; set; }

        [ForeignKey("Message")]
        public int MessageId { get; set; }
        // Navigational Prop
        public Message Message { get; set; }
    }
}
