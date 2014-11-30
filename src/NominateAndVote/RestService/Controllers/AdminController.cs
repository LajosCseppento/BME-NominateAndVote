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
        public IHttpActionResult BanUser(string id)
        {
            Guid idGuid;
            if (Guid.TryParse(id, out idGuid))
            {
                var user = _dataManager.QueryUser(idGuid);
                user.IsBanned = true;
                return Ok(user);
            }
            return null;
        }

        [Route("UnBanUser")]
        [HttpPost]
        public IHttpActionResult UnBanUser(string id)
        {
            Guid idGuid;
            if (Guid.TryParse(id, out idGuid))
            {
                var user = _dataManager.QueryUser(idGuid);
                user.IsBanned = false;
                return Ok(user);
            }
            return null;
        }
    }
}