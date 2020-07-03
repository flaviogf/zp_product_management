using System;

namespace ZPProductManagement.Domain
{
    public class File
    {
        public File(Guid id, string name, string path, string ext)
        {
            Id = id;
            Name = name;
            Path = path;
            Ext = ext;
        }

        public Guid Id { get; }

        public string Name { get; }

        public string Path { get; }

        public string Ext { get; }
    }
}
