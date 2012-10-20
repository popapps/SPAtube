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
    public class PlaylistsController : BaseApiController
    {
        public PlaylistsController(IAppUow uow)
        {
            Uow = uow;
        }


        public IEnumerable<Playlist> Get()
        {
            return CurrentUser == null ? new Playlist[0] : Uow.Playlists.GetByUser(CurrentUser.Id);
        }

        [Authorize]
        [HttpPost]
        public Playlist Post(Playlist entity)
        {
            entity.UserProfileId = CurrentUser.Id;
            Uow.Playlists.Add(entity);
            Uow.Commit();
            return entity;
        }

        [Authorize]
        [HttpDelete]
        public void Delete(int id)
        {
            Uow.Playlists.Delete(id);
            Uow.Commit();
        }
    }
}