using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using App.Data.Contracts;
using App.Model;

namespace App.Web.Controllers
{
    public class VideosController : BaseApiController
    {
        public VideosController(IAppUow uow)
        {
            Uow = uow;
        }


        public IEnumerable<Video> Get()
        {
            return CurrentUser == null ? Uow.Videos.MostPopular().Take(20) : Uow.Videos.GetByUser(CurrentUser.Id);
        }
        [HttpGet]
        public IEnumerable<Video> Search(string q)
        {
            return Uow.Videos.SearchVideos(q);
        }

        public Video Get(string id)
        {
            return Uow.Videos.GetByVideoId(id);
        }

    }
}