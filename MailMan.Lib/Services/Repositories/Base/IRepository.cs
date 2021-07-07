using System;
using System.Collections.Generic;

using MailMan.Models.Base;

namespace MailMan.Services.Repositories.Base
{
    public interface IRepository<T> where T : Entity
    {   
        public T Add(T entity);
        public bool Update(T entity);
        public bool Remove(T entity);
        public T GetById(int id);
        public IEnumerable<T> GetSelected(Predicate<T> match);
        public IEnumerable<T> GetAll();
    }
}
