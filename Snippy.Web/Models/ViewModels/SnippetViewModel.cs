namespace Snippy.Web.Models.ViewModels
{
    using System.Collections.Generic;

    public class SnippetViewModel : SnippetMinifiedViewModel
    {
        public IEnumerable<LabelViewModel> Labels { get; set; }
    }
}