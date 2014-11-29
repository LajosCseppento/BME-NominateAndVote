using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Model;
using System.Collections.Generic;
using System.Web.Http;

namespace RestService.Controllers
{
    [RoutePrefix("api/Poll")]
    public class PollController : ApiController
    {
        private IDataManager dataManager;

        public PollController()
            : base()
        {
            // a modellt ne nagyon haszbált, csak azt, amid a datamanager enged elérni! így csak kicseréljük pár helyen és megy majd a felhővel is. Msot a te adataiddal dolgozik
            SimpleDataModel model = new SimpleDataModel();
            model.LoadSampleData();
            dataManager = new DataModelManager(model);
        }

        // GET: api/Poll/ClosedPolls
        [Route("ClosedPolls")]
        public IEnumerable<Poll> GetClosedPolls()
        {
            return dataManager.QueryPolls(PollState.CLOSED);
        }

        // GET: api/Poll/NominationPolls
        [Route("NominationPolls")]
        public IEnumerable<Poll> GetNominationPolls()
        {
            return QueryPolls(PollState.NOMINATION);
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

        // GET: api/Poll/VotingPolls
        [Route("VotingPolls")]
        public IEnumerable<Poll> GetVotingPolls()
        {
            return dataManager.QueryPolls(PollState.VOTING);
        }

        // GET: api/Poll/{id}
        public Poll Get(int id)
        {
            /*foreach (Poll p in dataManager.QueryPolls()) {
                if (p.ID.Equals(id))
                {
                    return p;
                }
            }*/
            return dataManager.QueryPolls()[id];
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}