namespace StevenSoftware.Server.Models.Dto
{
    public class BlogPostUpdateDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public string CoverImage { get; set; }
    }
}
