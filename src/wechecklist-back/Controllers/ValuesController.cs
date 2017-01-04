using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using wechecklist_back.ORM;
using Microsoft.Extensions.Options;
using wechecklist_back.DataAccess.User;

namespace wechecklist_back.Controllers
{
    public class ValuesController : Controller
    {
        public ValuesController()
            :base()
        {
        }
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet]
        public string GetId(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post(int id, [FromBody]TokenCookie value)
        {
        }
    }
}
