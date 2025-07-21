using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedData.DTOs.TechnicianDTOs
{
    public class FilteredTechniciansDTO
    {
        public TimeOnly StartWorking { get; set; }
        public TimeOnly EndWorking { get; set; }
        public string Description { get; set; }
        public ICollection<TCategoryDTO> TCategories { get; set; } = new List<TCategoryDTO>();
        public ICollection<ReviewDTO> reviews { get; set; } = new List<ReviewDTO>();

    }
}
