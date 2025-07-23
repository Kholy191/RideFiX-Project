using AutoMapper;
using Domain.Contracts;
using Domain.Entities.CoreEntites.EmergencyEntities;
using Service.Specification_Implementation;
using ServiceAbstraction.CoreServicesAbstractions;
using SharedData.DTOs.TechnicianEmergencyRequestDTOs;
using SharedData.Enums;
using SharedData.QueryModel;

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
            var technician = await unitOfWork.GetRepository<Technician, int>().GetByIdAsync(technicianId);
            if (technician == null)
                return new List<EmergencyRequestDetailsDTO>();
            var spec = new RequestsAssignedToTechnicianSpecification(technicianId, RequestState.Waiting);

            var requests = await unitOfWork
                .GetRepository<EmergencyRequest, int>()
                .GetAllAsync(spec);

            return mapper.Map<List<EmergencyRequestDetailsDTO>>(requests);
        }

        public async Task<EmergencyRequestDetailsDTO> GetRequestDetailsByIdAsync(int id)
        {
            var spec= new EmergencyRequestDetailsByIdSpecification(id);
            var request = await unitOfWork.GetRepository<EmergencyRequest, int>().GetByIdAsync(spec);
            return mapper.Map<EmergencyRequestDetailsDTO>(request);
        }

        public async Task<bool> UpdateRequestFromCarOwnerAsync(TechnicianUpdateEmergencyRequestDTO emergencyRequestDTO)
        {
            var spec = new TechnicianUpdateRequestSpec(emergencyRequestDTO.RequestId, emergencyRequestDTO.TechnicianId);
            var request = await unitOfWork.GetRepository<EmergencyRequest, int>().GetByIdAsync(spec);
            if (request == null) return false;
            var technicianSpec = new TechnicianWithAppUserSpec(emergencyRequestDTO.TechnicianId, emergencyRequestDTO.Pin);
            var technician = await unitOfWork.GetRepository<Technician, int>().GetByIdAsync(technicianSpec);
            if (technician == null) return false;
            request.CallState = emergencyRequestDTO.NewStatus;
            request.IsCompleted = emergencyRequestDTO.IsCompleted;
            await unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
