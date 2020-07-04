using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPProductManagement.Common;
using ZPProductManagement.Domain.Entities;
using ZPProductManagement.Domain.ValueObjects;

namespace ZPProductManagement.Application.Products
{
    public class CreateProductApplication
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IFileRepository _fileRepository;
        private readonly IProductRepository _productRepository;

        public CreateProductApplication(ICategoryRepository categoryRepository, IFileRepository fileRepository, IProductRepository productRepository)
        {
            _categoryRepository = categoryRepository;
            _fileRepository = fileRepository;
            _productRepository = productRepository;
        }

        public async Task<Result> Execute(CreateProduct createProduct)
        {
            var productOrError = await GetProduct(createProduct);

            if (productOrError.Failure)
            {
                return Result.Fail(productOrError.Message);
            }

            var createdProductOrError = await SaveProduct(productOrError.Value);

            if (createdProductOrError.Failure)
            {
                return Result.Fail(createdProductOrError.Message);
            }

            return Result.Ok();
        }

        private async Task<Result<Product>> GetProduct(CreateProduct createProduct)
        {
            var maybeStoredProduct = await _productRepository.FindById(createProduct.Id);

            if (maybeStoredProduct.HasValue)
            {
                return Result.Fail<Product>("ProductId is already taken");
            }

            var maybeStoredCategory = await _categoryRepository.FindByName(createProduct.CategoryName);

            if (maybeStoredCategory.HasNoValue)
            {
                return Result.Fail<Product>("Category does not exist");
            }

            var idOrError = Identifier.Of(createProduct.Id);

            var categoryOrError = maybeStoredCategory.Value.ToCategory();

            var nameOrError = Name.Of(createProduct.Name);

            var descriptionOrError = Description.Of(createProduct.Description);

            var priceOrError = Price.Of(createProduct.Price);

            var quantityOrError = Quantity.Of(createProduct.Quantity);

            var filesOrError = await GetFiles(createProduct.FileNames);

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

        private async Task<IEnumerable<Result<File>>> GetFiles(IEnumerable<string> fileNames)
        {
            var tasks = fileNames.Select(GetFile);

            var results = await Task.WhenAll(tasks);

            return results;
        }

        private async Task<Result<File>> GetFile(string fileName)
        {
            var maybeFile = await _fileRepository.FindByName(fileName);

            if (maybeFile.HasNoValue)
            {
                return Result.Fail<File>("File does not exist");
            }

            var storedFile = maybeFile.Value;

            var result = storedFile.ToFile();

            return result;
        }

        private async Task<Result<CreatedProduct>> SaveProduct(Product product)
        {
            var createdProduct = new CreatedProduct(product);

            var result = await _productRepository.Save(createdProduct);

            if (result.Failure)
            {
                return Result.Fail<CreatedProduct>(result.Message);
            }

            return Result.Ok(createdProduct);
        }
    }
}