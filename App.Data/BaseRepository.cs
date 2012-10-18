using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using App.Data.Contracts;

namespace App.Data
{
    /// <summary>
    /// Classe base Repository generica per l'accesso ai dati. Dipende da Entity Framework. 
    /// </summary>
    /// <typeparam name="T">Tipo di entità per questo Repository</typeparam>
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        public BaseRepository(AppDbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext");
            DbContext = dbContext;
            DbSet = DbContext.Set<T>();
        }

        protected DbSet<T> DbSet
        {
            get;
            private set;
        }

        protected AppDbContext DbContext
        {
            get;
            private set;
        }
        public virtual IQueryable<T> GetAll()
        {
            return DbSet;
        }


        public virtual T GetById(int id)
        {
            return DbSet.Find(id);
        }
        public virtual void Add(T entity)
        {
            var dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
                DbSet.Add(entity);
            }
        }

        public void Update(T entity)
        {
            var dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            dbEntityEntry.State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            var dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                if (dbEntityEntry.State == EntityState.Detached)
                {
                    DbSet.Attach(entity);
                }
                DbSet.Remove(entity);
            }
        }

        public virtual void Delete(int id)
        {
            var entity = GetById(id);
            if (entity == null)
                return;
            Delete(entity);
        }
    }

}
