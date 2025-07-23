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
        public bool IsCompleted { get; set; }
        public int TechnicianId {  get; set; }
        public int RequestId { get; set; }
        public RequestState NewStatus { get; set; } // Accept / Reject
        public int Pin {  get; set; }
    }
}
