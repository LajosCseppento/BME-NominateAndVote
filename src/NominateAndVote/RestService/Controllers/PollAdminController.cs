using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NominateAndVote.RestService.Models;

namespace NominateAndVote.RestService.Controllers
{
    [RoutePrefix("api/AdminPoll")]
    public class PollAdminController : ApiController
    {
        private IDataManager dataManager;

        public PollAdminController()
            : base()
        {
            SimpleDataModel model = new SimpleDataModel();
            model.LoadSampleData();
            dataManager = new DataModelManager(model);
        }

        public PollAdminController(IDataManager dataManager)
            : base()
        {
            if (dataManager == null)
            {
                throw new ArgumentNullException("The data manager must not be null", "dataManager");
            }

            this.dataManager = dataManager;
        }

        [Route("Save")]
        [HttpPost]
        public IHttpActionResult Save(PollBindingModell pollBindingModel)
        {
            if (pollBindingModel == null)
            {
                return BadRequest("No data");
            }
            else if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Poll poll = pollBindingModel.ToPoco();
            if (poll.ID.Equals(Guid.Empty))
            {
                poll.ID = Guid.NewGuid();
            }
            else
            {
                Poll oldPoll = dataManager.QueryPoll(poll.ID);
            }

            dataManager.SavePoll(poll);

            return Ok(poll);
        }

    }
}
