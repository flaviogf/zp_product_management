using System;
using System.Collections.Generic;
using System.Linq;
using ZPProductManagement.Application.Files;
using ZPProductManagement.Application.Products;
using ZPProductManagement.Common.Enums;

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
            IEnumerable<IFileAdapter> files,
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
            Files = files;
            Status = status;
        }

        public Guid Id { get; }

        public string Name { get; }

        public string Description { get; }

        public decimal Price { get; }

        public int Quantity { get; }

        public Guid CategoryId { get; }

        public string CategoryName { get; }

        public IEnumerable<Guid> FileIds => Files.Select(it => it.Id);

        public IEnumerable<string> FileNames => Files.Select(it => it.Name);

        public IEnumerable<IFileAdapter> Files { get; }

        public EStatus Status { get; }
    }
}
