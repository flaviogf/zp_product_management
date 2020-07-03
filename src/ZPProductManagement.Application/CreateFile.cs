using System;

namespace ZPProductManagement.Application
{
    public class CreateFile
    {
        public CreateFile(Guid id, string name, string path, string extension)
        {
            Id = id;
            Name = name;
            Path = path;
            Extension = extension;
        }

        public Guid Id { get; }

        public string Name { get; }

        public string Path { get; }

        public string Extension { get; }
    }
}
