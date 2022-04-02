﻿using EntityFramework.Web.DBContext;
using EntityFramework.Web.Entities;
using Microsoft.AspNetCore.Http;
using WebNuoc.Repository.Interfaces;

namespace WebNuoc.Repository
{
    public class ContactRepository : GenericRepository<Contact, long>, IContactRepository
    {
        private readonly AppDbContext _context;
        public ContactRepository(AppDbContext dbContext, IHttpContextAccessor contextAccessor)
            : base(dbContext, contextAccessor)
        {
            _context = dbContext;
        }
    }
}