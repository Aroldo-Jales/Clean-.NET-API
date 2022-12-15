using Prova1.Application.Common.Interfaces.Persistence;
using System.Data.Entity;

namespace Prova1.Infrastructure.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        public DbSet<TEntity> Entity { get; }

        Microsoft.EntityFrameworkCore.DbSet<TEntity> IRepositoryBase<TEntity>.Entity => throw new NotImplementedException();

        private readonly DbContext _ctx;

        public RepositoryBase(DbContext ctx)
        {
            _ctx = ctx;
            Entity = _ctx.Set<TEntity>();
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await Entity.FindAsync(id);
        }

        public async Task<bool> TryAddAndSave(TEntity entity)
        {
            Entity.Add(entity);
            int commits = await _ctx.SaveChangesAsync();
            return commits > 0;
        }

        public Task<TEntity> AddAndSaveAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TryAddAndSaveAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task ReloadIfModifiedAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
