using System;
using System.IO;

namespace ZPProductManagement.Application
{
    public class CreatedFile
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public string Ext { get; set; }

        public Stream Content { get; set; }
    }
}
