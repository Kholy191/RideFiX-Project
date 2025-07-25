using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Service.CoreServices.TechniciansServices;
using ServiceAbstraction;
using ServiceAbstraction.CoreServicesAbstractions;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        public IRequestServices requestServices { get; }
        public ITechnicianService technicianService { get; }

        public ITechnicianRequestEmergency technicianRequestEmergency { get; }
        public ICategoryService categoryService { get; }
        public IReviewService reviewService { get; }
        public ICarOwnerService carOwnerService { get; }


        public ServiceManager(IRequestServices requestServices,
                    ITechnicianService technicianService, 
                    ITechnicianRequestEmergency _tech,
                    ICategoryService categoryService,
                    IReviewService reviewService)
                    ICarOwnerService carOwnerService)
        {
            this.requestServices = requestServices;
            this.technicianService = technicianService;
            this.technicianRequestEmergency = _tech;
            this.categoryService = categoryService;
            this.carOwnerService = carOwnerService;
            this.reviewService = reviewService;
        }



    }
}
