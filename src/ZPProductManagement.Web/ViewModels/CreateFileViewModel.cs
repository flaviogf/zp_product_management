using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace ZPProductManagement.Web.ViewModels
{
    public class CreateFileViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public IFormFile File { get; set; }
    }
}
