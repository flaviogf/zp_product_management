using System;
using System.Collections.Generic;
using ZPProductManagement.Application.Products;

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
            string categoryName
        )
        {
            Id = id;
            CategoryId = Guid.Empty;
            Name = name;
            Description = description;
            Price = price;
            Quantity = quantity;
            CategoryId = categoryId;
            CategoryName = categoryName;
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