using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Client.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Client.DAL;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        TokenManager tokenManager = new TokenManager();

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        // Show the received tokens metadata, on the html index page, if authorized with a role of "administrator"
        [Authorize(Roles = "Admin")]
        public ActionResult ShowToken()
        {

            var tokenMetadata = User.Identity.GetType().MetadataToken.ToString();
            return View("Index", "The tokens metadata is: " + tokenMetadata);
        }

        public async Task Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("oidc");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
