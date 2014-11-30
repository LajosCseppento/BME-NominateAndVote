﻿using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Poco;
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
            _dataManager = new MemoryDataManager(new DefaultDataModel());
        }

        public PollController(IDataManager dataManager)
        {
            if (dataManager == null)
            {
                throw new ArgumentNullException("dataManager", "The data manager must not be null");
            }

            _dataManager = dataManager;
        }

        [Route("GetPoll")]
        [HttpGet]
        public Poll GetPoll(string pollId)
        {
            Guid id;
            if (Guid.TryParse(pollId, out id))
            {
                return _dataManager.QueryPoll(id);
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
            return _dataManager.QueryPolls(PollState.Closed);
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