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
            // TODO Ági nem létező user???
            long id;
            if (long.TryParse(userId, out id))
            {
                var user = _dataManager.QueryUser(id);
                return _dataManager.QueryNominations(user);
            }
            return null;
        }

        [Route("Save")]
        [HttpPost]
        public IHttpActionResult Save(SaveNominationBindingModel saveNominationBindingModel)
        {
            // TODO Ági teszt invalid bejövő adatokra
            if (saveNominationBindingModel == null)
            {
                return BadRequest("No data");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nomination = saveNominationBindingModel.ToPoco();
            _dataManager.SaveNomination(nomination);

            return Ok(nomination);
        }

        [Route("Delete")]
        [HttpDelete]
        public bool Delete(string nominationId)
        {
            Guid id;
            if (Guid.TryParse(nominationId, out id))
            {
                _dataManager.DeleteNomination(id);
                return true;
            }
            return false;
        }
    }
}