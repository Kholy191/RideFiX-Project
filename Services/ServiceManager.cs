using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using ServiceAbstraction;
using ServiceAbstraction.CoreServicesAbstractions;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        public IRequestServices requestServices { get; }
        public ITechnicianService technicianService { get; }

        public ServiceManager(IRequestServices requestServices, ITechnicianService technicianService)
        {
            this.requestServices = requestServices;
            this.technicianService = technicianService;
        }

    }
}
