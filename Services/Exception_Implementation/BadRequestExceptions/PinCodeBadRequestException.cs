using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Exceptions;

namespace Service.Exception_Implementation.BadRequestExceptions
{
    public class PinCodeBadRequestException : BadRequestException
    {
        public PinCodeBadRequestException() : base("Invalid PIN provided for the Car Owner")
        {
        }
    }
}
