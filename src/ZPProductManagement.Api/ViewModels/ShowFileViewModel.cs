using System;

namespace ZPProductManagement.Api.ViewModels
{
    public class ShowFileViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public string Ext { get; set; }
    }
}
