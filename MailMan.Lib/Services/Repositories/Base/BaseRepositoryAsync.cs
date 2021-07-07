using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MailMan.Models.Base;

using Microsoft.EntityFrameworkCore;

namespace MailMan.Services.Repositories.Base
{
    public abstract class BaseRepositoryAsync<T> : BaseRepository<T>, IRepositoryAsync<T>, IAsyncDisposable where T : Entity
    {
        public BaseRepositoryAsync(DbContext dbContext) : base(dbContext) { }

        #region IRepositoryAsync implementation

        public virtual async Task<T> AddAsync(T entity)
        {
            _db.Add(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<bool> UpdateAsync(T entity) => await CheckExistAsync(entity) && await UpdateEntityAsync(entity);

        private Task<bool> CheckExistAsync(T entity) => _db.Set<T>().ContainsAsync(entity);

        private async Task<bool> UpdateEntityAsync(T entity)
        {
            _db.Set<T>().Update(entity);
            return await _db.SaveChangesAsync() > 0;
        }

        public virtual async Task<bool> RemoveAsync(T entity)
        {
            _db.Remove(entity);
            return await _db.SaveChangesAsync() > 0;
        }

        public virtual async Task<T> GetByIdAsync(int id) => await _db.FindAsync<T>(id);

        public virtual async Task<IEnumerable<T>> GetSelectedAsync(Predicate<T> match) => await _db.Set<T>().Where(e => match(e)).ToListAsync();

        public virtual async Task<IEnumerable<T>> GetAllAsync() => await _db.Set<T>().ToListAsync();

        #endregion

        public ValueTask DisposeAsync() => ((IAsyncDisposable)_db).DisposeAsync();
    }
}
