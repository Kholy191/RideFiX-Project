﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Contracts
{
    public interface IUnitOfWork
    {
        IGenericRepository<T, TK> GetRepository<T, TK>() where T : BaseEntity<TK>;

        public Task<int> SaveChangesAsync();
    }
}
