using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZPProductManagement.Application.Products;
using ZPProductManagement.Web.Infrastructure;

namespace ZPProductManagement.Web.Controllers
{
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly CreateProductApplication _createProductApplication;
        private readonly IUnitOfWork _uow;

        public ProductController(CreateProductApplication createProductApplication, IUnitOfWork uow)
        {
            _createProductApplication = createProductApplication;
            _uow = uow;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create()
        {
            var createProduct = new CreateProduct(Guid.NewGuid(), "Shoes", "Adidas Grand Court", "Color: White | Size: 42", 49.99M, 1, new List<string> { "avatar" });

            var result = await _createProductApplication.Execute(createProduct);

            if (result.Failure)
            {
                TempData["Failure"] = result.Message;

                return RedirectToAction("Index", "Product");
            }

            TempData["Success"] = "Product has been created";

            _uow.Commit();

            return RedirectToAction("Index", "Product");
        }
    }
}