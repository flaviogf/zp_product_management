using System.Collections.Generic;
using ZPProductManagement.Common.Enums;
using ZPProductManagement.Domain.Entities;
using ZPProductManagement.Domain.ValueObjects;

namespace ZPProductManagement.Application.Products
{
    public class ProductFactory
    {
        public static Product Create(Identifier id, Category category, Name name, Description description, Price price, Quantity quantity, IEnumerable<File> files, EStatus status)
        {
            return status switch
            {
                EStatus.Activated => new ActivatedProduct(
                    id,
                    category,
                    name,
                    description,
                    price,
                    quantity,
                    files
                ),
                EStatus.Archived => new ArchivedProduct(
                    id,
                    category,
                    name,
                    description,
                    price,
                    quantity,
                    files
                ),
                EStatus.Deleted => new DeletedProduct(
                    id,
                    category,
                    name,
                    description,
                    price,
                    quantity,
                    files
                ),
                _ => new Product
                (
                    id,
                    category,
                    name,
                    description,
                    price,
                    quantity,
                    files
                ),
            };
        }
    }
}
