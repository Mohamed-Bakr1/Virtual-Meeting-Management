using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Persistence
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly VirtualMeetingDbContext _context;
        private readonly DbSet<TEntity> _entity;

        public Repository(VirtualMeetingDbContext context)
        {
            _context = context;
            _entity = _context.Set<TEntity>();
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return _entity.Where(predicate);
        }

        public async Task<TEntity?> GetByIdAsync(object id)
        {
            return await _entity.FindAsync(id);
        }

        public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _entity.FirstOrDefaultAsync(predicate);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _entity.AnyAsync(predicate);
        }

        public async Task AddAsync(TEntity entity)
        {
            await _entity.AddAsync(entity);
        }

        public async Task AddRange(IEnumerable<TEntity> entities)
        {
            await _entity.AddRangeAsync(entities);
        }

        public void Update(TEntity entity)
        {
            _entity.Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _entity.UpdateRange(entities);
        }

        public async Task DeleteAsync(object id)
        {
            var entity = await _entity.FindAsync(id);

            if (entity is null)
                return;

            _entity.Remove(entity);
        }

        public void Delete(TEntity entity)
        {
            _entity.Remove(entity);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            _entity.RemoveRange(entities);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}