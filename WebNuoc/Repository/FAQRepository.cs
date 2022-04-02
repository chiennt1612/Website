﻿using EntityFramework.Web.DBContext;
using EntityFramework.Web.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebNuoc.Repository.Interfaces;

namespace WebNuoc.Repository
{
    public class FAQRepository : GenericRepository<FAQ, long>, IFAQRepository
    {
        private readonly AppDbContext _context;
        public FAQRepository(AppDbContext dbContext, IHttpContextAccessor contextAccessor)
            : base(dbContext, contextAccessor)
        {
            _context = dbContext;
        }

        public async Task<IList<FAQ>> GetTopAsync(Expression<Func<FAQ, bool>> expression, int top)
        {
            return await _context.FAQs
                            .Where(a => a.IsDeleted == false)
                            .Where(expression)
                            .OrderByDescending(u => u.Id).Take(top).ToListAsync();
        }
    }
}
