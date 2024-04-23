using System.ComponentModel.DataAnnotations;

namespace blogg.Models.viewModel.RoleViewModel
{
    public class RoleViewModel
    {
     
        [Required(ErrorMessage ="role name required")]
        public string RoleName { get; set; }

   //     public List<string> Users {  get; set; }

    }

}
