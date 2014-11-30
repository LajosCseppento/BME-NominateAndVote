using NominateAndVote.DataModel;
using NominateAndVote.RestService.Models;
using System;
using System.Web.Http;

namespace NominateAndVote.RestService.Controllers
{
    [RoutePrefix("api/PollAdmin")]
    public class PollAdminController : ApiController
    {
        private readonly IDataManager _dataManager;

        public PollAdminController()
        {
            // TODO Lali tablestorage / config alapján
            _dataManager = new MemoryDataManager(new DefaultDataModel());
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
        public IHttpActionResult Save(SavePollBindingModel savePollBindingModel)
        {
            if (savePollBindingModel == null)
            {
                return BadRequest("No data");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var poll = savePollBindingModel.ToPoco();

            _dataManager.SavePoll(poll);

            return Ok(poll);
        }
    }
}