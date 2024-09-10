using System.ComponentModel.DataAnnotations;

namespace Company.Web.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="First Name is Required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is Required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; }


        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).*(?:([^\\W\\d_])\\1?(?!.*\\1)){2,}.*$",ErrorMessage ="Password must Has Uniqe Chars , 2 Digits , UpperCase , LowerCase")]
        public string Password { get; set; }

        [Compare(nameof(Password),ErrorMessage ="Confirm Password Musr Equal To password")]
        public string ConfirmPassword { get; set; }
        public bool IsAgree { get; set; }
    }
}
