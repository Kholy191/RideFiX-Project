using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Exceptions;

namespace Service.Exception_Implementation.NotFoundExceptions
{

    public class TechnicianNotFountException : NotFoundException
    {
        public TechnicianNotFountException() : base($"لا يوجد فنيين متاحين في هذه المنطقة للقيام بهذه الخدمة")
        {
        }
    }
}

