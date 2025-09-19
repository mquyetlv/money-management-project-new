using Microsoft.EntityFrameworkCore;
using money_management_service.Core;
using money_management_service.Data;
using money_management_service.Exceptions;
using money_management_service.Services.Interfaces;
using System.Linq.Dynamic.Core;

namespace money_management_service.Services
{
    public class BaseService<TEntity, TKey> : IBaseService<TEntity, TKey> where TEntity : class
    {
        protected readonly ApplicationDBContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseService(ApplicationDBContext dbContext) 
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<(int total, List<TEntity> data)> GetAllWithPagingAsync(CustomQuery<TEntity> customQuery)
        {
            var query = _dbSet.AsQueryable();

            if (customQuery.Filters.Count > 0)
            {
                customQuery.Filters.ForEach(filter => query = query.Where(filter));
            }

            int total = await query.CountAsync();

            query = query.Skip((customQuery.Page) * customQuery.Size).Take(customQuery.Size);

            if (customQuery.OrderBy != null)
            {
               query = customQuery.IsDescending ?? false
                    ? query.OrderBy($"{customQuery.OrderBy} descending")
                    : query.OrderBy(customQuery.OrderBy);
            }

            List<TEntity> data = await query.ToListAsync();

            return (total, data);
        }

        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            TEntity entity = await _dbSet.FindAsync(id);
            if (entity == null)
            {
                throw new NotFoundException("Not found");
            }
            return entity;
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> DeleteByIdAsync(TKey id)
        {
            TEntity entity = await _dbSet.FindAsync(id);
            if (entity == null)
            {
                throw new NotFoundException("Not found");
            }

            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TKey id, Dictionary<string, object> updateFields)
        {
            TEntity entity = await _dbSet.FindAsync(id);
            var entry = _dbSet.Entry(entity);
            
            if (entity == null)
            {
                throw new NotFoundException("Not found");
            }

            foreach(var item in updateFields)
            {
                var property = entry.Property(item.Key);
                if (property != null) 
                {
                    property.CurrentValue = item.Value;
                    property.IsModified = true;
                }
            }

            await _dbContext.SaveChangesAsync();

            return entity;
        }
    }
    public class BaseService<TEntity> : BaseService<TEntity, Guid> where TEntity : class 
    {
        public BaseService(ApplicationDBContext _dbContext) : base(_dbContext) { }
    }
}
