using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZPProductManagement.Application.Products;
using ZPProductManagement.Web.Infrastructure;

namespace ZPProductManagement.Web.Controllers
{
    [Authorize]
    [Route("Product/{id}/active")]
    public class ActiveProductController : Controller
    {
        private readonly ActiveProduct _activeProduct;
        private readonly IUnitOfWork _uow;

        public ActiveProductController(ActiveProduct activeProduct, IUnitOfWork uow)
        {
            _activeProduct = activeProduct;
            _uow = uow;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guid id)
        {
            var result = await _activeProduct.Execute(id);

            if (result.Failure)
            {
                TempData["Failure"] = result.Message;

                _uow.Rollback();

                return RedirectToAction("Index", "Product");
            }

            TempData["Success"] = "Product has been activated";

            _uow.Commit();

            return RedirectToAction("Index", "Product");
        }
    }
}
