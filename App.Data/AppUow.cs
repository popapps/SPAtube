using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using App.Data.Contracts;
using App.Data.Helpers;
using App.Model;

namespace App.Data
{
    public class AppUow : IAppUow, IDisposable
    {
        private AppDbContext DbContext { get; set; }

        public AppUow(IRepositoryProvider repositoryProvider)
        {
            CreateDbContext();
            repositoryProvider.DbContext = DbContext;
            RepositoryProvider = repositoryProvider;
        }


        private T GetRepository<T>() where T : class
        {
            return RepositoryProvider.GetRepository<T>();
        }

        private IRepository<T> GetStandardRepository<T>() where T : class
        {
            return RepositoryProvider.GetRepositoryForEntityType<T>();
        }

        private void CreateDbContext()
        {
            DbContext = new AppDbContext();
            // Disabilito la creazione delle entity proxy altrimenti la serializzazione fallisce
            DbContext.Configuration.ProxyCreationEnabled = false;

            // Le proprietà di navigazione possono essere navigate solo tramite Load esplicito
            DbContext.Configuration.LazyLoadingEnabled = false;

            DbContext.Configuration.ValidateOnSaveEnabled = false;
        }

        public void Dispose()
        {
        }

        internal IRepositoryProvider RepositoryProvider { get; set; }

        public void Commit()
        {
            DbContext.SaveChanges();
        }


        public IUserProfileRepository UserProfiles
        {
            get { return GetRepository<IUserProfileRepository>(); }
        }
    }
}
