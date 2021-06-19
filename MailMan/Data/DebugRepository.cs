using System;
using System.Collections.Generic;

using MailMan.Services.Repositories.Base;

namespace MailMan.Data
{
    public abstract class DebugRepository<T> : IRepository<T> where T : class
    {
        protected readonly List<T> collection;

        public DebugRepository(List<T> collection) => this.collection = collection ?? throw new ArgumentNullException(nameof(collection));
        public DebugRepository() => this.collection = new();

        public abstract T Create(params object[] parameters); //  => throw new NotImplementedException()
        public virtual bool Add(T entity)
        {
            collection.Add(entity);
            return true;
        }
        public virtual bool Update(T entity, Predicate<T> match)
        {
            var idx = collection.FindIndex(match);
            if (idx == -1) return false;
            collection[idx] = entity;
            return true;
        }
        public virtual bool Remove(T entity) => collection.Remove(entity);
        public virtual IEnumerable<T> GetAll() => collection;
        public virtual IEnumerable<T> SelectEntities(Predicate<T> match) => collection.FindAll(match);
    }
}
