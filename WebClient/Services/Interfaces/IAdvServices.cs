﻿using EntityFramework.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebClient.Services.Interfaces
{
    public interface IAdvServices
    {
        Task<IEnumerable<Adv>> GetAllAsync();
        Task<Adv> GetByIdAsync(long Id);
        Task<IEnumerable<Adv>> GetManyAsync(Expression<Func<Adv, bool>> where);
    }
}
