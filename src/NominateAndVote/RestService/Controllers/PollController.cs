using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Poco;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace NominateAndVote.RestService.Controllers
{
    [RoutePrefix("api/Poll")]
    public class PollController : BaseApiController
    {
        public PollController()
        {
        }

        public PollController(IDataManager dataManager)
            : base(dataManager)
        {
        }

        [Route("Get")]
        [HttpGet]
        public Poll Get(string pollId)
        {
            Guid id;
            if (Guid.TryParse(pollId, out id))
            {
                return DataManager.QueryPoll(id);
            }
            return null;
        }

        [Route("ListNominationPolls")]
        [HttpGet]
        public IEnumerable<Poll> ListNominationPolls()
        {
            return QueryPolls(PollState.Nomination);
        }

        [Route("ListVotingPolls")]
        [HttpGet]
        public IEnumerable<Poll> ListVotingPolls()
        {
            return QueryPolls(PollState.Voting);
        }

        [Route("ListClosedPolls")]
        [HttpGet]
        public IEnumerable<Poll> ListClosedPolls()
        {
            return DataManager.QueryPolls(PollState.Closed);
        }

        private IEnumerable<Poll> QueryPolls(PollState state)
        {
            var polls = DataManager.QueryPolls(state);

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

        [Route("SearchPollSubject")]
        [HttpPost]
        public IEnumerable<PollSubject> SearchPollSubject(string term)
        {
            return term.Length >= 4 ? DataManager.SearchPollSubjects(term) : new List<PollSubject>();
        }
    }
}