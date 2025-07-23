using SharedData.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction.CoreServicesAbstractions
{
    public interface ICategoryService
    {
        public Task<List<TCategoryDTO>> GetAllCategoriesAsync();
    }
}
