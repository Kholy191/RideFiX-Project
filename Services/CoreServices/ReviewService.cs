using AutoMapper;
using Domain.Contracts;
using Domain.Entities.CoreEntites.EmergencyEntities;
using Service.Exception_Implementation.ArgumantNullException;
using ServiceAbstraction.CoreServicesAbstractions;
using SharedData.DTOs.ReviewsDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.CoreServices
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public ReviewService(IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }
        public async Task AddReviewAsync(AddReviewDTO addReview)
        {
            if (addReview == null)
            {
                throw new ReviewNullException();
            }
            var review = mapper.Map<Review>(addReview);
            await unitOfWork.GetRepository<Review, int>().AddAsync(review);
            await unitOfWork.SaveChangesAsync();

        }
    }
}
