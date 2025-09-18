namespace money_management_service.Services.Interfaces
{
    public interface IBaseService<TEntity, TKey>
    {
        Task<List<TEntity>> GetAllAsync();

        Task<TEntity> GetByIdAsync(TKey id);

        Task<TEntity> CreateAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TKey id, Dictionary<string, object> UpdateFields);

        Task<TEntity> DeleteByIdAsync(TKey id);
    }

    public interface IBaseService<TEntity> : IBaseService<TEntity, Guid> { }
}
