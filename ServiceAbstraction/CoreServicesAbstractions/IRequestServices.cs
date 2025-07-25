﻿using SharedData.DTOs.RequestsDTOs;
using SharedData.DTOs.TechnicianDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction.CoreServicesAbstractions
{
    public interface IRequestServices
    {
        public Task<PreRequestDTO> CreateRequestAsync(CreatePreRequestDTO request);
        public Task CancelAll(int id);
        public Task CreateRealRequest(RealRequestDTO request);

    }
}
