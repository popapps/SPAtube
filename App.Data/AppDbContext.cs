using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using App.Data.Configuration;
using App.Model;

namespace App.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(): base("DefaultConnection")
        {
        }

        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Video> Videos { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new PlaylistConfiguration());
            modelBuilder.Configurations.Add(new UserProfileConfiguration());
            modelBuilder.Configurations.Add(new VideoConfiguration());
        }

    }
}
