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

        private static bool CheckValidity(List<T> set) => ListIdsUnique(set) && ListIdsGreaterThanZero(set);

        private static bool ListIdsUnique(List<T> set) => set.Distinct().Count() == set.Count;

        private static bool ListIdsGreaterThanZero(List<T> set) => set.FindAll(e => e.Id < 1).Count == 0;

        public DebugRepository() => collection = new();
                
        public virtual T Add(T entity)
        {
            if (collection.Contains(entity)) return entity;
            if (entity.Id < 1)
                entity.Id = collection.Max(e => e.Id) + 1;
            collection.Add(entity);
            return entity;
        }
        public virtual bool Update(T entity)
        {
            var idx = collection.FindIndex(e => e.Id == entity.Id);
            if (idx == -1) return false;
            collection[idx] = entity;
            return true;
        }
        public virtual bool Remove(T entity) => collection.Remove(entity);
        public virtual IEnumerable<T> GetAll() => collection;
        public virtual IEnumerable<T> GetSelected(Predicate<T> match) => collection.FindAll(match);        
        public T GetById(int id) => collection.Find(e => e.Id == id);
    }
}
