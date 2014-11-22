using MVCWebExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MVCWebExample.Controllers
{
    public class UserController : ApiController
    {
        public IEnumerable<User> GetAllFacts()
        {
            return facts;
        }

        public User getUserByID(int id)
        {
            User fact = (from f in facts where f.ID == id select f).FirstOrDefault();
            if (fact == null)
                throw new HttpResponseException(this.Request.CreateResponse(HttpStatusCode.NotFound));

            return fact;
        }

        public HttpResponseMessage PostFact(User user)
        {
            if (!this.ModelState.IsValid)
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest));

            facts.Add(user);
            HttpResponseMessage response = this.Request.CreateResponse(HttpStatusCode.Created, user);
            response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = user.ID }));

            return response;
        }

        private static List<User> facts = new List<User>(){
            new User{ID=1, Token="aa", isBanned=false},
            new User{ID=2, Token="ab", isBanned=false},
            new User{ID=3, Token="ccc", isBanned=true},
        };
    }

    
}
