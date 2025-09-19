using money_management_service.Core;

namespace money_management_service.Services.Interfaces
{
    public interface IBaseService<TEntity, TKey>
    {
        Task<List<TEntity>> GetAllAsync();

        Task<(int total, List<TEntity> data)> GetAllWithPagingAsync(CustomQuery<TEntity> customQuery);

        Task<TEntity> GetByIdAsync(TKey id);

        Task<TEntity> CreateAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TKey id, Dictionary<string, object> updateFields);

        Task<TEntity> DeleteByIdAsync(TKey id);
    }

    public interface IBaseService<TEntity> : IBaseService<TEntity, Guid> { }
}
