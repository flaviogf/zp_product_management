using System;
using System.Collections.Generic;
using System.Linq;
using ZPProductManagement.Domain.Entities;

namespace ZPProductManagement.Application.Products
{
    public class CreatedProduct
    {
        private readonly Product _product;

        internal CreatedProduct(Product product)
        {
            _product = product;
        }

        public Guid Id => _product.Id;

        public Guid CategoryId => _product.Category.Id;

        public string Name => _product.Name;

        public string Description => _product.Description;

        public decimal Price => _product.Price;

        public int Quantity => _product.Quantity;

        public IEnumerable<(Guid ProductId, Guid FileId)> Files => _product.Files.Select((it) => (ProductId: (Guid)_product.Id, FileId: (Guid)it.Id));
    }
}