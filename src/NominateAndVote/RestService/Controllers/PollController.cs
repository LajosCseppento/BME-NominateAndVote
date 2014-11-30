using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Model;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace NominateAndVote.RestService.Controllers
{
    [RoutePrefix("api/Poll")]
    public class PollController : ApiController
    {
        private IDataManager dataManager;

        public PollController()
            : base()
        {
            SimpleDataModel model = new SimpleDataModel();
            model.LoadSampleData();
            dataManager = new DataModelManager(model);
        }

        public PollController(IDataManager dataManager)
            : base()
        {
            if (dataManager == null)
            {
                throw new ArgumentNullException("The data manager must not be null", "dataManager");
            }

            this.dataManager = dataManager;
        }

        [Route("NominationPolls")]
        [HttpGet]
        public IEnumerable<Poll> GetNominationPolls()
        {
            return QueryPolls(PollState.NOMINATION);
        }

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
    }
}