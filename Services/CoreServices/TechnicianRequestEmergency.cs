using AutoMapper;
using Domain.Contracts;
using Domain.Entities.CoreEntites.EmergencyEntities;
using ServiceAbstraction.CoreServicesAbstractions;
using SharedData.DTOs.TechnicianEmergencyRequestDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.CoreServices.TechniciansServices
{
    internal class TechnicianRequestEmergency : ITechnicianRequestEmergency
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public TechnicianRequestEmergency(IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }

        public Task<bool> ApplyRequestFromHomePage(TechnicianApplyEmergencyRequestDTO emergencyRequestDTO)
        {
            throw new NotImplementedException();
        }

        public Task<List<EmergencyRequestDetailsDTO>> GetAllAcceptedRequestsAsync(int technicianId)
        {
            throw new NotImplementedException();
        }

        public Task<List<EmergencyRequestDetailsDTO>> GetAllActiveRequestsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<EmergencyRequestDetailsDTO>> GetAllRequestsAssignedToTechnicianAsync(int technicianId)
        {
            throw new NotImplementedException();
        }

        public async Task<EmergencyRequestDetailsDTO> GetRequestDetailsByIdAsync(int id)
        {
            var request = await unitOfWork.GetRepository<EmergencyRequest, int>().GetByIdAsync(id);
            return mapper.Map<EmergencyRequestDetailsDTO>(request);
        }

        public Task<bool> UpdateRequestFromCarOwnerAsync(TechnicianUpdateEmergencyRequestDTO emergencyRequestDTO)
        {
            throw new NotImplementedException();
        }
    }
}
