using EntityFramework.Web.Entities;

namespace WebAdmin.Repository.Interfaces
{
    public interface IAdvRepository : IGenericRepository<Adv, long>
    {
    }
    public interface IAdvPositionRepository : IGenericRepository<AdvPosition, long>
    {
    }
}
