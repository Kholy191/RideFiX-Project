using Domain.Entities.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.CoreEntites.EmergencyEntities
{
   public abstract class IcanOrder : ApplicationUser
    {
        public ICollection<Technician> Technicianes { get; set; } = new List<Technician>();
        public ICollection<CarOwner> CarOwners { get; set; } = new List<CarOwner>();
    }
}
