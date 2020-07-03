using System;
using System.Collections.Generic;

namespace ZPProductManagement.Domain
{
    public class Product
    {
        public Product
        (
            Guid id,
            Category category,
            string name,
            string description,
            decimal price,
            uint quantity,
            IReadOnlyList<File> files
        )
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

        public Category Category { get; }

        public string Name { get; }

        public string Description { get; }

        public decimal Price { get; }

        public uint Quantity { get; }

        public IReadOnlyList<File> Files { get; }
    }
}
