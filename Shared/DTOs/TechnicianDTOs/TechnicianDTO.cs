using SharedData.DTOs.ReviewsDTOs;
using SharedData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedData.DTOs.TechnicianDTOs
{
    public class TechnicianDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public TimeOnly StartWorking { get; set; }
        public TimeOnly EndWorking { get; set; }
        public string Description { get; set; }
        public string FaceImageUrl { get; set; }
        public HashSet<TCategoryDTO> Categories { get; set; } = new HashSet<TCategoryDTO>();
        public List<ReviewDetailsDTO> Reviews { get; set; } = new List<ReviewDetailsDTO>();
        public string government { get; set; }
    }
}
