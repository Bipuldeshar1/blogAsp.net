using System.ComponentModel.DataAnnotations;

namespace blogg.Models.viewModel
{
    public class ChangePasswordModel
    {
        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }

        [Compare("NewPassword")]
        public string ConfirmPassword { get; set;}
    }
}
