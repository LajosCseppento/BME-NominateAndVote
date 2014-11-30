using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NominateAndVote.RestService.Controllers
{
    [RoutePrefix("api/Polls")]
    public class PollsController : ApiController
    {
        private IDataManager dataManager;

        public PollsController()
            : base()
        {
            SimpleDataModel model = new SimpleDataModel();
            model.LoadSampleData();
            dataManager = new DataModelManager(model);
        }

        public PollsController(IDataManager dataManager)
            : base()
        {
            if (dataManager == null)
            {
                throw new ArgumentNullException("The data manager must not be null", "dataManager");
            }

            this.dataManager = dataManager;
        }

        [Route("ClosedPolls")]
        [HttpGet]
        public IEnumerable<Poll> GetClosedPolls()
        {
            return dataManager.QueryPolls(PollState.CLOSED);
        }

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
    }
}
