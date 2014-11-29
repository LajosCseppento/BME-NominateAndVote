using NominateAndVote.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NominateAndVote.RestService.Controllers
{
    [RoutePrefix("api/User")]
    public class AdminController : ApiController
    {
        private IDataManager dataManager;

        public AdminController()
            : base()
        {
            SimpleDataModel model = new SimpleDataModel();
            model.LoadSampleData();
            dataManager = new DataModelManager(model);
        }

        public AdminController(IDataManager dataManager)
            : base()
        {
            if (dataManager == null)
            {
                throw new ArgumentNullException("The data manager must not be null", "dataManager");
            }

            this.dataManager = dataManager;
        }

        [Route("Ban")]
        [HttpPost]
        public IHttpActionResult Ban(string id)
        {
            Guid idGuid = Guid.Empty;
            if (Guid.TryParse(id, out idGuid))
            {
                var user = dataManager.QueryUser(idGuid);
                user.IsBanned = true;
                return Ok(user);
            }
            else
            {
                return null;
            }
        }

        [Route("UnBan")]
        [HttpPost]
        public IHttpActionResult UnBan(string id)
        {
            Guid idGuid = Guid.Empty;
            if (Guid.TryParse(id, out idGuid))
            {
                var user = dataManager.QueryUser(idGuid);
                user.IsBanned = false;
                return Ok(user);
            }
            else
            {
                return null;
            }
        }
    }
}
