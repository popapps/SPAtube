using System.Collections.Generic;
using System.Linq;
using App.Data.Contracts;
using App.Model;

namespace App.Data
{
    public class PlaylistRepository : BaseRepository<Playlist>, IPlaylistRepository
    {
        public PlaylistRepository(AppDbContext dbContext)
            : base(dbContext)
        {
        }

        public IEnumerable<Playlist> GetByUser(int userId)
        {
            return GetAll()
                .Where(t => t.UserProfileId == userId).ToList();
        }
    }
}
