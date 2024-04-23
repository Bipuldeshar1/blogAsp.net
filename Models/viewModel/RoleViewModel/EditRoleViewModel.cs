using System.ComponentModel.DataAnnotations;

namespace blogg.Models.viewModel.RoleViewModel
{
    public class EditRoleViewModel
    {
        public EditRoleViewModel()
        {
            Users = new List<string>();
        }
        public string Id { get; set; }
        [Required(ErrorMessage = "role name required")]
        public string RoleName { get; set; }

          public List<string> Users {  get; set; }
    }
}
