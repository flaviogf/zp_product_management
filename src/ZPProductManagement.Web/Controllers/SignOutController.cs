using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZPProductManagement.Web.Infrastructure;

namespace ZPProductManagement.Web.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class SignOutController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public SignOutController(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Destroy()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Create", "SignIn");
        }
    }
}
