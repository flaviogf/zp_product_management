using System;
using ZPProductManagement.Domain;

namespace ZPProductManagement.Application
{
    public class CreatedFile
    {
        private readonly File _file;

        internal CreatedFile(File file)
        {
            _file = file;
        }

        public Guid Id => _file.Id;

        public string Name => _file.Name;

        public string Path => _file.Path;

        public string Extension => _file.Extension;
    }
}
