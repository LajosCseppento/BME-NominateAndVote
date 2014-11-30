using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Tests;
using NominateAndVote.RestService.Models;
using System;
using System.Web.Http;

namespace NominateAndVote.RestService.Controllers
{
    [RoutePrefix("api/AdminPoll")]
    public class PollAdminController : ApiController
    {
        private readonly IDataManager _dataManager;

        public PollAdminController()
        {
            // TODO Lali tablestorage / config alapján
            _dataManager = new SampleDataModel().CreateDataManager();
        }

        public PollAdminController(IDataManager dataManager)
        {
            if (dataManager == null)
            {
                throw new ArgumentNullException("dataManager", "The data manager must not be null");
            }

            _dataManager = dataManager;
        }

        [Route("Save")]
        [HttpPost]
        public IHttpActionResult Save(PollBindingModell pollBindingModel)
        {
            if (pollBindingModel == null)
            {
                return BadRequest("No data");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var poll = pollBindingModel.ToPoco();
            if (poll.Id == Guid.Empty)
            {
                poll.Id = Guid.NewGuid();
            }
            else
            {
                var oldPoll = _dataManager.QueryPoll(poll.Id);
            }

            _dataManager.SavePoll(poll);

            return Ok(poll);
        }
    }
}