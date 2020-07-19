using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZPProductManagement.Web.Infrastructure;
using ZPProductManagement.Web.ViewModels;

namespace ZPProductManagement.Web.Controllers
{
    [Route("[controller]")]
    public class SignInController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public SignInController(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] CreateSignInViewModel viewModel)
        {
            var result = await _signInManager.PasswordSignInAsync(viewModel.Email, viewModel.Password, isPersistent: true, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                TempData["Failure"] = "Wrong email or password";

                return View(viewModel);
            }

            return RedirectToAction("Index", "Product");
        }
    }
}
