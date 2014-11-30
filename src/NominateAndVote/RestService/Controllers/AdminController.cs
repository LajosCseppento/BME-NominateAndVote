using NominateAndVote.DataModel;
using System;
using System.Web.Http;

namespace NominateAndVote.RestService.Controllers
{
    [RoutePrefix("api/Admin")]
    public class AdminController : ApiController
    {
        private readonly IDataManager _dataManager;

        public AdminController()
        {
            // TODO Lali tablestorage / config alapján
            _dataManager = new MemoryDataManager(new DefaultDataModel());
        }

        public AdminController(IDataManager dataManager)
        {
            if (dataManager == null)
            {
                throw new ArgumentNullException("dataManager", "The data manager must not be null");
            }

            _dataManager = dataManager;
        }

        [Route("BanUser")]
        [HttpPost]
        public IHttpActionResult BanUser(string userId)
        {
            long id;
            if (long.TryParse(userId, out id))
            {
                var user = _dataManager.QueryUser(id);
                user.IsBanned = true;
                _dataManager.SaveUser(user);

                return Ok(user);
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
                var user = _dataManager.QueryUser(id);

                if (user != null)
                {
                    user.IsBanned = false;
                    _dataManager.SaveUser(user);

                    return Ok(user);
                }
                else
                {
                    return NotFound();
                }
            }

            return NotFound();
        }
    }
}