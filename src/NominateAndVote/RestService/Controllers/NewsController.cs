using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Poco;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace NominateAndVote.RestService.Controllers
{
    [RoutePrefix("api/News")]
    public class NewsController : ApiController
    {
        private readonly IDataManager _dataManager;

        public NewsController()
        {
            // TODO Lali tablestorage / config alapján
            _dataManager = new MemoryDataManager(new DefaultDataModel());
        }

        public NewsController(IDataManager dataManager)
        {
            if (dataManager == null)
            {
                throw new ArgumentNullException("dataManager", "The data manager must not be null");
            }

            _dataManager = dataManager;
        }

        [Route("ListNews")]
        [HttpGet]
        public IEnumerable<News> ListNews()
        {
            return _dataManager.QueryNews();
        }
    }
}