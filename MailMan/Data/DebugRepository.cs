using System;
using System.Collections.Generic;
using System.Linq;

using MailMan.Models.Base;
using MailMan.Services.Repositories.Base;

namespace MailMan.Data
{
    public abstract class DebugRepository<T> : IRepository<T> where T : BaseModel, new()
    {
        protected readonly List<T> collection;

        public DebugRepository(List<T> collection) => this.collection = collection ?? throw new ArgumentNullException(nameof(collection));
        public DebugRepository() => collection = new();

        public virtual T Create(params object[] parameters)
        {
            if (parameters is null) throw new ArgumentNullException(nameof(parameters));
            if (parameters[0] is not T entity) throw new ArgumentException($"parameter is not {typeof(T)}");
            entity.Id = collection.Max(e => e.Id) + 1;
            return Add(entity) ? entity : throw new InvalidOperationException("Failed to add new instance to repo");
        }
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
