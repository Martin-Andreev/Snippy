namespace Snippy.Web.Models.ViewModels
{
    using Infrastructure;
    using Snippy.Models;

    public class SnippetMinifiedViewModel : IMapFrom<Snippet>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}