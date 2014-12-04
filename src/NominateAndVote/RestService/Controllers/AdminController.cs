using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Poco;
using System.Collections.Generic;
using System.Web.Http;

namespace NominateAndVote.RestService.Controllers
{
    [RoutePrefix("api/Admin")]
    public class AdminController : BaseApiController
    {
        public AdminController()
        {
        }

        public AdminController(IDataManager dataManager)
            : base(dataManager)
        {
        }

        [Route("SearchUser")]
        [HttpPost]
        public IEnumerable<User> SearchUser(string term)
        {
            return term.Length >= 4 ? DataManager.SearchUsers(term) : new List<User>();
        }

        [Route("BanUser")]
        [HttpPost]
        public IHttpActionResult BanUser(string userId)
        {
            long id;
            if (long.TryParse(userId, out id))
            {
                var user = DataManager.QueryUser(id);
                if (user != null)
                {
                    user.IsBanned = true;
                    DataManager.SaveUser(user);

                    return Ok(user);
                }
                return NotFound();
            }

            return NotFound();
        }

        [Route("UnBanUser")]
        [HttpPost]
        public IHttpActionResult UnBanUser(string userId)
        {
            long id;
            if (long.TryParse(userId, out id))
            {
                var user = DataManager.QueryUser(id);

                if (user != null)
                {
                    user.IsBanned = false;
                    DataManager.SaveUser(user);

                    return Ok(user);
                }
            }

            return NotFound();
        }
    }
}