using System;
using ZPProductManagement.Domain.Entities;

namespace ZPProductManagement.Application.Files
{
    internal class OutputFileAdapter : IFileAdapter
    {
        private readonly File _file;

        internal OutputFileAdapter(File file)
        {
            _file = file;
        }

        public Guid Id => _file.Id;

        public string Name => _file.Name;

        public string Path => _file.Path;

        public string Extension => _file.Extension;
    }
}
