using NominateAndVote.DataModel;
using System;
using System.Web.Http;

namespace NominateAndVote.RestService.Controllers
{
    [RoutePrefix("api/User")]
    public class AdminController : ApiController
    {
        private readonly IDataManager _dataManager;

        public AdminController()
            : base()
        {
            var model = new SimpleDataModel();
            model.LoadSampleData();
            _dataManager = new DataModelManager(model);
        }

        public AdminController(IDataManager dataManager)
            : base()
        {
            if (dataManager == null)
            {
                throw new ArgumentNullException("The data manager must not be null", "dataManager");
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