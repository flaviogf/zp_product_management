using System;
using System.Collections.Generic;
using System.Linq;
using ZPProductManagement.Domain.Entities;

namespace ZPProductManagement.Application.Products
{
    internal class OutputProductAdapter : IProductAdapter
    {
        private readonly Product _product;

        public OutputProductAdapter(Product product)
        {
            _product = product;
        }

        public Guid Id => _product.Id;

        public string Name => _product.Name;

        public string Description => _product.Description;

        public decimal Price => _product.Price;

        public int Quantity => _product.Quantity;

        public Guid CategoryId => _product.Category.Id;

        public string CategoryName => _product.Category.Name;

        public IEnumerable<Guid> FileIds => _product.Files.Select(it => (Guid)it.Id);

        public IEnumerable<string> FileNames => _product.Files.Select(it => (string)it.Name);
    }
}