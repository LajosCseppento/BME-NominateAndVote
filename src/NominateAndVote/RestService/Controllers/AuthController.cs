using NominateAndVote.DataModel;
using System.Web.Http;

namespace NominateAndVote.RestService.Controllers
{
    [RoutePrefix("api/Auth")]
    public class AuthController : BaseApiController
    {
        public AuthController()
        {
        }

        public AuthController(IDataManager dataManager)
            : base(dataManager)
        {
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