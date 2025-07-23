using AutoMapper;
using Domain.Contracts;
using Domain.Entities.CoreEntites.EmergencyEntities;
using Service.Specification_Implementation;
using ServiceAbstraction.CoreServicesAbstractions;
using SharedData.DTOs.TechnicianEmergencyRequestDTOs;
using SharedData.Enums;
using SharedData.QueryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.CoreServices.TechniciansServices
{
    public class TechnicianRequestEmergency : ITechnicianRequestEmergency
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

        //public async Task<bool> ApplyRequestFromHomePage(TechnicianApplyEmergencyRequestDTO emergencyRequestDTO)
        //{
        //    var repo = unitOfWork.GetRepository<EmergencyRequest, int>();
        //    EmergencyRequest requestToUpdate =await repo.GetByIdAsync(emergencyRequestDTO.RequestId);
        //    if (requestToUpdate != null)
        //    {
        //        requestToUpdate.TechnicainId = emergencyRequestDTO.UserId;
        //        requestToUpdate.s
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //}

        public async Task<List<EmergencyRequestDetailsDTO>> GetAllAcceptedRequestsAsync(int tecId)
        {
            var repo = unitOfWork.GetRepository<EmergencyRequest, int>();
            var allRequests = await repo.GetAllAsync(new EmergencyRequestSpecification(new RequestQueryData() { TechnicainId = tecId, CallState = RequestState.Answered }));
            return mapper.Map<List<EmergencyRequestDetailsDTO>>(allRequests);
        }

        public async Task<List<EmergencyRequestDetailsDTO>> GetAllActiveRequestsAsync()
        {
            var repo = unitOfWork.GetRepository<EmergencyRequest, int>();
            var allRequests = await repo.GetAllAsync(new EmergencyRequestSpecification(new RequestQueryData() { IsCompleted = false }));
            return mapper.Map<List<EmergencyRequestDetailsDTO>>(allRequests);

        }

        public async Task<List<EmergencyRequestDetailsDTO>> GetAllRequestsAssignedToTechnicianAsync(int technicianId)
        {
            var spec = new RequestsAssignedToTechnicianSpecification(technicianId, RequestState.Waiting);

            var requests = await unitOfWork
                .GetRepository<EmergencyRequest, int>()
                .GetAllAsync(spec);

            return mapper.Map<List<EmergencyRequestDetailsDTO>>(requests);
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
