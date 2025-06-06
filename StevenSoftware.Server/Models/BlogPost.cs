using System.ComponentModel.DataAnnotations;

namespace StevenSoftware.Server.Models
{
    public class BlogPost
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        [Required]
        public string AuthorId { get; set; }
        public ApplicationUser Author { get; set; }
    }
}