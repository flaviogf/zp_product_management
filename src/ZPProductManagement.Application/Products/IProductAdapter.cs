using System;
using System.Collections.Generic;

namespace ZPProductManagement.Application.Products
{
    public interface IProductAdapter
    {
        Guid Id { get; }

        string Name { get; }

        string Description { get; }

        decimal Price { get; }

        int Quantity { get; }

        Guid CategoryId { get; }

        string CategoryName { get; }

        IEnumerable<Guid> FileIds { get; }

        IEnumerable<string> FileNames { get; }
    }
}