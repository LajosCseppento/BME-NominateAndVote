using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Model;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace NominateAndVote.RestService.Controllers
{
    [RoutePrefix("api/Polls")]
    public class PollsController : ApiController
    {
        private readonly IDataManager _dataManager;

        public PollsController()
        {
            // TODO Lali tablestorage / config alapján
            _dataManager = new MemoryDataManager(new DefaultDataModel());
        }

        public PollsController(IDataManager dataManager)
        {
            if (dataManager == null)
            {
                throw new ArgumentNullException("dataManager", "The data manager must not be null");
            }

            _dataManager = dataManager;
        }

        [Route("ClosedPolls")]
        [HttpGet]
        public IEnumerable<Poll> GetClosedPolls()
        {
            return _dataManager.QueryPolls(PollState.Closed);
        }

        [Route("Poll")]
        [HttpGet]
        public Poll Get(string id)
        {
            Guid idGuid;
            if (Guid.TryParse(id, out idGuid))
            {
                return _dataManager.QueryPoll(idGuid);
            }
            return null;
        }
    }
}