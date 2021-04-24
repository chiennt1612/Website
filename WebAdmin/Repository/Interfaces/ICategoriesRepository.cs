using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EntityFramework.Web.Entities;
using WebAdmin.Helpers;
using X.PagedList;

namespace WebAdmin.Repository.Interfaces
{
    public interface ICategoriesRepository: IGenericRepository<Categories, long>
    {
    }
}
