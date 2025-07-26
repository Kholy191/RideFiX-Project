﻿using AutoMapper;
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
		public async Task<bool> ApplyRequestFromHomePage(TechnicianApplyEmergencyRequestDTO emergencyRequestDTO)
		{
			var techSpec = new TechnicianWithAppUserSpec(emergencyRequestDTO.UserId, emergencyRequestDTO.Pin);
			var technician = await unitOfWork.GetRepository<Technician, int>().GetByIdAsync(techSpec);
			if (technician == null) return false;
			var repo = unitOfWork.GetRepository<EmergencyRequest, int>();
			EmergencyRequest requestToUpdate = await repo.GetByIdAsync(emergencyRequestDTO.RequestId);
			//TechReverseRequest isTechnicanExists = requestToUpdate.TechReverseRequests.Where(r => r.TechnicianId == emergencyRequestDTO.UserId).First();
			var rechRequestRepo = unitOfWork.GetRepository<TechReverseRequest, int>();
			TechReverseRequest isTechnicanExists = await rechRequestRepo.GetByIdAsync(new TechReverseRequestSpec(emergencyRequestDTO.RequestId, emergencyRequestDTO.UserId));

			if (requestToUpdate != null && isTechnicanExists == null)
			{
				requestToUpdate.TechReverseRequests.Add(new TechReverseRequest
				{
					EmergencyRequestId = emergencyRequestDTO.RequestId, 
					TechnicianId = emergencyRequestDTO.UserId,
					CallState = RequestState.Waiting,
					TimeStamp = DateTime.UtcNow
				});
				int affectedRows = await unitOfWork.SaveChangesAsync();
				if (affectedRows > 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}

		}

		public async Task<List<EmergencyRequestDetailsDTO>> GetAllAcceptedRequestsAsync(int tecId)
		{

			var requestTechRepo = unitOfWork.GetRepository<EmergencyRequestTechnicians, int>();
			var joinEntries = await requestTechRepo.GetAllAsync(new EmergencyRequestTechnicanSpecefication(new RequestQueryData() { TechnicainId = tecId, CallState = RequestState.Answered }));
			return mapper.Map<List<EmergencyRequestDetailsDTO>>(joinEntries);
		}

		public async Task<List<EmergencyRequestDetailsDTO>> GetAllActiveRequestsAsync(int tecId)
		{
			var requests = unitOfWork.GetRepository<EmergencyRequest, int>();
			var spec = new EmergencyRequestWithTechnicianLinkSpec(tecId, false);
			var activeRequests = await requests.GetAllAsync(spec);
			if (activeRequests == null || !activeRequests.Any())
				return new List<EmergencyRequestDetailsDTO>();

			return mapper.Map<List<EmergencyRequestDetailsDTO>>(activeRequests);



		}

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

            if (joinEntry == null) {
                Console.WriteLine("join entry null");
                return null;
            }
               
            

			return mapper.Map<EmergencyRequestDetailsDTO>(joinEntry);
		}

        public async Task<bool> UpdateRequestFromCarOwnerAsync(TechnicianUpdateEmergencyRequestDTO dto)
        {
            // 1. Verify technician and PIN
            var techSpec = new TechnicianWithAppUserSpec(dto.TechnicianId, dto.Pin);
            var technician = await unitOfWork.GetRepository<Technician, int>().GetByIdAsync(techSpec);
            if (technician == null) return false;

            // 2. Load EmergencyRequest with technician links
            var spec = new EmergencyRequestWithTechnicianLinkSpec(dto.RequestId);
            var request = await unitOfWork.GetRepository<EmergencyRequest, int>().GetByIdAsync(spec);
            if (request == null) return false;

            // 3. Find link between this technician and the request
            var targetLink = request.EmergencyRequestTechnicians
                .FirstOrDefault(e => e.TechnicianId == dto.TechnicianId);
            if (targetLink == null) return false;

            // 4. Technician is accepting
            if (dto.RequestState == RequestState.Answered)
            {
                // Make sure no one else has accepted
                bool alreadyAccepted = request.EmergencyRequestTechnicians
                    .Any(e => e.CallStatus == RequestState.Answered);
                if (alreadyAccepted) return false;

                // Mark this technician as accepted
                targetLink.CallStatus = RequestState.Answered;

                // Mark all other technicians as rejected
                foreach (var link in request.EmergencyRequestTechnicians)
                {
                    if (link.TechnicianId != dto.TechnicianId)
                    {
                        link.CallStatus = RequestState.Rejected;
                    }
                }

                // Mark the request as completed
                request.IsCompleted = true;
                request.EndTimeStamp = DateTime.UtcNow;
            }
            // 5. Technician is rejecting
            else if (dto.RequestState == RequestState.Rejected)
            {
                targetLink.CallStatus = RequestState.Rejected;

               
            }

            await unitOfWork.SaveChangesAsync();
            return true;
        }



    }
}
