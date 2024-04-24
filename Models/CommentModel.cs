using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace blogg.Models
{
    public class CommentModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Comment { get; set; }

       
        [Required]
        public int BlogId { get; set; }

        public BlogModel Blog { get; set; }

        [ForeignKey("AppUser")]
        public string UserId { get; set; }

        public AppUSer AppUser { get; set; }

    }
}
