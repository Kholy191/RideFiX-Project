﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SharedData.DTOs.Account;

namespace ServiceAbstraction.CoreServicesAbstractions.Account
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterStep1Async(RegisterStep1Dto dto);
        Task<IdentityResult> RegisterStep2Async(RegisterStep2Dto dto);
        Task<string> LoginAsync(LoginDto dto);

    }
}
