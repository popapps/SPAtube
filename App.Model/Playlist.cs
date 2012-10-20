using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Model
{
    public class Playlist
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public int UserProfileId { get; set; }
        public virtual UserProfile UserProfile { get; set; }


        public virtual ICollection<Video> Videos { get; set; }
    }
}
