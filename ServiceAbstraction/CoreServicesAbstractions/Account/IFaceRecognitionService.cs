using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction.CoreServicesAbstractions.Account
{
    public interface IFaceRecognitionService
    {
        Task<bool> AreFacesMatchingAsync(Stream image1, Stream image2);
    }
}
