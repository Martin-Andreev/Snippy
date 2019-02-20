namespace Snippy.Web.Models.ViewModels
{
    public class CommentViewModel : CommentMinifiedViewModel
    {
        public SnippetMinifiedViewModel Snippet { get; set; }
    }
}