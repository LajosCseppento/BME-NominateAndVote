using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Tests;
using System;
using System.Web.Http;

namespace NominateAndVote.RestService.Controllers
{
    [RoutePrefix("api/User")]
    public class AdminController : ApiController
    {
        private readonly IDataManager _dataManager;

        public AdminController()
        {
            // TODO Lali tablestorage / config alapján
            _dataManager = new SampleDataModel().CreateDataManager();
        }

        public AdminController(IDataManager dataManager)
        {
            if (dataManager == null)
            {
                throw new ArgumentNullException("dataManager", "The data manager must not be null");
            }

            _dataManager = dataManager;
        }

        [Route("Ban")]
        [HttpPost]
        public IHttpActionResult Ban(string id)
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

        [Route("UnBan")]
        [HttpPost]
        public IHttpActionResult UnBan(string id)
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