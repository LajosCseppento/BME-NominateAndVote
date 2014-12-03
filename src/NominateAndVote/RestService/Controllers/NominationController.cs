using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Poco;
using NominateAndVote.RestService.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace NominateAndVote.RestService.Controllers
{
    [RoutePrefix("api/Nomination")]
    public class NominationController : BaseApiController
    {
        public NominationController()
        {
        }

        public NominationController(IDataManager dataManager)
            : base(dataManager)
        {
        }

        [Route("GetForUser")]
        [HttpGet]
        public IEnumerable<Nomination> GetForUser(string userId)
        {
            long id;
            if (long.TryParse(userId, out id))
            {
                var user = DataManager.QueryUser(id);
                var nominations = DataManager.QueryNominations(user);
                if (nominations != null)
                {
                    return nominations;
                }
                return null;
            }
            return null;
        }

        [Route("Save")]
        [HttpPost]
        public IHttpActionResult Save(SaveNominationBindingModel saveNominationBindingModel)
        {
            if (saveNominationBindingModel == null)
            {
                return BadRequest("No data");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nomination = saveNominationBindingModel.ToPoco();
            DataManager.SaveNomination(nomination);

            return Ok(nomination);
        }

        [Route("Delete")]
        [HttpDelete]
        public bool Delete(string pollId, string nominationId)
        {
            Guid id, idPoll;
            if (Guid.TryParse(nominationId, out id) && Guid.TryParse(pollId, out idPoll))
            {
                DataManager.DeleteNomination(new Nomination { Id = id, Poll = new Poll { Id = idPoll }, User = new User(), Subject = new PollSubject() });
                return true;
            }
            return false;
        }
    }
}