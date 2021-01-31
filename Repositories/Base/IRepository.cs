using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Base
{
    public interface IRepository<T> where T:class
    {
        public bool Add(T entity);
        public bool Update(T entity);
        public bool Remove(T entity);
        public T GetById(int id);
        public ICollection<T> GetAll();
    }
}
