using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ZPProductManagement.Common;
using ZPProductManagement.Domain.ValueObjects;

namespace ZPProductManagement.Domain.Entities
{
    public class Product
    {
        private readonly List<File> _files = new List<File>();

        public Product
        (
            Identifier id,
            Category category,
            Name name,
            Description description,
            Price price,
            Quantity quantity,
            IEnumerable<File> files
        )
        {
            if (id == null)
            {
                throw new ArgumentException($"{nameof(id)} must not be null", nameof(id));
            }

            if (category == null)
            {
                throw new ArgumentException($"{nameof(category)} must not be null", nameof(category));
            }

            if (name == null)
            {
                throw new ArgumentException($"{nameof(name)} must not be null", nameof(name));
            }

            if (description == null)
            {
                throw new ArgumentException($"{nameof(description)} must not be null", nameof(description));
            }

            if (price == null)
            {
                throw new ArgumentException($"{nameof(price)} must not be null", nameof(price));
            }

            if (quantity == null)
            {
                throw new ArgumentException($"{nameof(quantity)} must not be null", nameof(quantity));
            }

            if (files == null)
            {
                throw new ArgumentException($"{nameof(files)} must not be null", nameof(files));
            }

            if (!files.Any())
            {
                throw new ArgumentException($"{nameof(files)} must have at least a file", nameof(files));
            }

            Id = id;
            Category = category;
            Name = name;
            Description = description;
            Price = price;
            Quantity = quantity;

            AddRange(files);

            Activated = new Activated(this);
            Archived = new Archived(this);
            Deleted = new Deleted(this);

            Status = Activated;
        }

        public Identifier Id { get; }

        public Category Category { get; }

        public Name Name { get; }

        public Description Description { get; }

        public Price Price { get; }

        public Quantity Quantity { get; }

        public IReadOnlyList<File> Files => new ReadOnlyCollection<File>(_files);

        public Status Status { get; internal protected set; }

        public Status Activated { get; }

        public Status Archived { get; }

        public Status Deleted { get; }

        public Result AddRange(IEnumerable<File> files)
        {
            _files.AddRange(files);

            return Result.Ok();
        }

        public Result Active()
        {
            return Status.Active();
        }

        public Result Archive()
        {
            return Status.Archive();
        }

        public Result Delete()
        {
            return Status.Delete();
        }
    }
}