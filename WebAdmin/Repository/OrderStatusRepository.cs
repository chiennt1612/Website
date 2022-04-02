﻿using EntityFramework.Web.DBContext;
using EntityFramework.Web.Entities.Ordering;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebAdmin.Repository.Interfaces;

namespace WebAdmin.Repository
{
    public class OrderStatusRepository : IOrderStatusRepository
    {
        private readonly HttpContext _httpContext;
        private readonly AppDbContext _dbContext;
        private readonly DbSet<OrderStatus> _dbSet;
        public OrderStatusRepository(AppDbContext dbContext, IHttpContextAccessor contextAccessor)
        {
            _dbContext = dbContext;
            _httpContext = contextAccessor?.HttpContext;
            _dbSet = _dbContext.Set<OrderStatus>();
        }
        public async Task<IEnumerable<OrderStatus>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<OrderStatus> GetAsync(Expression<Func<OrderStatus, bool>> where)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(where);
        }

        public async Task<OrderStatus> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
    }
}
