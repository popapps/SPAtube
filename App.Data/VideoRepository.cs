using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml.Linq;
using App.Data.Contracts;
using Google.GData.Client;
using Google.GData.YouTube;
using Google.YouTube;
using Video = App.Model.Video;

namespace App.Data
{
    public class VideoRepository : BaseRepository<Video>, IVideoRepository
    {

        public VideoRepository(AppDbContext dbContext)
            : base(dbContext)
        {
        }

        public IEnumerable<Video> MostPopular()
        {
            return GetVideos(YouTubeQuery.MostViewedVideo);
        }

        public Video GetByVideoId(string id)
        {
            return DbSet.FirstOrDefault(t => t.Id == id) ??
                   GetVideos(string.Format("https://gdata.youtube.com/feeds/api/videos/{0}?v=2", id)).FirstOrDefault();

        }

        public IEnumerable<Video> SearchVideos(string q)
        {

            var response = DownloadString(
                new WebClient(),
                string.Format("https://gdata.youtube.com/feeds/api/videos?q={0}&max-results=10&start-index=1&orderby=published&v=2", q),
                Encoding.UTF8);

            XDocument xDoc = XDocument.Parse(response);
            XNamespace ns = XNamespace.Get("http://www.w3.org/2005/Atom");
            XNamespace yt = XNamespace.Get("http://gdata.youtube.com/schemas/2007");
            XNamespace media = XNamespace.Get("http://search.yahoo.com/mrss/");
            return from entry in xDoc.Descendants(ns + "entry")
                   let grp = entry.Element(media + "group")
                   select new Video
                   {
                       Id = entry.Element(ns + "id").Value.Split(':').Last(),
                       Title = entry.Element(ns + "title").Value,
                       Seconds = int.Parse(grp.Element(yt + "duration").Attribute("seconds").Value)
                   };

        }

        public String DownloadString(WebClient webClient, String address, Encoding encoding)
        {
            var buffer = webClient.DownloadData(address);

            var bom = encoding.GetPreamble();

            if ((0 == bom.Length) || (buffer.Length < bom.Length))
            {
                return encoding.GetString(buffer);
            }

            for (int i = 0; i < bom.Length; i++)
            {
                if (buffer[i] != bom[i])
                {
                    return encoding.GetString(buffer);
                }
            }

            return encoding.GetString(buffer, bom.Length, buffer.Length - bom.Length);
        }

        public IEnumerable<Video> GetByUser(int userId)
        {
            return DbSet.Where(t => t.Playlists.Any(tt => tt.UserProfileId == userId)).ToList();

        }

        private static IEnumerable<Video> GetVideos(string videofeed)
        {
            var query = new YouTubeQuery(videofeed);
            return Map(GetVideos(query));
        }

        public static IEnumerable<Video> Map(IEnumerable<Google.YouTube.Video> items)
        {
            if (items == null)
                return null;
            return items.Select(t => new Video
                                         {
                                             Id = t.VideoId,
                                             Author = t.Author,
                                             Description = t.Description,
                                             Seconds = int.Parse(t.Media.Duration.Seconds),
                                             Title = t.Title
                                         });
        }

        private static IEnumerable<Google.YouTube.Video> GetVideos(YouTubeQuery q)
        {
            var request = GetRequest();
            Feed<Google.YouTube.Video> feed = null;

            try
            {
                feed = request.Get<Google.YouTube.Video>(q);
            }
            catch (GDataRequestException gdre)
            {
                var response = (HttpWebResponse)gdre.Response;
            }
            return feed != null ? feed.Entries : null;
        }

        public static YouTubeRequest GetRequest()
        {
            var settings = new YouTubeRequestSettings(
                ConfigurationManager.AppSettings["AppName"],
                ConfigurationManager.AppSettings["YoutubeDeveloperKey"])
                               {
                                   AutoPaging = true
                               };
            return new YouTubeRequest(settings);
        }

    }
}
