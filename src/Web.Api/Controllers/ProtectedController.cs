using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using Web.Api.Infrastructure.Identity;

namespace Web.Api.Controllers
{
    [Authorize(Policy = "ApiUser")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProtectedController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;

        public ProtectedController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }
        // GET api/protected/home
        [HttpGet]
        public IActionResult Home()
        {
            var sid = User.Claims.Where(c => c.Type == "id")
                               .Select(c => c.Value).SingleOrDefault();
            var ApiUser = userManager.Users.FirstOrDefault(u => u.Id == sid);
            return new OkObjectResult(new { result = true });
        }
    }
}
