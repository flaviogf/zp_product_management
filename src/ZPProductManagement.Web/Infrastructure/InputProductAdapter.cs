using System;
using System.Collections.Generic;
using ZPProductManagement.Application.Products;

namespace ZPProductManagement.Web.Infrastructure
{
    public class InputProductAdapter : IProductAdapter
    {
        public InputProductAdapter
        (
            Guid id,
            string name,
            string description,
            decimal price,
            int quantity,
            Guid? categoryId = default,
            string categoryName = default,
            IEnumerable<Guid> fileIds = default,
            IEnumerable<string> fileNames = default
        )
        {
            Id = id;
            CategoryId = Guid.Empty;
            Name = name;
            Description = description;
            Price = price;
            Quantity = quantity;

            CategoryId = categoryId ?? Guid.Empty;
            CategoryName = categoryName ?? string.Empty;
            FileIds = fileIds ?? new List<Guid>();
            FileNames = fileNames ?? new List<string>();
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