namespace Snippy.Web.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class CommentBindingModel
    {
        [Required]
        public string Content { get; set; }

        public int SnippetId { get; set; }
    }
}