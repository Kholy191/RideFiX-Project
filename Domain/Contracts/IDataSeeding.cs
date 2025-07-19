﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IDataSeeding
    {
        public Task SeedDataAsync();
        public Task SeedCategories();
        public Task SeedIdentityDataAsync();
    }
}
