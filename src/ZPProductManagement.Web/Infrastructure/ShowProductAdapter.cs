using System;
using System.Collections.Generic;
using ZPProductManagement.Application.Files;
using ZPProductManagement.Application.Products;

namespace ZPProductManagement.Web.Infrastructure
{
    public class ShowProductAdapter : IProductAdapter
    {
        public ShowProductAdapter
        (
            Guid id,
            string name,
            string description,
            decimal price,
            int quantity,
            Guid categoryId,
            string categoryName,
            IEnumerable<IFileAdapter> files
        )
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Quantity = quantity;
            CategoryId = categoryId;
            CategoryName = categoryName;
            Files = files;
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

        public IEnumerable<IFileAdapter> Files { get; }
    }
}