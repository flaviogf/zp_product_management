using System;
using System.Collections.Generic;
using ZPProductManagement.Application.Files;
using ZPProductManagement.Application.Products;
using ZPProductManagement.Common.Enums;

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

        public Guid CategoryId => throw new NotImplementedException();

        public string CategoryName { get; }

        public IEnumerable<Guid> FileIds => throw new NotImplementedException();

        public IEnumerable<string> FileNames { get; }

        public IEnumerable<IFileAdapter> Files => throw new NotImplementedException();

        public EStatus Status => throw new NotImplementedException();
    }
}
