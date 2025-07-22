using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Service.Exception_Implementation.NotFoundExceptions
{
    public class CarOwnerNotFoundException : NotFoundException 
    {
        public CarOwnerNotFoundException() : base($"لا يوجد صاحب سيارة بهذه البيانات")
        {
        }
    }
}
