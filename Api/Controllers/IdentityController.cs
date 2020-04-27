using System.Linq;
using System.Security.Claims;
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
            var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
            var roles = claimsIdentity.Claims.ToList();           
            
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