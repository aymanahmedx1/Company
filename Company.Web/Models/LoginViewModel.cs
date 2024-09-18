using System.ComponentModel.DataAnnotations;

namespace Company.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; }


        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$", ErrorMessage = "Password must Has Uniqe Chars , 2 Digits , UpperCase , LowerCase")]
        public string Password { get; set; }
        public bool Remember { get; set; }
    }
}
