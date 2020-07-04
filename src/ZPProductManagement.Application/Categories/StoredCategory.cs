using System;

namespace ZPProductManagement.Application.Categories
{
    public class StoredCategory
    {
        public StoredCategory(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; }

        public string Name { get; }
    }
}