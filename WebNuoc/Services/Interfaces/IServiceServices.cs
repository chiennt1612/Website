﻿using EntityFramework.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebNuoc.Services.Interfaces
{
    public interface IServiceServices
    {
        Task<IEnumerable<Service>> GetAllAsync();
        Task<Service> GetByIdAsync(long Id);
        Task<BaseEntityList<Service>> GetListAsync(
            Expression<Func<Service, bool>> expression,
            Func<Service, object> sort, bool desc,
            int page, int pageSize);
    }
}
