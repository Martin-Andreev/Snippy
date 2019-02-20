namespace Snippy.Web.Models.ViewModels
{
    using System.Collections.Generic;

    public class LabelDetailedViewModel : LabelViewModel
    {
        public IEnumerable<SnippetViewModel> Snippets { get; set; }
    }
}