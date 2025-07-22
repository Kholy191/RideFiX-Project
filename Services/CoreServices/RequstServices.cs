using AutoMapper;
using Domain.Contracts;
using Domain.Entities.CoreEntites.EmergencyEntities;
using Service.Specification_Implementation;
using ServiceAbstraction.CoreServicesAbstractions;
using SharedData.DTOs.RequestsDTOs;
using SharedData.DTOs.TechnicianDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.CoreServices
{
    public class RequstServices : IRequestServices
    {
        private readonly ITechnicianService technicianService;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public RequstServices(IUnitOfWork _unitOfWork, IMapper _mapper, ITechnicianService technicianService)
        {

            unitOfWork = _unitOfWork;
            mapper = _mapper;
            this.technicianService = technicianService;
        }
        public async Task<PreRequestDTO> CreateRequestAsync(CreatePreRequestDTO request)
        {
            
            var spec = new EmergencyRequestByPINSpecification(request);
            var user = await unitOfWork.GetRepository<EmergencyRequest, int>().GetByIdAsync(spec);

            if (user == null)
            {
                throw new ArgumentException("Car Owner not found");
            }
            else if (user.CarOwner.ApplicationUser.PIN != request.PIN)
            {
                throw new ArgumentException("Invalid PIN provided for the Car Owner");
            }
            var filteredTechnicians = await technicianService.GetTechniciansByFilterAsync(request);
            if (filteredTechnicians == null || !filteredTechnicians.Any())
            {
                return new PreRequestDTO { };
            }
            
            return new PreRequestDTO { Technicians = filteredTechnicians };

        }
    }
}
