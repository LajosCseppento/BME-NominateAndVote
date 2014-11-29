using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Model;
using System.Collections.Generic;
using System.Web.Http;

namespace RestService.Controllers
{
    public class NewsController : ApiController
    {
        private IDataManager dataManager;

        public NewsController()
            : base()
        {
            // a modellt ne nagyon haszbált, csak azt, amid a datamanager enged elérni! így csak kicseréljük pár helyen és megy majd a felhővel is. Msot a te adataiddal dolgozik
            SimpleDataModel model = new SimpleDataModel();
            model.LoadSampleData();
            dataManager = new DataModelManager(model);
        }

        // GET: api/News
        public IEnumerable<News> Get()
        {
            return dataManager.QueryNews();
        }

        // GET: api/News/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/News
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/News/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/News/5
        public void Delete(int id)
        {
        }
    }
}