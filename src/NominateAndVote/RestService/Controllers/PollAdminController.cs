using NominateAndVote.DataModel;
using NominateAndVote.RestService.Models;
using System.Web.Http;

namespace NominateAndVote.RestService.Controllers
{
    [RoutePrefix("api/PollAdmin")]
    public class PollAdminController : BaseApiController
    {
        public PollAdminController()
        {
        }

        public PollAdminController(IDataManager dataManager)
            : base(dataManager)
        {
        }

        [Route("Save")]
        [HttpPost]
        public IHttpActionResult Save(SavePollBindingModel savePollBindingModel)
        {
            // TODO Ági teszt invalid bejövő adatokra
            if (savePollBindingModel == null)
            {
                return BadRequest("No data");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var poll = savePollBindingModel.ToPoco();

            DataManager.SavePoll(poll);

            return Ok(poll);
        }
    }
}