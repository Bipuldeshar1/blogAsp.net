using System.ComponentModel.DataAnnotations;

namespace blogg.Models.viewModel
{
    public class RegisterModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password",ErrorMessage ="psw no match")]
        public string ConfirmPassowrd { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public int PhoneNumber { get; set; }
    }
}
