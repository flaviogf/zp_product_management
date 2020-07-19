using System;
using ZPProductManagement.Domain.ValueObjects;

namespace ZPProductManagement.Domain.Entities
{
    public class File
    {
        public File(Identifier id, Name name, Path path, Extension extension)
        {
            if (id == null)
            {
                throw new ArgumentException($"{nameof(id)} must not be null", nameof(id));
            }

            if (name == null)
            {
                throw new ArgumentException($"{nameof(name)} must not be null", nameof(name));
            }

            if (path == null)
            {
                throw new ArgumentException($"{nameof(path)} must not be null", nameof(path));
            }

            if (extension == null)
            {
                throw new ArgumentException($"{nameof(extension)} must not be null", nameof(extension));
            }

            Id = id;
            Name = name;
            Path = path;
            Extension = extension;
        }

        public Identifier Id { get; }

        public Name Name { get; }

        public Path Path { get; }

        public Extension Extension { get; }
    }
}
