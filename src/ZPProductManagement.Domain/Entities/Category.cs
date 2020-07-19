using System;
using ZPProductManagement.Domain.ValueObjects;

namespace ZPProductManagement.Domain.Entities
{
    public class Category
    {
        public Category(Identifier id, Name name)
        {
            if (id == null)
            {
                throw new ArgumentException($"{nameof(id)} must not be null", nameof(id));
            }

            if (name == null)
            {
                throw new ArgumentException($"{nameof(name)} must not be null", nameof(name));
            }

            Id = id;
            Name = name;
        }

        public Identifier Id { get; }

        public Name Name { get; }
    }
}
