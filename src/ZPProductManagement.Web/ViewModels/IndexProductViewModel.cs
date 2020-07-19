using System;

namespace ZPProductManagement.Web.ViewModels
{
    public class IndexProductViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string CategoryName { get; set; }

        public string Status { get; set; }
    }
}
