using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace blogg.Models
{
    public class AppUSer:IdentityUser
    {
        [Key]
       public int Id {  get; set; }
        public string NickName { get; set; }

        public ICollection<BlogModel>BlogModels { get; set; }

        public ICollection<CommentModel> commentModels { get; set; }
    }
    
}
