using Microsoft.EntityFrameworkCore;
using Simple_Online_Voitng_System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repositories.Base
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected ApplicationDbContext db;
        public Repository(ApplicationDbContext db)
        {
            this.db = db;
        }
        private DbSet<T> Table
        {
            get
            {
                return db.Set<T>();
            }
        }
        public virtual bool Add(T entity)
        {
            Table.Add(entity);
            return db.SaveChanges() > 0;
        }

        public virtual ICollection<T> GetAll()
        {
            return Table.ToList();
        }

        public virtual T GetById(int id)
        {
            return Table.Find(id);
        }

        public virtual bool Remove(T entity)
        {
            Table.Remove(entity);
            return db.SaveChanges()>0;
        }

        public virtual bool Update(T entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            return db.SaveChanges() > 0;
        }
    }
}
