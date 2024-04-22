using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace blogg.Models
{
    public class AppUSer:IdentityUser
    {
       
        public string NickName { get; set; }

        public ICollection<BlogModel>BlogModels { get; set; }
    }
    
}
