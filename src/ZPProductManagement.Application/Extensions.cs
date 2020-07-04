using System.Linq;
using ZPProductManagement.Application.Categories;
using ZPProductManagement.Application.Files;
using ZPProductManagement.Application.Products;
using ZPProductManagement.Common;
using ZPProductManagement.Domain.Entities;
using ZPProductManagement.Domain.ValueObjects;

namespace ZPProductManagement.Application
{
    internal static class Extensions
    {
        internal static Result<Product> ToProduct(this StoredProduct target)
        {
            var idOrError = Identifier.Of(target.Id);

            var categoryOrError = ToCategory(target.Category);

            var nameOrError = Name.Of(target.Name);

            var descriptionOrError = Description.Of(target.Description);

            var priceOrError = Price.Of(target.Price);

            var quantityOrError = Quantity.Of(target.Quantity);

            var filesOrErrors = target.Files.Select(ToFile);

            var result = Result.Combine(
                Result.Combine(idOrError, categoryOrError, nameOrError, descriptionOrError, priceOrError, quantityOrError),
                Result.Combine(filesOrErrors)
            );

            if (result.Failure)
            {
                return Result.Fail<Product>(result.Message);
            }

            var files = filesOrErrors.Select(it => it.Value);

            var product = new Product(idOrError.Value, categoryOrError.Value, nameOrError.Value, descriptionOrError.Value, priceOrError.Value, quantityOrError.Value, files);

            return Result.Ok(product);
        }

        internal static Result<Category> ToCategory(this StoredCategory target)
        {
            var idOrError = Identifier.Of(target.Id);

            var nameOrError = Name.Of(target.Name);

            var result = Result.Combine(idOrError, nameOrError);

            if (result.Failure)
            {
                return Result.Fail<Category>(result.Message);
            }

            var category = new Category(idOrError.Value, nameOrError.Value);

            return Result.Ok(category);
        }

        internal static Result<File> ToFile(this StoredFile target)
        {
            var idOrError = Identifier.Of(target.Id);

            var nameOrError = Name.Of(target.Name);

            var pathOrError = Path.Of(target.Path);

            var extensionOrError = Extension.Of(target.Extension);

            var result = Result.Combine(idOrError, nameOrError, pathOrError, extensionOrError);

            if (result.Failure)
            {
                return Result.Fail<File>(result.Message);
            }

            var file = new File(idOrError.Value, nameOrError.Value, pathOrError.Value, extensionOrError.Value);

            return Result.Ok(file);
        }
    }
}