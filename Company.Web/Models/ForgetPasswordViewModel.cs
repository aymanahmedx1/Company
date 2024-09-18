using System.ComponentModel.DataAnnotations;

namespace Company.Web.Models
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "Email is Required") , EmailAddress]
        public string Email { get; set; }
    }
}
