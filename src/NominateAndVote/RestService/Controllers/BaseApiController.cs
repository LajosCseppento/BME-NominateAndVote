using NominateAndVote.DataModel;
using System.Web.Http;

namespace NominateAndVote.RestService.Controllers
{
    public abstract class BaseApiController : ApiController
    {
        protected readonly IDataManager _dataManager;

        public BaseApiController()
            : this(null)
        {
        }

        public BaseApiController(IDataManager dataManager)
        {
            if (dataManager != null)
            {
                _dataManager = dataManager;
            }
            else
            {
                // TODO Csepi table storage configos csatlakozás
                _dataManager = new MemoryDataManager(new DefaultDataModel());
            }
        }
    }
}