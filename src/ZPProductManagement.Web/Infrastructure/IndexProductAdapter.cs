using System;
using System.Collections.Generic;
using ZPProductManagement.Application.Files;
using ZPProductManagement.Application.Products;
using ZPProductManagement.Common.Enums;

namespace ZPProductManagement.Web.Infrastructure
{
    public class IndexProductAdapter : IProductAdapter
    {
        public IndexProductAdapter
        (
            Guid id,
            string name,
            string description,
            decimal price,
            int quantity,
            Guid categoryId,
            string categoryName,
            EStatus status
        )
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Quantity = quantity;
            CategoryId = categoryId;
            CategoryName = categoryName;
            Status = status;
        }

        public Guid Id { get; }

        public string Name { get; }

        public string Description { get; }

        public decimal Price { get; }

        public int Quantity { get; }

        public Guid CategoryId { get; }

        public string CategoryName { get; }

        public IEnumerable<Guid> FileIds => throw new NotImplementedException();

        public IEnumerable<string> FileNames => throw new NotImplementedException();

        public IEnumerable<IFileAdapter> Files => throw new NotImplementedException();

        public EStatus Status { get; }
    }
}
