using EntityFramework.Web.Entities;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using System.Threading.Tasks;

namespace WebNuoc.Services.Interfaces
{
    public interface IContactServices
    {
        Task<Contact> AddAsync(Contact contact);
        Task<Contact> GetByIdAsync(long Id);
        Task<Contact> Update(Contact order);
        Task<IEnumerable<Contact>> GetManyAsync(Expression<Func<Contact, bool>> where);
    }
}
