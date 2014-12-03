using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Poco;
using System.Collections.Generic;
using System.Web.Http;

namespace NominateAndVote.RestService.Controllers
{
    [RoutePrefix("api/News")]
    public class NewsController : BaseApiController
    {
        public NewsController()
        {
        }

        public NewsController(IDataManager dataManager)
            : base(dataManager)
        {
        }

        [Route("List")]
        [HttpGet]
        public IEnumerable<News> List()
        {
            return _dataManager.QueryNews();
        }
    }
}