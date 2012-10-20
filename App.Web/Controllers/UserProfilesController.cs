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
    public class UserProfilesController : BaseApiController
    {
        public UserProfilesController(IAppUow uow)
        {
            Uow = uow;
        }


        public IEnumerable<UserProfile> Get()
        {
            return Uow.UserProfiles.GetAll();
        }

        [HttpGet]
        public UserProfile Current()
        {
            if (CurrentUser == null)
                return new UserProfile
                           {
                               Id = 0,
                               UserName = string.Empty
                           };
            return CurrentUser;
        }

    }
}