using NominateAndVote.DataModel;
using System;
using System.Web.Http;

namespace NominateAndVote.RestService.Controllers
{
    [RoutePrefix("api/Auth")]
    public class AuthController : ApiController
    {
        private readonly IDataManager _dataManager;

        public AuthController()
        {
            // TODO Lali tablestorage / config alapján
            _dataManager = new MemoryDataManager(new DefaultDataModel());
        }

        public AuthController(IDataManager dataManager)
        {
            if (dataManager == null)
            {
                throw new ArgumentNullException("dataManager", "The data manager must not be null");
            }

            _dataManager = dataManager;
        }

        [Route("Login")]
        [HttpPost]
        public IHttpActionResult Login()
        {
            // TODO Lali
            return Ok();

        }

        [Route("Logout")]
        [HttpPost]
        public IHttpActionResult Logout()
        {
            // TODO Lali
            return Ok();

        }
    }
}