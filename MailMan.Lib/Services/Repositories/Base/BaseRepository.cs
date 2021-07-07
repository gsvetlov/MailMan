using System;
using System.Collections.Generic;
using System.Linq;

using MailMan.Models.Base;

using Microsoft.EntityFrameworkCore;

namespace MailMan.Services.Repositories.Base
{
    public abstract class BaseRepository<T> : IRepository<T>, IDisposable where T : Entity
    {
        protected readonly DbContext _db;
        protected BaseRepository(DbContext dbContext)
        {
            _db = dbContext;
        }

        #region IRepository implementation

        public T Add(T entity)
        {
            _db.Add(entity);
            _db.SaveChanges();
            return entity;
        }

        public virtual bool Update(T entity) => CheckExist(entity) && UpdateEntity(entity);

        private bool UpdateEntity(T entity)
        {
            _ = _db.Set<T>().Update(entity);
            return _db.SaveChanges() > 0;
        }

        private bool CheckExist(T entity) => _db.Set<T>().Contains(entity);

        public virtual bool Remove(T entity)
        {
            _db.Remove(entity);
            return _db.SaveChanges() > 0;
        }

        public virtual T GetById(int id) => _db.Set<T>().Find(id);

        public virtual IEnumerable<T> GetSelected(Predicate<T> match) => _db.Set<T>().Where(e => match(e));

        public virtual IEnumerable<T> GetAll() => _db.Set<T>();

        #endregion

        #region IDisposable auto implementation
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _db.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~BaseRepository()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion



    }
}
