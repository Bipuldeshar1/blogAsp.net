using System.ComponentModel.DataAnnotations;

namespace blogg.Models
{
    public class CommentModel
    {
        [Key]
        public int Id { get; set; }
        public string comment { get; set; }
        public string BlogId { get; set; }
        public BlogModel blogModel { get; set; }
        public string userId { get; set; }
        public AppUSer appUser { get; set; }
    }
}
