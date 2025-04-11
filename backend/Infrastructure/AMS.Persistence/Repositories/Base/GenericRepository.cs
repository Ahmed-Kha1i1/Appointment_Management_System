using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AMS.Application.Contracts.Persistence.Base;
using AMS.Doman.Entities.Base;

namespace AMS.Persistence.Repositories.Base
{
    public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : BaseEntity
    {
        private readonly AppDbContext _dbContext;
        private DbSet<Entity> _entities { get; set; }
        public GenericRepository(AppDbContext context)
        {
            _dbContext = context;
            _entities = _dbContext.Set<Entity>();
        }
        public async Task<Entity?> GetByIdAsync(int id)
        {
            return await _entities.FindAsync(id);
        }
        public async Task<bool> IsExists(int id)
        {
            return await _entities.AnyAsync(e => e.Id == id);
        }
        public async Task<List<Entity>> GetAllAsNoTracking()
        {
            return await _entities.AsNoTracking().ToListAsync();
        }

        public IQueryable<Entity> GetAllAsTracking()
        {
            return _entities.AsQueryable();
        }

        public async Task AddRangeAsync(ICollection<Entity> entities)
        {
            await _entities.AddRangeAsync(entities);
        }

        public async Task AddAsync(Entity entity)
        {
            await _entities.AddAsync(entity);
        }

        public async Task UpdateRangeAsync<TProperty>(Func<Entity, TProperty> propertyExpression, Func<Entity, TProperty> valueExpression)
        {
            await _entities.ExecuteUpdateAsync(x => x.SetProperty(propertyExpression, valueExpression));
        }

        public async Task DeleteRangeAsync(Expression<Func<Entity, bool>> predicate)
        {
            await _entities.Where(predicate).ExecuteDeleteAsync();
        }

        public void Delete(Entity entity)
        {
            _entities.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
             await _entities.Where(en => en.Id == id).ExecuteDeleteAsync();
        }
    }
}
