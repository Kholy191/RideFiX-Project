using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.CoreEntites.EmergencyEntities
{
   public class TCategory : BaseEntity<int>
    {
        public string Name { get; set; }
        public ICollection<Technician> Technicians { get; set; } = new List<Technician>();
    }
}
