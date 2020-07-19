using System;
using System.Collections.Generic;

namespace ZPProductManagement.Web.ViewModels
{
    public class ShowProductViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public Guid CategoryId { get; set; }

        public string CategoryName { get; set; }

        public IEnumerable<ShowFileViewModel> Files { get; set; }

        public string Status { get; set; }
    }
}
