using System.ComponentModel.DataAnnotations;

namespace Company.Web.Models
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; }

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$", ErrorMessage = "Password must Has Uniqe Chars , 2 Digits , UpperCase , LowerCase")]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Confirm Password Must Equal To password")]
        public string ConfirmPassword { get; set; }
        public string Token { get; set; }
    }
}
