using ServiceAbstraction.CoreServicesAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IServiceManager
    {

        ITechnicianService technicianService { get; }
        IRequestServices requestServices { get; }
        ITechnicianRequestEmergency technicianRequestEmergency { get; }
        ICategoryService categoryService { get; }

        ICarOwnerService carOwnerService { get; }


    }
}
