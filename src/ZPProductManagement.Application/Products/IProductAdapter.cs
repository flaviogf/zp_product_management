using System;
using System.Collections.Generic;
using ZPProductManagement.Application.Files;
using ZPProductManagement.Common.Enums;

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

        IEnumerable<IFileAdapter> Files { get; }

        EStatus Status { get; }
    }
}
