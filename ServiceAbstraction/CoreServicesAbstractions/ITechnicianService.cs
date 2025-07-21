using SharedData.DTOs.RequestsDTOs;
using SharedData.DTOs.TechnicianDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction.CoreServicesAbstractions
{
    public interface ITechnicianService
    {
        public Task<List<FilteredTechniciansDTO>> GetTechniciansByFilterAsync(CreatePreRequestDTO request);
        public Task<string> GetCity(double Latitude, double Longitude);
        public Task<List<FilteredTechniciansDTO>> GetAllTechnicians();

    }
}
