using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using MailMan.Models.Base;

namespace MailMan.Services.Repositories.Base
{
    public interface IRepositoryAsync<T> : IRepository<T> where T : Entity
    {
        public Task<T> AddAsync(T entity);
        public Task<bool> UpdateAsync(T entity);
        public Task<bool> RemoveAsync(T entity);
        public Task<T> GetByIdAsync(int id);
        public Task<IEnumerable<T>> GetSelectedAsync(Predicate<T> match);
        public Task<IEnumerable<T>> GetAllAsync();
    }
}
