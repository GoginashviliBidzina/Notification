using System.Threading.Tasks;

namespace Notification.Infrastructure.Repository
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        Task<TEntity> AddAsync(TEntity entity);
    }
}
