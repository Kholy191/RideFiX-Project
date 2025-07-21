using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SharedData.DTOs.TechnicianEmergencyRequestDTOs
{
    public class EmergencyRequestDetailsDTO
    {
        public int RequestId { get; set; }
        public string Description { get; set; }
        public int CarOwnerId { get; set; }
    }
}
