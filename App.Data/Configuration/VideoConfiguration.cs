using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using App.Model;

namespace App.Data.Configuration
{
    public class VideoConfiguration : EntityTypeConfiguration<Video>
    {
        public VideoConfiguration()
        {
            ToTable("Videos");
            Property(t => t.Id).HasMaxLength(50);
            Property(t => t.Title).HasMaxLength(255);
        }
    }
}
