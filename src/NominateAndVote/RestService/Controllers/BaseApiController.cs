using NominateAndVote.DataModel;
using System.Web.Http;

namespace NominateAndVote.RestService.Controllers
{
    public abstract class BaseApiController : ApiController
    {
        protected readonly IDataManager DataManager;

        public BaseApiController()
            : this(null)
        {
        }

        public BaseApiController(IDataManager dataManager)
        {
            if (dataManager != null)
            {
                DataManager = dataManager;
            }
            else
            {
                // TODO Csepi table storage configos csatlakozás
                DataManager = new MemoryDataManager(new DefaultDataModel());
            }
        }
    }
}