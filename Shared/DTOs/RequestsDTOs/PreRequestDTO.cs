using SharedData.DTOs.TechnicianDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedData.DTOs.RequestsDTOs
{
    public class PreRequestDTO
    {
        public List<FilteredTechniciansDTO> Technicians { get; set; }

    }
}
