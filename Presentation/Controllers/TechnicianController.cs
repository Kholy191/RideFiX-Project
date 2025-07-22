using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.PaginatedModel;
using SharedData.DTOs.RequestsDTOs;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TechnicianController : ControllerBase
    {
        private readonly IServiceManager serviceManager;
        public TechnicianController(IServiceManager _serviceManager)
        {
            serviceManager = _serviceManager;
        }

        [HttpPost]
        public  async Task<IActionResult> GetFilteredTechnicians([FromBody] CreatePreRequestDTO request)
        {
           var result = await serviceManager.requestServices.CreateRequestAsync(request);
            if (result == null || result.Technicians == null || !result.Technicians.Any())
            {
                return NotFound("No technicians found matching the criteria.");
            }
            return Ok(result.Technicians);
        }

    }
}
