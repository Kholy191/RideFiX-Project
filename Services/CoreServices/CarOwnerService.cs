using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities.CoreEntites.EmergencyEntities;
using Service.Exception_Implementation.NotFoundExceptions;
using Service.Specification_Implementation;
using ServiceAbstraction.CoreServicesAbstractions;
using SharedData.DTOs;
using SharedData.DTOs.RequestsDTOs;
using SharedData.Enums;

namespace Service.CoreServices
{
    public class CarOwnerService : ICarOwnerService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public CarOwnerService(IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }

        public async Task<RequestBreifDTO> IsRequested(int Id)
        {
            var Repo = unitOfWork.GetRepository<EmergencyRequest,int>();
            var spec = new NotCompletedRequestSpecification(Id);
            var notCompletedRequests = await Repo.GetAllAsync(spec);
            if (notCompletedRequests.Any())
            {
                foreach (var request in notCompletedRequests)
                {
                    foreach (var tech in request.EmergencyRequestTechnicians)
                    {
                        if (tech.CallStatus == RequestState.Answered)
                        {
                            return mapper.Map<RequestBreifDTO>(request);
                        }
                    }
                    foreach (var rev in request.TechReverseRequests)
                    {
                        if (rev.CallState == RequestState.Answered)
                        {
                            return mapper.Map<RequestBreifDTO>(request);
                        }
                    }
                }
            }
            throw new RequestNotFoundException();
        }

        //public async Task<bool> ConfirmPIN(ConfirmPIN_DTO PinRequest)
        //{
        //    var user = await unitOfWork.GetRepository<CarOwner, int>().GetByIdAsync(PinRequest.CarOwnerId);
        //    if (user == null)
        //    {
        //        throw new ArgumentException("Car Owner not found");
        //    }else if (user.ApplicationUser.PIN != PinRequest.PIN)
        //    {
        //        throw new ArgumentException("Invalid PIN provided for the Car Owner");
        //    }
        //    return true;

        //}
    }
}
