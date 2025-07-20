namespace StevenSoftware.Server.Models.Dto
{
    public class BlogPostGetDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public string CoverImage { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string? AuthorId { get; set; }
        public UserDto Author { get; set; }
    }

    public class BlogPostGetListDto
    {
        public List<BlogPostGetDto>? BlogPosts { get; set; }
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
    }
}
