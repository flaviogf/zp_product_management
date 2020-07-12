using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ZPProductManagement.Application;
using ZPProductManagement.Application.Products;
using ZPProductManagement.Common;
using ZPProductManagement.Web.Infrastructure;
using ZPProductManagement.Web.ViewModels;

namespace ZPProductManagement.Web.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly CreateProduct _createProduct;
        private readonly DeleteProduct _deleteProduct;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ProductController(CreateProduct createProduct, DeleteProduct deleteProduct, IProductRepository productRepository, IUnitOfWork uow, IMapper mapper)
        {
            _createProduct = createProduct;
            _deleteProduct = deleteProduct;
            _productRepository = productRepository;
            _uow = uow;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] int page = 1)
        {
            var result = await _productRepository.Pagination(page, perPage: 5);

            var products = _mapper.Map<IEnumerable<IndexProductViewModel>>(result.Content);

            var pagination = new Pagination<IndexProductViewModel>(products, result.Total, result.Page, result.Pages);

            return View(pagination);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] CreateProductViewModel viewModel)
        {
            var createProducts = Create(viewModel.File);

            var results = await Task.WhenAll(createProducts);

            var result = Result.Combine(results);

            if (result.Failure)
            {
                TempData["Failure"] = result.Message;

                _uow.Rollback();

                return RedirectToAction("Index", "Product");
            }

            TempData["Success"] = "Product has been created";

            _uow.Commit();

            return RedirectToAction("Index", "Product");
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Show([FromRoute] Guid id)
        {
            var maybeProduct = await _productRepository.FindById(id);

            if (maybeProduct.HasNoValue)
            {
                return RedirectToAction("Index", "Product");
            }

            var product = _mapper.Map<ShowProductViewModel>(maybeProduct.Value);

            return View(product);
        }

        [HttpPost]
        [Route("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _deleteProduct.Execute(id);

            if (result.Failure)
            {
                TempData["Failure"] = result.Message;

                _uow.Rollback();

                return RedirectToAction("Index", "Product");
            }

            TempData["Success"] = "Product has been deleted";

            _uow.Commit();

            return RedirectToAction("Index", "Product");
        }

        private IEnumerable<Task<Result>> Create(IFormFile file)
        {
            using var stream = file.OpenReadStream();

            using var reader = new StreamReader(stream);

            string line;

            while ((line = reader.ReadLine()) != null)
            {
                yield return Create(line);
            }
        }

        private async Task<Result> Create(string line)
        {
            var fields = line.Split(',');

            var maybeId = GetId(fields);

            if (!maybeId.HasValue)
            {
                return Result.Fail("Id must be informed");
            }

            var maybeName = GetName(fields);

            if (!maybeName.HasValue)
            {
                return Result.Fail("Name must be informed");
            }

            var maybeDescription = GetDescription(fields);

            if (!maybeDescription.HasValue)
            {
                return Result.Fail("Description must be informed");
            }

            var maybePrice = GetPrice(fields);

            if (!maybePrice.HasValue)
            {
                return Result.Fail("Price must be informed");
            }

            var maybeQuantity = GetQuantity(fields);

            if (!maybeQuantity.HasValue)
            {
                return Result.Fail("Quantity must be informed");
            }

            var maybeCategoryName = GetCategoryName(fields);

            if (!maybeCategoryName.HasValue)
            {
                return Result.Fail("CategoryName must be informed");
            }

            var maybeFileNames = GetFileNames(fields);

            if (!maybeFileNames.HasValue)
            {
                return Result.Fail("FileNames must be informed");
            }

            var productAdapter = new CreateProductAdapter
            (
                maybeId.Value,
                maybeName.Value,
                maybeDescription.Value,
                maybePrice.Value,
                maybeQuantity.Value,
                maybeCategoryName.Value,
                maybeFileNames.Value
            );

            var result = await _createProduct.Execute(productAdapter);

            return result;
        }

        private Guid? GetId(string[] fields)
        {
            if (Guid.TryParse(fields.ElementAtOrDefault(0), out var result))
            {
                return result;
            }

            return null;
        }

        private Maybe<string> GetName(string[] fields)
        {
            return fields.ElementAtOrDefault(1);
        }

        private Maybe<string> GetDescription(string[] fields)
        {
            return fields.ElementAtOrDefault(2);
        }

        private decimal? GetPrice(string[] fields)
        {
            if (decimal.TryParse(fields.ElementAtOrDefault(3), out var result))
            {
                return result;
            }

            return null;
        }

        private int? GetQuantity(string[] fields)
        {
            if (int.TryParse(fields.ElementAtOrDefault(4), out var result))
            {
                return result;
            }

            return null;
        }

        private Maybe<string> GetCategoryName(string[] fields)
        {
            return fields.ElementAtOrDefault(5);
        }

        private Maybe<string[]> GetFileNames(string[] fields)
        {
            Maybe<string> field = fields.ElementAtOrDefault(6);

            if (!field.HasValue)
            {
                return null;
            }

            var value = field.Value;

            return value.Split(' ');
        }
    }
}