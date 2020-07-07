using System;
using System.Collections.Generic;
using ZPProductManagement.Application.Products;

namespace ZPProductManagement.Web.Infrastructure
{
    public class CreateProductAdapter : IProductAdapter
    {
        public CreateProductAdapter
        (
            Guid id,
            string name,
            string description,
            decimal price,
            int quantity,
            string categoryName,
            IEnumerable<string> fileNames
        )
        {
            Id = id;
            CategoryId = Guid.Empty;
            Name = name;
            Description = description;
            Price = price;
            Quantity = quantity;
            CategoryName = categoryName;
            FileNames = fileNames;
        }

        public Guid Id { get; }

        public string Name { get; }

        public string Description { get; }

        public decimal Price { get; }

        public int Quantity { get; }

        public Guid CategoryId { get; }

        public string CategoryName { get; }

        public IEnumerable<Guid> FileIds { get; }

        public IEnumerable<string> FileNames { get; }
    }
}