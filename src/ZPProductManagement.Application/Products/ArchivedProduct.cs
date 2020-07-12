using System.Collections.Generic;
using ZPProductManagement.Domain.Entities;
using ZPProductManagement.Domain.ValueObjects;

namespace ZPProductManagement.Application.Products
{
    internal class ArchivedProduct : Product
    {
        public ArchivedProduct
        (
            Identifier id,
            Category category,
            Name name,
            Description description,
            Price price,
            Quantity quantity,
            IEnumerable<File> files
        ) : base
        (
            id,
            category,
            name,
            description,
            price,
            quantity,
            files
        )
        {
            Status = Archived;
        }
    }
}