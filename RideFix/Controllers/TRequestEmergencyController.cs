using Microsoft.AspNetCore.Mvc;
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
        [HttpGet("{id}")]
        [EndpointSummary("Get emegencyRequestdetails by id")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<EmergencyRequestDetailsDTO>))]
        [ProducesResponseType(404,Type=typeof(ApiResponse<string>))]
        public async Task<IActionResult> GetRequestDetailsAsync(int id)
        {
            var request = await serviceManager.technicianRequestEmergency.GetRequestDetailsByIdAsync(id);

            if (request == null)
                return NotFound("test");

            return Ok(ApiResponse<EmergencyRequestDetailsDTO>.SuccessResponse(request, "request details found"));
        }



    }
}
