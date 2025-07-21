using SharedData.DTOs.TechnicianEmergencyRequestDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction.CoreServicesAbstractions
{
    public interface ITechnicianRequestEmergency
    {
        
        public Task<EmergencyRequestDetailsDTO> GetRequestDetailsByIdAsync(int id); // to show reguest details
        public Task<List<EmergencyRequestDetailsDTO>> GetAllAcceptedRequestsAsync(int technicianId); // for history
        public Task<List<EmergencyRequestDetailsDTO>> GetAllActiveRequestsAsync();
        public Task<bool> UpdateRequestFromCarOwnerAsync(TechnicianUpdateEmergencyRequestDTO emergencyRequestDTO); // accept or reject
        public Task<bool> ApplyRequestFromHomePage( TechnicianApplyEmergencyRequestDTO emergencyRequestDTO); 



    }
}
