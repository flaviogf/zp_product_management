using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPProductManagement.Common;
using ZPProductManagement.Domain.Entities;
using ZPProductManagement.Domain.ValueObjects;

namespace ZPProductManagement.Application.Products
{
    public class CreateProduct
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IFileRepository _fileRepository;
        private readonly IProductRepository _productRepository;

        public CreateProduct(ICategoryRepository categoryRepository, IFileRepository fileRepository, IProductRepository productRepository)
        {
            _categoryRepository = categoryRepository;
            _fileRepository = fileRepository;
            _productRepository = productRepository;
        }

        public async Task<Result> Execute(IProductAdapter productAdapter)
        {
            var getProductOrError = await GetProductOrError(productAdapter);

            if (getProductOrError.Failure)
            {
                return Result.Fail(getProductOrError.Message);
            }

            var saveProductOrError = await SaveProductOrError(getProductOrError.Value);

            if (saveProductOrError.Failure)
            {
                return Result.Fail(saveProductOrError.Message);
            }

            return Result.Ok();
        }

        private async Task<Result<Product>> GetProductOrError(IProductAdapter productAdapter)
        {
            var maybeProduct = await _productRepository.FindById(productAdapter.Id);

            if (maybeProduct.HasValue)
            {
                return Result.Fail<Product>("ProductId is already taken");
            }

            var categoryOrError = await GetCategoryOrError(productAdapter.CategoryName);

            var filesOrError = await GetFilesOrErrors(productAdapter.FileNames);

            var idOrError = Identifier.Of(productAdapter.Id);

            var nameOrError = Name.Of(productAdapter.Name);

            var descriptionOrError = Description.Of(productAdapter.Description);

            var priceOrError = Price.Of(productAdapter.Price);

            var quantityOrError = Quantity.Of(productAdapter.Quantity);

            var result = Result.Combine
            (
                Result.Combine(idOrError, categoryOrError, nameOrError, descriptionOrError, priceOrError, quantityOrError),
                Result.Combine(filesOrError)
            );

            if (result.Failure)
            {
                return Result.Fail<Product>(result.Message);
            }

            var files = filesOrError.Select(it => it.Value);

            var product = new Product(idOrError.Value, categoryOrError.Value, nameOrError.Value, descriptionOrError.Value, priceOrError.Value, quantityOrError.Value, files);

            return Result.Ok(product);
        }

        private async Task<Result<Category>> GetCategoryOrError(string categoryName)
        {
            var maybeCategory = await _categoryRepository.FindByName(categoryName);

            if (maybeCategory.HasNoValue)
            {
                return Result.Fail<Category>("Category does not exist");
            }

            var categoryAdapter = maybeCategory.Value;

            var idOrError = Identifier.Of(categoryAdapter.Id);

            var nameOrError = Name.Of(categoryAdapter.Name);

            var result = Result.Combine(idOrError, nameOrError);

            if (result.Failure)
            {
                return Result.Fail<Category>(result.Message);
            }

            var category = new Category(idOrError.Value, nameOrError.Value);

            return Result.Ok(category);
        }

        private async Task<IEnumerable<Result<File>>> GetFilesOrErrors(IEnumerable<string> fileNames)
        {
            var tasks = fileNames.Select(GetFileOrError);

            var results = await Task.WhenAll(tasks);

            return results;
        }

        private async Task<Result<File>> GetFileOrError(string fileName)
        {
            var maybeFile = await _fileRepository.FindByName(fileName);

            if (maybeFile.HasNoValue)
            {
                return Result.Fail<File>("File does not exist");
            }

            var fileAdapter = maybeFile.Value;

            var idOrError = Identifier.Of(fileAdapter.Id);

            var nameOrError = Name.Of(fileAdapter.Name);

            var pathOrError = Path.Of(fileAdapter.Path);

            var extensionOrError = Extension.Of(fileAdapter.Extension);

            var result = Result.Combine(idOrError, nameOrError, pathOrError, extensionOrError);

            if (result.Failure)
            {
                return Result.Fail<File>(result.Message);
            }

            var file = new File(idOrError.Value, nameOrError.Value, pathOrError.Value, extensionOrError.Value);

            return Result.Ok(file);
        }

        private async Task<Result<Product>> SaveProductOrError(Product product)
        {
            IProductAdapter productAdapter = new OutputProductAdapter(product);

            var result = await _productRepository.Save(productAdapter);

            if (result.Failure)
            {
                return Result.Fail<Product>(result.Message);
            }

            return Result.Ok(product);
        }
    }
}