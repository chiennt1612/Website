﻿using EntityFramework.Web.DBContext;
using EntityFramework.Web.Entities;
using Microsoft.AspNetCore.Http;
using WebNuoc.Repository.Interfaces;

namespace WebNuoc.Repository
{
    public class AboutRepository : GenericRepository<About, long>, IAboutRepository
    {
        private readonly AppDbContext _context;
        public AboutRepository(AppDbContext dbContext, IHttpContextAccessor contextAccessor)
            : base(dbContext, contextAccessor)
        {
            _context = dbContext;
        }
    }
}
