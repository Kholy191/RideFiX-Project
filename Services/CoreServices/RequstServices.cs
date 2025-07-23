using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities.CoreEntites.EmergencyEntities;
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

        public Task CancelAll(int id)
        {
            throw new NotImplementedException();
        }

        public async Task CreateRealRequest(RealRequestDTO request)
        {
            var emergancyRequest = request.TechnicianIDs
                .Select(technicianId => new EmergencyRequest
                {
                    CarOwnerId = request.CarOwnerId,
                    TechnicainId = technicianId,
                    Description = request.Description,
                    Latitude = request.Latitude,
                    Longitude = request.Longitude,             
                    CallState = RequestState.Waiting,
                    IsCompleted = false,
                    TimeStamp = DateTime.UtcNow,
                    EndTimeStamp = null,
                    categoryId = request.categoryId
                }).ToList();
            foreach (var item in emergancyRequest)
            {
                await unitOfWork.GetRepository<EmergencyRequest, int>().AddAsync(item);
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
            else if (user.ApplicationUser.PIN != request.PIN)
            {
                throw new PinCodeBadRequestException();
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
