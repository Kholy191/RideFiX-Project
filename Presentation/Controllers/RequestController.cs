using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using SharedData.DTOs.RequestsDTOs;
using SharedData.DTOs.TechnicianDTOs;
using SharedData.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RequestController : ControllerBase
    {
        private readonly IServiceManager serviceManager;
        public RequestController(IServiceManager _serviceManager)
        {
            serviceManager = _serviceManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRequest(RealRequestDTO request)
        {
            if (request == null)
            {
                return BadRequest("Request data cannot be null.");
            }
            await serviceManager.requestServices.CreateRealRequest(request);
            return Ok(ApiResponse<string>.SuccessResponse(null, "requests succesfully created"));
        }
    }
}
