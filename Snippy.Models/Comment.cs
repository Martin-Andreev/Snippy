namespace Snippy.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public string Content { get; set; }

        public int SnippetId { get; set; }

        public virtual Snippet Snippet { get; set; }
    }
}
