using System;

namespace ZPProductManagement.Application
{
    public class StoredFile
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public string Ext { get; set; }
    }
}
