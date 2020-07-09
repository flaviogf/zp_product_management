using System.ComponentModel.DataAnnotations;

namespace ZPProductManagement.Web.ViewModels
{
    public class CreateSignInViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}