using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Model;
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

        [Route("User")]
        [HttpGet]
        public List<Nomination> Get(string id)
        {
            Guid idGuid;
            if (Guid.TryParse(id, out idGuid))
            {
                var user = _dataManager.QueryUser(idGuid);
                return _dataManager.QueryNominations(user);
            }
            return null;
        }

        [Route("Save")]
        [HttpPost]
        public IHttpActionResult Save(NominationBindingModel nominationBindingModel)
        {
            if (nominationBindingModel == null)
            {
                return BadRequest("No data");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nomination = nominationBindingModel.ToPoco();
            _dataManager.SaveNomination(nomination);

            return Ok(nomination);
        }

        [Route("Delete")]
        [HttpDelete]
        public bool Delete(string id)
        {
            Guid idGuid;
            if (Guid.TryParse(id, out idGuid))
            {
                _dataManager.DeleteNomination(idGuid);
                return true;
            }
            return false;
        }
    }
}