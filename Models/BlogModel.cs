using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace blogg.Models
{
    public class BlogModel
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [ForeignKey("appUSer")]
        public string AuthorId { get; set; }

        public AppUSer appUSer { get; set; }

    }
}
