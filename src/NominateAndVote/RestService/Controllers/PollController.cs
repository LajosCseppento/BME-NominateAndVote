using NominateAndVote.DataModel.Model;
using System.Collections.Generic;
using System.Web.Http;

namespace RestService.Controllers
{
    public class PollController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // GET api/<controller>/5
        public Poll Get2(int id)
        {
            //DataModel d=new DataModel();
            //return d.Polls[id];
            return null;
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}