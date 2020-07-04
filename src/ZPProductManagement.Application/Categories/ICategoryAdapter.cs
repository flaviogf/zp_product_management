using System;

namespace ZPProductManagement.Application.Categories
{
    public interface ICategoryAdapter
    {
        Guid Id { get; }

        string Name { get; }
    }
}