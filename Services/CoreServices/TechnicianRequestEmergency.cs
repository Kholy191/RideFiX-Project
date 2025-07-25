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

        public Task<List<EmergencyRequestDetailsDTO>> GetAllAcceptedRequestsAsync(int technicianId)
        {
            throw new NotImplementedException();
        }

        public Task<List<EmergencyRequestDetailsDTO>> GetAllActiveRequestsAsync()
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

        //public async Task<List<EmergencyRequestDetailsDTO>> GetAllAcceptedRequestsAsync(int tecId)
        //{
        //    var repo = unitOfWork.GetRepository<EmergencyRequest, int>();
        //    var allRequests = await repo.GetAllAsync(new EmergencyRequestSpecification(new RequestQueryData() { TechnicainId = tecId, CallState = RequestState.Answered }));
        //    return mapper.Map<List<EmergencyRequestDetailsDTO>>(allRequests);
        //}

        //public async Task<List<EmergencyRequestDetailsDTO>> GetAllActiveRequestsAsync()
        //{
        //    var repo = unitOfWork.GetRepository<EmergencyRequest, int>();
        //    var allRequests = await repo.GetAllAsync(new EmergencyRequestSpecification(new RequestQueryData() { IsCompleted = false }));
        //    return mapper.Map<List<EmergencyRequestDetailsDTO>>(allRequests);

        //}

        public async Task<List<EmergencyRequestDetailsDTO>> GetAllRequestsAssignedToTechnicianAsync(int technicianId)
        {
            var spec = new EmergencyRequestTechniciansAssignedToTechSpec(technicianId, RequestState.Waiting);

            var repo = unitOfWork.GetRepository<EmergencyRequestTechnicians, int>();

            var assignedEntries = await repo.GetAllAsync(spec);

            var result = mapper.Map<List<EmergencyRequestDetailsDTO>>(assignedEntries);

            return result;
        }

        public async Task<EmergencyRequestDetailsDTO> GetRequestDetailsByIdAsync(int requestId, int technicianId)
        {
            var spec = new EmergencyRequestTechnicianWithRequestSpec(requestId, technicianId);

            var joinEntry = await unitOfWork
                .GetRepository<EmergencyRequestTechnicians, int>()
                .GetByIdAsync(spec);

            if (joinEntry == null)
                return null;

            return mapper.Map<EmergencyRequestDetailsDTO>(joinEntry);
        }

        public async Task<bool> UpdateRequestFromCarOwnerAsync(TechnicianUpdateEmergencyRequestDTO dto)
        {
            // Verify technician + PIN
            var techSpec = new TechnicianWithAppUserSpec(dto.TechnicianId, dto.Pin);
            var technician = await unitOfWork.GetRepository<Technician, int>().GetByIdAsync(techSpec);
            if (technician == null) return false;

            // Load request with navigation
            var spec = new EmergencyRequestWithTechnicianLinkSpec(dto.RequestId);
            var request = await unitOfWork.GetRepository<EmergencyRequest, int>().GetByIdAsync(spec);
            if (request == null) return false;

            // Get link object
            var link = request.EmergencyRequestTechnicians.FirstOrDefault(e => e.TechnicianId == dto.TechnicianId);
            if (link == null) return false;

            if (dto.NewStatus == RequestState.Answered)
            {
                // Ensure no other accepted
                if (request.TechReverseRequests.Any(r => r.CallState == RequestState.Answered))
                    return false;

                link.CallStatus = RequestState.Answered;
                request.IsCompleted = true;
                request.EndTimeStamp = DateTime.UtcNow;

                request.TechReverseRequests.Add(new TechReverseRequest
                {
                    EmergencyRequestId = dto.RequestId,
                    TechnicianId = dto.TechnicianId,
                    CallState = RequestState.Answered,
                    TimeStamp = DateTime.UtcNow
                });
            }
            else if (dto.NewStatus == RequestState.Rejected)
            {
                link.CallStatus = RequestState.Rejected;

                request.TechReverseRequests.Add(new TechReverseRequest
                {
                    EmergencyRequestId = dto.RequestId,
                    TechnicianId = dto.TechnicianId,
                    CallState = RequestState.Rejected,
                    TimeStamp = DateTime.UtcNow
                });
            }

            await unitOfWork.SaveChangesAsync();
            return true;
        }

    }
}
