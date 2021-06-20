using System;
using System.Collections.Generic;

namespace MailMan.Services.Repositories.Base
{
    public interface IRepository<T> where T : class
    {
        public T Create(params object[] parameters);
        public bool Add(T entity);
        public bool Update(T entity, Predicate<T> match);
        public bool Remove(T entity);
        public IEnumerable<T> GetAll();
        public IEnumerable<T> SelectEntities(Predicate<T> match);
    }
}
