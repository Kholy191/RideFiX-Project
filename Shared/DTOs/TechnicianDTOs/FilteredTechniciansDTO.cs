using SharedData.DTOs.ReviewsDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedData.DTOs.TechnicianDTOs
{
    public class FilteredTechniciansDTO
    {
        public string Name { get; set; }
        public string FaceImageURL { get; set; }
        public string StartWorking { get; set; }
        public string EndWorking { get; set; }
        public string Description { get; set; }
        public ICollection<TCategoryDTO> TCategories { get; set; } = new List<TCategoryDTO>();
        public ReviewDTO reviews { get; set; } 
    }
}
