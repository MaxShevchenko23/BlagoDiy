namespace BlagoDiy.DataAccessLayer.Repositories;

public abstract class Repository<TEntity> where TEntity : class
{
    public abstract Task<TEntity?> GetByIdAsync(int id);
    public abstract Task<IEnumerable<TEntity>> GetAllAsync();
    public abstract Task AddAsync(TEntity entity);
    public abstract Task UpdateAsync(TEntity entity);
    public abstract Task DeleteAsync(int id);
}