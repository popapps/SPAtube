using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Model
{
    public class Video
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public virtual ICollection<Playlist> Playlists { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

        public int Seconds { get; set; }
    }
}
