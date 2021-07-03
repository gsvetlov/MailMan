using System;
using System.Collections.Generic;
using System.Linq;

using MailMan.Models.Base;
using MailMan.Services.Repositories.Base;

namespace MailMan.Data
{
    public abstract class DebugRepository<T> : IRepository<T> where T : Entity, new()
    {
        protected readonly List<T> collection;

        public DebugRepository(List<T> set)
        {
            if (set is null) throw new ArgumentNullException(nameof(set));
            collection = CheckValidity(set) ? set : throw new ArgumentOutOfRangeException(nameof(set), "Collection contains invalid or duplicate entities");
        }

        // проверка, что значения Id уникальны и Id > 0
        private static bool CheckValidity(List<T> set) => (set.Distinct().Count() == set.Count) && (set.FindAll(e => e.Id < 1).Count == 0);

        public DebugRepository() => collection = new();
                
        public virtual T Add(T entity)
        {
            if (collection.Contains(entity)) return entity;
            if (entity.Id < 1)
                entity.Id = collection.Max(e => e.Id) + 1;
            collection.Add(entity);
            return entity;
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
        public virtual IEnumerable<T> GetSelected(Predicate<T> match) => collection.FindAll(match);
        public bool Update(T entity, int id) => Update(entity, e => e.Id == id);
        public T GetById(int id) => collection.Find(e => e.Id == id);
    }
}
