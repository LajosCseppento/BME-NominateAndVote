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

        // GET: api/Poll/{id}
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

        // DELETE api/<controller>/5
        [Route("Delete")]
        [HttpGet]
        public void Delete(string id)
        {
            Guid idGuid = Guid.Empty;
            if (Guid.TryParse(id, out idGuid))
            {
               // dataManager.DeleteNomination(idGuid);
            }
        }


    }
}
