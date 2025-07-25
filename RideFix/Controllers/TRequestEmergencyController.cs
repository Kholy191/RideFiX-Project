using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Presistence.unitofwork;
using ServiceAbstraction;
using SharedData.DTOs.TechnicianEmergencyRequestDTOs;
using SharedData.Wrapper;

namespace RideFix.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TRequestEmergencyController : ControllerBase
    {
        private readonly IServiceManager serviceManager;
        public TRequestEmergencyController(IServiceManager _iServiceManager)
        {
            serviceManager = _iServiceManager;
        }
        [HttpGet]
        [EndpointSummary("Get emegencyRequestdetails by id")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<EmergencyRequestDetailsDTO>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> GetRequestDetailsAsync(int id)
        {
            var request = await serviceManager.technicianRequestEmergency.GetRequestDetailsByIdAsync(id);

            if (request == null)
                return NotFound("requestDetails not found with this id");

            return Ok(ApiResponse<EmergencyRequestDetailsDTO>.SuccessResponse(request, "request details found"));
        }
        [HttpGet("accepted/id")]
        [HttpGet("accepted/{technicalId}")]
        public async Task<ActionResult<EmergencyRequestDetailsDTO>> GetAllAcceptedRequests(int technicalId)
        {
            var request = await serviceManager.technicianRequestEmergency.GetAllAcceptedRequestsAsync(technicalId);
            if (request == null)
                return NotFound(ApiResponse<string>.FailResponse("technician doesn't have requests"));
            return Ok(ApiResponse<List<EmergencyRequestDetailsDTO>>.SuccessResponse(request, "technician have requests"));
        }
        [HttpGet("active")]
       
        public async Task<ActionResult<EmergencyRequestDetailsDTO>> GetAllActiveedRequests()
        {
            var request = await serviceManager.technicianRequestEmergency.GetAllActiveRequestsAsync();
            if (request == null)
                return NotFound(ApiResponse<string>.FailResponse("technician doesn't have requests"));
            return Ok(ApiResponse<List<EmergencyRequestDetailsDTO>>.SuccessResponse(request, "technician have requests"));
        }

        [EndpointSummary("Get RequestsAssignedToTechnician by technicianId if it's waiting state ")]
        [HttpGet("assigned/{technicianId}")]
        public async Task<IActionResult> GetAllRequestsAssignedToTechnician(int technicianId)
        {
            /*
             to do:
            validate if technicianId found or not 
             */
           
            var request = await serviceManager.technicianRequestEmergency.GetAllRequestsAssignedToTechnicianAsync(technicianId);
            if (request == null || !request.Any())
                return NotFound(ApiResponse<string>.FailResponse("technician doesn't have requests"));
            return Ok(ApiResponse<List<EmergencyRequestDetailsDTO>>.SuccessResponse(request, "technician have requests"));
        }
        [HttpPut]
        [EndpointSummary("Accept or Reject Emergency Request By Technician")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> UpdateRequestFromCarOwnerAsync([FromBody] TechnicianUpdateEmergencyRequestDTO dto)
        {
            var result = await serviceManager.technicianRequestEmergency.UpdateRequestFromCarOwnerAsync(dto);
            if (!result)
                return BadRequest(ApiResponse<string>.FailResponse("Invalid technician, PIN, or request"));

             return Ok(ApiResponse<bool>.SuccessResponse(true, "Updated successfully"));
        }



    }
}
