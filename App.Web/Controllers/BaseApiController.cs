using System.Web.Http;
using App.Data.Contracts;
using App.Model;

namespace App.Web.Controllers
{
    public class BaseApiController : ApiController
    {
        protected IAppUow Uow { get; set; }


        protected override void Initialize(System.Web.Http.Controllers.HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            var userName = User.Identity.Name;
            CurrentUser = Uow.UserProfiles.GetByUserName(userName);

        }

        public UserProfile CurrentUser { get; set; }
    }
}
