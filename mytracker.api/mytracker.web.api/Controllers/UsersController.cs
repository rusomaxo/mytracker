using System.Collections.Generic;
using System.Web.Http;

namespace mytracker.web.api.Controllers
{
    public class UsersController : ApiController
    {
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", User.Identity.Name  };
        }

        // GET api/users/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/users
        public void Post([FromBody]string value)
        {
        }

        // PUT api/users/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/users/5
        public void Delete(int id)
        {
        }
    }
}
