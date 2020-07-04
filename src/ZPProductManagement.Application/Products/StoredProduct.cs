using System;
using System.Collections.Generic;
using ZPProductManagement.Application.Categories;
using ZPProductManagement.Application.Files;

namespace ZPProductManagement.Application.Products
{
    public class StoredProduct
    {
        public StoredProduct(Guid id, StoredCategory category, string name, string description, decimal price, int quantity, IEnumerable<StoredFile> files)
        {
            Id = id;
            Category = category;
            Name = name;
            Description = description;
            Price = price;
            Quantity = quantity;
            Files = files;
        }

        public Guid Id { get; }

        public StoredCategory Category { get; }

        public string Name { get; }

        public string Description { get; }

        public decimal Price { get; }

        public int Quantity { get; }

        public IEnumerable<StoredFile> Files { get; }
    }
}