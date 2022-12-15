using Microsoft.EntityFrameworkCore;

namespace Prova1.Application.Common.Interfaces.Persistence
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        DbSet<TEntity> Entity { get; }
        Task<bool> TryAddAndSaveAsync(TEntity entity);
        Task<TEntity> AddAndSaveAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(TEntity entity);
        Task<TEntity> GetByIdAsync(int id);
        Task<List<TEntity>> GetAllAsync();        
        Task ReloadIfModifiedAsync(TEntity entity);
    }    
}
