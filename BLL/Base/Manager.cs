using Repositories.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Base
{
    public abstract class Manager<T> : IManager<T> where T : class
    {
        private IRepository<T> repository;
        public Manager(IRepository<T> repository)
        {
            this.repository = repository;
        }
        public virtual bool Add(T entity)
        {
            return repository.Add(entity);
        }

        public virtual ICollection<T> GetAll()
        {
            return repository.GetAll();
        }

        public virtual T GetById(int id)
        {
            return repository.GetById(id);
        }

        public virtual bool Remove(T entity)
        {
            return repository.Remove(entity);
        }

        public virtual bool Update(T entity)
        {
            return repository.Update(entity);
        }
    }
}
