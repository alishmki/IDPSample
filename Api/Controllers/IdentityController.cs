using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    
    [Authorize]
    public class IdentityController : ControllerBase
    {
        [HttpGet]
        [Route("identity")]
        public IActionResult Get()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }

        [HttpGet]
        [Route("get1")]
        public IActionResult Get1()
        {
            return new JsonResult(new [] { User.Identity });
        }


        [HttpGet]
        [Route("get2")]
        public IActionResult Get2()
        {
            return new JsonResult(new[] { User.IsInRole("admin") });
        }

    }
}