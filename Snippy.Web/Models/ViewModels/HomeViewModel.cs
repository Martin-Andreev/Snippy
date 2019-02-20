namespace Snippy.Web.Models.ViewModels
{
    using System.Collections.Generic;

    public class HomeViewModel
    {
        public IEnumerable<SnippetViewModel> LatestSnippets { get; set; }

        public IEnumerable<CommentViewModel> LatestComments { get; set; }

        public IEnumerable<LabelsMinifiedViewModel> BestLabels { get; set; }
    }
}