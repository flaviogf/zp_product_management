using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ZPProductManagement.Web.ViewModels
{
    public class CreateFileViewModel
    {
        [Required]
        public IFormFileCollection Files { get; set; }
    }
}
