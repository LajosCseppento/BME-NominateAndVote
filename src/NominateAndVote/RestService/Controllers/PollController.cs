using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Model;
using NominateAndVote.DataModel.Tests;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace NominateAndVote.RestService.Controllers
{
    [RoutePrefix("api/Poll")]
    public class PollController : ApiController
    {
        private readonly IDataManager _dataManager;

        public PollController()
        {
            // TODO Lali tablestorage / config alapján
            _dataManager = new SampleDataModel().CreateDataManager();
        }

        public PollController(IDataManager dataManager)
        {
            if (dataManager == null)
            {
                throw new ArgumentNullException("dataManager", "The data manager must not be null");
            }

            _dataManager = dataManager;
        }

        [Route("NominationPolls")]
        [HttpGet]
        public IEnumerable<Poll> GetNominationPolls()
        {
            return QueryPolls(PollState.Nomination);
        }

        [Route("VotingPolls")]
        [HttpGet]
        public IEnumerable<Poll> GetVotingPolls()
        {
            return _dataManager.QueryPolls(PollState.Voting);
        }

        private IEnumerable<Poll> QueryPolls(PollState state)
        {
            var polls = _dataManager.QueryPolls(state);

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
    }
}