using EntityFramework.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebNuoc.Repository.Interfaces
{
    public interface IFAQRepository : IGenericRepository<FAQ, long>
    {
        Task<IList<FAQ>> GetTopAsync(Expression<Func<FAQ, bool>> expression, int top);
    }
}
