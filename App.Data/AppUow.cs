using System;
using App.Data.Contracts;
using App.Data.Helpers;

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
            // Do NOT enable proxied entities, else serialization fails
            DbContext.Configuration.ProxyCreationEnabled = false;

            // Load navigation properties explicitly (avoid serialization trouble)
            DbContext.Configuration.LazyLoadingEnabled = false;

            // Because Web API will perform validation, we don't need/want EF to do so
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


        public IPlaylistRepository Playlists
        {
            get { return GetRepository<IPlaylistRepository>(); }
        }
        public IUserProfileRepository UserProfiles
        {
            get { return GetRepository<IUserProfileRepository>(); }
        }
        public IVideoRepository Videos
        {
            get { return GetRepository<IVideoRepository>(); }
        }
    }
}
