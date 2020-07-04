using System;
using ZPProductManagement.Application.Files;

namespace ZPProductManagement.Web.Infrastructure
{
    public class InputFileAdapter : IFileAdapter
    {
        public InputFileAdapter(Guid id, string name, string path, string extension)
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