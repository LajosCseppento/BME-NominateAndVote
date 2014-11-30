using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Model;
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
            : base()
        {
            var model = new SimpleDataModel();
            model.LoadSampleData();
            _dataManager = new DataModelManager(model);
        }

        public NewsController(IDataManager dataManager)
            : base()
        {
            if (dataManager == null)
            {
                throw new ArgumentNullException("The data manager must not be null", "dataManager");
            }

            _dataManager = dataManager;
        }

        public IEnumerable<News> Get()
        {
            return _dataManager.QueryNews();
        }
    }
}