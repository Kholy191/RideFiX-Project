using AutoMapper;
using Domain.Contracts;
using Domain.Entities.CoreEntites.EmergencyEntities;
using ServiceAbstraction.CoreServicesAbstractions;
using SharedData.DTOs;
using SharedData.DTOs.RequestsDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
