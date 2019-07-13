using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using order_service.Domains;

namespace order_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        private readonly ISession session;

        public HealthController(ISession session)
        {
            this.session = session;
        }

        // GET api/health?check=all
        [HttpGet]
        public ActionResult<string> Get(string check = "all")
        {
            session.Query<TestObject>().ToList();
            return "Order Service is OK";
        }

        // GET api/values
        /*[HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] {"value1", "value2"};
        }*/

        // POST api/values
/*
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
*/

        // PUT api/values/5
        /*[HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }*/

        // DELETE api/values/5
        /*[HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}