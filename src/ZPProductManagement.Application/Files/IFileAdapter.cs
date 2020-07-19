using System;

namespace ZPProductManagement.Application.Files
{
    public interface IFileAdapter
    {
        Guid Id { get; }

        string Name { get; }

        string Path { get; }

        string Extension { get; }
    }
}
