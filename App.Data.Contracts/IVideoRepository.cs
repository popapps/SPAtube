using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Model;

namespace App.Data.Contracts
{
    public interface IVideoRepository : IRepository<Video>
    {

        IEnumerable<Video> MostPopular();

        Video GetByVideoId(string id);

        IEnumerable<Video> SearchVideos(string q);

        IEnumerable<Video> GetByUser(int userId);
    }
}
