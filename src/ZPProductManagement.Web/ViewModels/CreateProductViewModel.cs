using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ZPProductManagement.Web.ViewModels
{
    public class CreateProductViewModel
    {
        [Required]
        public IFormFile File { get; set; }
    }
}