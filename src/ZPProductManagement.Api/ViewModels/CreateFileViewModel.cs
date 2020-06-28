using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace ZPProductManagement.Api.ViewModels
{
    public class CreateFileViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public IFormFile Content { get; set; }
    }
}
