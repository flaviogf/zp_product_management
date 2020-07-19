using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZPProductManagement.Application.Products;
using ZPProductManagement.Web.Infrastructure;

namespace ZPProductManagement.Web.Controllers
{
    [Authorize]
    [Route("Product/{id}/archive")]
    public class ArchiveProductController : Controller
    {
        private readonly ArchiveProduct _archiveProduct;
        private readonly IUnitOfWork _uow;

        public ArchiveProductController(ArchiveProduct archiveProduct, IUnitOfWork uow)
        {
            _archiveProduct = archiveProduct;
            _uow = uow;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guid id)
        {
            var result = await _archiveProduct.Execute(id);

            if (result.Failure)
            {
                TempData["Failure"] = result.Message;

                _uow.Rollback();

                return RedirectToAction("Index", "Product");
            }

            TempData["Success"] = "Product has been archived";

            _uow.Commit();

            return RedirectToAction("Index", "Product");
        }
    }
}
