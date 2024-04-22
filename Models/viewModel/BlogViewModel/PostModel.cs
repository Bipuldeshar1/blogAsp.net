using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace blogg.Models.viewModel.BlogViewModel
{
    public class PostModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public IFormFile ImageFile { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
