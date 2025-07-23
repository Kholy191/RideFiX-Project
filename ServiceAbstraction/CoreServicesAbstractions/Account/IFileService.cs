using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ServiceAbstraction.CoreServicesAbstractions.Account
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile file, string folderName = "uploads");

    }
}
