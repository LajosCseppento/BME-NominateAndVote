using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Model;
using NominateAndVote.DataModel.Tests;
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
            _dataManager = new SampleDataModel().CreateDataManager();
        }

        public NewsController(IDataManager dataManager)
        {
            if (dataManager == null)
            {
                throw new ArgumentNullException("dataManager", "The data manager must not be null");
            }

            _dataManager = dataManager;
        }

        public IEnumerable<News> Get()
        {
            return _dataManager.QueryNews();
        }
    }
}