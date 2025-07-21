using SharedData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedData.DTOs.TechnicianEmergencyRequestDTOs
{
    public class TechnicianUpdateEmergencyRequestDTO
    {
        public int RequestId { get; set; }
        public RequestState NewStatus { get; set; } // Accept / Reject
    }
}
