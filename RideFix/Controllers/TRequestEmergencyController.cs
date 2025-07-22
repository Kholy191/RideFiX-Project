using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using SharedData.DTOs.TechnicianEmergencyRequestDTOs;

namespace RideFix.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TRequestEmergency : ControllerBase
    {
        private readonly IServiceManager serviceManager;
        public TRequestEmergency(IServiceManager _iServiceManager)
        {
            serviceManager = _iServiceManager;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<EmergencyRequestDetailsDTO>> GetDetailsAsync(int id)
        {
            var request = await serviceManager.technicianRequestEmergency.GetRequestDetailsByIdAsync(id);
            if (request == null)
                return NotFound("request with id not found y s7by");
            return Ok(request);
        }
     
        [HttpGet("accepted/{technicalId}")]
        public async Task<ActionResult<EmergencyRequestDetailsDTO>> GetAllAcceptedRequests(int technicalId)
        {
            var request = await serviceManager.technicianRequestEmergency.GetAllAcceptedRequestsAsync(technicalId);
            if (request == null)
                return NotFound("request with id not found y s7by");
            return Ok(request);
        }


    }
}
