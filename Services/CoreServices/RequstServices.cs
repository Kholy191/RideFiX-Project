using AutoMapper;
using Domain.Contracts;
using Domain.Entities.CoreEntites.EmergencyEntities;
using ServiceAbstraction.CoreServicesAbstractions;
using SharedData.DTOs.RequestsDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.CoreServices
{
    public class RequstServices : IRequestServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public RequstServices(IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }
        public async Task<CreatePreRequestDTO> CreateRequestAsync(CreatePreRequestDTO request)
        {
            var user = await unitOfWork.GetRepository<CarOwner, int>().GetByIdAsync(request.CarOwnerId);
            if (user == null)
            {
                throw new ArgumentException("Car Owner not found");
            }
            else if (user.ApplicationUser.PIN != request.PIN)
            {
                throw new ArgumentException("Invalid PIN provided for the Car Owner");
            }
            return request;

        }
    }
}
