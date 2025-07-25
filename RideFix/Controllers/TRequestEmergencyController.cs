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

        [HttpGet("details/{requestId}/{technicianId}")]
        [EndpointSummary("Get emegencyRequestdetails by id")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<EmergencyRequestDetailsDTO>))]
        [ProducesResponseType(404, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> GetRequestDetailsAsync(int requestId, int technicianId)
        {
            var request = await serviceManager.technicianRequestEmergency.GetRequestDetailsByIdAsync(requestId, technicianId);

            if (request == null)
                return NotFound("Request details not found for this technician and request");

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
        [HttpGet("active/{technicianId}")]

        public async Task<ActionResult<EmergencyRequestDetailsDTO>> GetAllActiveedRequests(int technicalId)
        {
            try
            {
                var request = await serviceManager.technicianRequestEmergency.GetAllActiveRequestsAsync(technicalId);
                int x = 9;
                if (request == null)
                    return NotFound(ApiResponse<string>.FailResponse("technician doesn't have requests"));
                return Ok(ApiResponse<List<EmergencyRequestDetailsDTO>>.SuccessResponse(request, "technician have requests"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.FailResponse($"An error occurred: {ex.Message}"));
            }
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

        [HttpPost]
        [EndpointSummary("Apply request from technical home page")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> ApplyRequestFromHomePage([FromBody] TechnicianApplyEmergencyRequestDTO dto)
        {
            var result = await serviceManager.technicianRequestEmergency.ApplyRequestFromHomePage(dto);
            if (!result)
                return BadRequest(ApiResponse<string>.FailResponse("Invalid technician, PIN, or request"));

            return Ok(ApiResponse<bool>.SuccessResponse(true, "Added successfully"));
        }

    }
}
