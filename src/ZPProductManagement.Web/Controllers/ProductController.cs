using Microsoft.AspNetCore.Mvc;

namespace ZPProductManagement.Web.Controllers
{
    [Route("[controller]")]
    public class ProductController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
