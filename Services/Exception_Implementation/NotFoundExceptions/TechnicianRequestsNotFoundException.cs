using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Exception_Implementation.NotFoundExceptions
{
    public class TechnicianRequestsNotFoundException:Exception
    {
        public TechnicianRequestsNotFoundException() : base("لا يوجد طلبات فنيه حاليه !")
        {
        }
    }
}
