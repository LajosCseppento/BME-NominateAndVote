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

        // GET: api/Poll/ClosedPolls
        [Route("ClosedPolls")]
        [HttpGet]
        public IEnumerable<Poll> GetClosedPolls()
        {
            return dataManager.QueryPolls(PollState.CLOSED);
        }

        // GET: api/Poll/NominationPolls
        [Route("NominationPolls")]
        [HttpGet]
        public IEnumerable<Poll> GetNominationPolls()
        {
            return QueryPolls(PollState.NOMINATION);
        }

        // GET: api/Poll/VotingPolls
        [Route("VotingPolls")]
        [HttpGet]
        public IEnumerable<Poll> GetVotingPolls()
        {
            return dataManager.QueryPolls(PollState.VOTING);
        }

        private List<Poll> QueryPolls(PollState state)
        {
            List<Poll> polls = dataManager.QueryPolls(state);

            // avoid circle references
            foreach (var poll in polls)
            {
                foreach (var nomination in poll.Nominations)
                {
                    nomination.Poll = null;
                    nomination.User = null;
                    nomination.Votes.Clear();
                }
            }

            return polls;
        }

        // GET: api/Poll/{id}
        [Route("Poll")]
        [HttpGet]
        public Poll Get(string id)
        {
            Guid idGuid = Guid.Empty;
            if (Guid.TryParse(id, out idGuid))
            {
                return dataManager.QueryPoll(idGuid);
            }
            else
            {
                return null;
            }
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
