using SharedData.DTOs.TechnicianEmergencyRequestDTOs;

namespace ServiceAbstraction.CoreServicesAbstractions
{
    public interface ITechnicianRequestEmergency
    {

        Task<EmergencyRequestDetailsDTO> GetRequestDetailsByIdAsync(int id); // to show reguest details
        Task<List<EmergencyRequestDetailsDTO>> GetAllAcceptedRequestsAsync(int technicianId); // for history
        Task<List<EmergencyRequestDetailsDTO>> GetAllActiveRequestsAsync(); // to apply this request from home page
        Task<bool> UpdateRequestFromCarOwnerAsync(TechnicianUpdateEmergencyRequestDTO emergencyRequestDTO); // accept or reject
        Task<bool> ApplyRequestFromHomePage(TechnicianApplyEmergencyRequestDTO emergencyRequestDTO);
        Task<List<EmergencyRequestDetailsDTO>> GetAllRequestsAssignedToTechnicianAsync(int technicianId);




    }
}
