using System.Data.Entity.ModelConfiguration;
using App.Model;

namespace App.Data.Configuration
{
    public class PlaylistConfiguration : EntityTypeConfiguration<Playlist>
    {
        public PlaylistConfiguration()
        {
            ToTable("Playlists");
            Property(t => t.Title).HasMaxLength(255);
            HasRequired(t => t.UserProfile).WithMany(t => t.Playlists).HasForeignKey(t => t.UserProfileId);
            HasMany(t => t.Videos).WithMany(t => t.Playlists);
        }
    }
}
