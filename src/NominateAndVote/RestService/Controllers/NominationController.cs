using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Model;
using NominateAndVote.RestService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NominateAndVote.RestService.Controllers
{
     [RoutePrefix("api/Nomination")]
    public class NominationController : ApiController
    {
        private IDataManager dataManager;

        public NominationController()
            : base()
        {
            SimpleDataModel model = new SimpleDataModel();
            model.LoadSampleData();
            dataManager = new DataModelManager(model);
        }

        public NominationController(IDataManager dataManager)
            : base()
        {
            if (dataManager == null)
            {
                throw new ArgumentNullException("The data manager must not be null", "dataManager");
            }

            this.dataManager = dataManager;
        }

        [Route("User")]
        [HttpGet]
        public List<Nomination> Get(string id)
        {
            Guid idGuid = Guid.Empty;
            if (Guid.TryParse(id, out idGuid))
            {
                User user = dataManager.QueryUser(idGuid);
                return dataManager.QueryNominations(user);
            }
            else
            {
                return null;
            }
        }

        [Route("Save")]
        [HttpPost]
        public IHttpActionResult Save(NominationBindingModel newsBindingModel)
        {
            if (newsBindingModel == null)
            {
                return BadRequest("No data");
            }
            else if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Nomination nomination = newsBindingModel.ToPoco();
            if (nomination.ID.Equals(Guid.Empty))
            {
                nomination.ID = Guid.NewGuid();
            }
            else
            {
                Poll poll = dataManager.QueryPoll(nomination.Poll.ID);
                User user= dataManager.QueryUser(nomination.User.ID);
                List<Nomination> nominations=dataManager.QueryNominations(poll, user);
                Nomination oldNomination;
                foreach(Nomination n in nominations){
                    if(nomination.ID==nomination.ID){
                        oldNomination=nomination;
                    }
                }
            }

            dataManager.SaveNomination(nomination);

            return Ok(nomination);
        }

        [Route("Delete")]
        [HttpGet]
        public void Delete(string id)
        {
            Guid idGuid = Guid.Empty;
            if (Guid.TryParse(id, out idGuid))
            {
               dataManager.DeleteNomination(idGuid);
            }
        }


    }
}
