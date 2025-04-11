using System.Linq.Expressions;

namespace AMS.Application.Contracts.Persistence.Base
{
    public interface IGenericRepository<Entity> where Entity : class
    {
        Task<Entity?> GetByIdAsync(int id);
        Task<bool> IsExists(int id);
        Task<List<Entity>> GetAllAsNoTracking();
        IQueryable<Entity> GetAllAsTracking();
        Task AddRangeAsync(ICollection<Entity> entities);
        Task AddAsync(Entity entity);
        Task UpdateRangeAsync<TProperty>(Func<Entity, TProperty> propertyExpression, Func<Entity, TProperty> valueExpression);
        Task DeleteRangeAsync(Expression<Func<Entity, bool>> predicate);
        Task DeleteAsync(int id);
        void Delete(Entity entity);
        Task SaveChangesAsync();

    }
}
