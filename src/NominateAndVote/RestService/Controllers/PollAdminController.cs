using NominateAndVote.DataModel;
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
            : base()
        {
            var model = new SimpleDataModel();
            model.LoadSampleData();
            _dataManager = new DataModelManager(model);
        }

        public PollAdminController(IDataManager dataManager)
            : base()
        {
            if (dataManager == null)
            {
                throw new ArgumentNullException("The data manager must not be null", "dataManager");
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
            if (poll.Id.Equals(Guid.Empty))
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