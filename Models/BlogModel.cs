namespace blogg.Models
{
    public class BlogModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string AuthorId { get; set; }

        public AppUSer appUSer { get; set; }

    }
}
