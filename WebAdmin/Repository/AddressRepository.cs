using EntityFramework.Web.DBContext;
using EntityFramework.Web.Entities.Ordering;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebAdmin.Repository.Interfaces;

namespace WebAdmin.Repository
{
    public class AddressRepository : GenericRepository<Address, int>, IAddressRepository
    {
        private readonly HttpContext _httpContext;
        private readonly AppDbContext _dbContext;
        private readonly DbSet<Address> _dbSet;
        public AddressRepository(AppDbContext dbContext, IHttpContextAccessor contextAccessor)
            : base(dbContext, contextAccessor)
        {
            _dbContext = dbContext;
            _httpContext = contextAccessor?.HttpContext;
            _dbSet = _dbContext.Set<Address>();
        }

        //public async Task<Address> GetByIdAsync(int id)
        //{
        //    return await _dbContext.Addresss.FirstOrDefaultAsync(m => m.Id == id);
        //}

        public async Task<Address> GetByUserIdAsync(long UserId)
        {
            return await _dbContext.Addresss.Where(m => m.UserId == UserId).Take(1).FirstOrDefaultAsync();
        }

        //public async Task<BaseEntityList<Address>> GetListAsync(Expression<Func<Address, bool>> expression, Func<Address, object> sort, bool desc, int page, int pageSize)
        //{
        //    var a = new BaseEntityList<Address>();
        //    a.totalRecords = await CountAsync(expression);
        //    int skipRows = (page - 1) * pageSize;
        //    if (desc)
        //        a.list = (await _dbSet.AsNoTracking().Where(expression).ToListAsync()).OrderByDescending(sort).Skip(skipRows).Take(pageSize).ToList();
        //    else
        //        a.list = (await _dbSet.AsNoTracking().Where(expression).ToListAsync()).OrderBy(sort).Skip(skipRows).Take(pageSize).ToList();
        //    return a;
        //}
        //public async Task<int> CountAsync(Expression<Func<Address, bool>> expression)
        //{
        //    return await _dbSet.AsNoTracking().CountAsync(expression);
        //}

        //public async  Task<IPagedList<Address>> GetListByPage(Expression<Func<Address, bool>> expression, Func<Address, object> sort, bool desc = false, int pageIndex = 1, int pageSize = 10)
        //{
        //    IQueryable<Address> r = _dbContext.Addresss.Where(expression);
        //    IOrderedEnumerable<Address> r1;
        //    if (desc)
        //    {
        //        r1 = r.OrderBy(sort);
        //    }
        //    else
        //    {
        //        r1 = r.OrderByDescending(sort);
        //    }
        //    return await r1.ToPagedListAsync(pageIndex, pageSize);
        //}
    }
}
