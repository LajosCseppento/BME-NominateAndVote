using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Poco;
using NominateAndVote.RestService.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace NominateAndVote.RestService.Controllers
{
    [RoutePrefix("api/Nomination")]
    public class NominationController : ApiController
    {
        private readonly IDataManager _dataManager;

        public NominationController()
        {
            // TODO Lali tablestorage / config alapján
            _dataManager = new MemoryDataManager(new DefaultDataModel());
        }

        public NominationController(IDataManager dataManager)
        {
            if (dataManager == null)
            {
                throw new ArgumentNullException("dataManager", "The data manager must not be null");
            }

            _dataManager = dataManager;
        }

        [Route("GetForUser")]
        [HttpGet]
        public IEnumerable<Nomination> GetForUser(string userId)
        {
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