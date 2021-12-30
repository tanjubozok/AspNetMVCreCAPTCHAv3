using System.ComponentModel.DataAnnotations;

namespace AspNetMVCreCAPTCHAv3.Models
{
    public class RegisterModel
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string GoogleCaptchaToken { get; set; }
    }
}