using System;
using System.Collections.Generic;

namespace ZPProductManagement.Application.Products
{
    public class CreateProduct
    {
        public CreateProduct(Guid id, string categoryName, string name, string description, decimal price, int quantity, IEnumerable<string> fileNames)
        {
            Id = id;
            CategoryName = categoryName;
            Name = name;
            Description = description;
            Price = price;
            Quantity = quantity;
            FileNames = fileNames;
        }

        public Guid Id { get; }

        public string CategoryName { get; }

        public string Name { get; }

        public string Description { get; }

        public decimal Price { get; }

        public int Quantity { get; }

        public IEnumerable<string> FileNames { get; }
    }
}