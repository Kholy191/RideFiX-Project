using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities.CoreEntites.EmergencyEntities;
using Microsoft.AspNetCore.Http.HttpResults;
using Service.Exception_Implementation;
using Service.Exception_Implementation.BadRequestExceptions;
using Service.Exception_Implementation.NotFoundExceptions;
using Service.Specification_Implementation;
using ServiceAbstraction.CoreServicesAbstractions;
using SharedData.DTOs.RequestsDTOs;
using SharedData.DTOs.TechnicianDTOs;
using SharedData.Enums;

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

        public async Task CancelAll(int CarOwnerID)
        {
            var spec = new CancelledRquestSpecification(CarOwnerID);
            var emergencyRequests = await unitOfWork.EmergencyRequestRepository.GetAllAsync(spec);
            if (emergencyRequests == null || !emergencyRequests.Any())
            {
                throw new RequestNotFoundException();
            }
            foreach (var emergencyRequest in emergencyRequests)
            {
                if (emergencyRequest.CallStatus == RequestState.Waiting)
                {
                    emergencyRequest.CallStatus = RequestState.Cancelled;
                    unitOfWork.EmergencyRequestRepository.UpdateAsync(emergencyRequest);
                }
            }
            await unitOfWork.SaveChangesAsync();

        }

        public async Task CreateRealRequest(RealRequestDTO request)
        {
           
            var emergancyRequest = new EmergencyRequest
            {
                CarOwnerId = request.CarOwnerId,
                Description = request.Description,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                IsCompleted = false,
                TimeStamp = DateTime.UtcNow,
                EndTimeStamp = null,
                categoryId = request.categoryId
              

            };
            
            await unitOfWork.GetRepository<EmergencyRequest, int>().AddAsync(emergancyRequest);

            if (request.TechnicianIDs != null && request.TechnicianIDs.Any())
            {
                foreach (var technicianId in request.TechnicianIDs)
                {
                    var emergencyRequestTechnicians = new EmergencyRequestTechnicians
                    {
                        EmergencyRequestId = emergancyRequest.Id,
                        TechnicianId = technicianId,
                        CallStatus = RequestState.Waiting
                    };
                    await unitOfWork.EmergencyRequestRepository.AddAsync(emergencyRequestTechnicians);
                }
            }

            await unitOfWork.SaveChangesAsync();

        }

        public async Task<PreRequestDTO> CreateRequestAsync(CreatePreRequestDTO request)
        {

            var spec = new CarOwnerSpecification(request);
            var user = await unitOfWork.GetRepository<CarOwner, int>().GetByIdAsync(spec);

            if (user == null)
            {
                throw new CarOwnerNotFoundException();
            }
            var filteredTechnicians = await technicianService.GetTechniciansByFilterAsync(request);
            if (filteredTechnicians == null || !filteredTechnicians.Any())
            {
                return new PreRequestDTO { };
            }

            return new PreRequestDTO { Technicians = filteredTechnicians };

        }

        public async Task<List<RequestBreifDTO>> RequestBreifDTOs(int carOwnerID)
        {
            var spec = new RequestBreifSpecification(carOwnerID);
            var emergencyRequests = await unitOfWork.GetRepository<EmergencyRequest, int>().GetAllAsync(spec);
            if (emergencyRequests == null || !emergencyRequests.Any())
            {
                throw new RequestNotFoundException();
            }
            var mappedRequests = mapper.Map<List<RequestBreifDTO>>(emergencyRequests);
            return mappedRequests;
        }
    }
}
