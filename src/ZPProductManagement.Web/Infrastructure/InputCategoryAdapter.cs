using System;
using ZPProductManagement.Application.Categories;

namespace ZPProductManagement.Web.Infrastructure
{
    public class InputCategoryAdapter : ICategoryAdapter
    {
        public InputCategoryAdapter(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; }

        public string Name { get; }
    }
}