using System.Web.Mvc;

namespace Snippy.Web.Controllers
{
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using Models.ViewModels;

    public class HomeController : BaseController
    {
        private const byte LatestSnipeptsCount = 5;
        private const byte LatestCommentsCount = 5;
        private const byte BestLabelsCount = 5;

        public ActionResult Index()
        {
            var snippets = this.Data
                .Snippets
                .All()
                .OrderByDescending(s => s.CreatedOn)
                .Take(LatestSnipeptsCount)
                .Project()
                .To<SnippetViewModel>();

            var comments = this.Data
                .Comments
                .All()
                .OrderByDescending(c => c.CreatedOn)
                .Take(LatestCommentsCount)
                .Project()
                .To<CommentViewModel>();

            var labels = this.Data
                .Labels
                .All()
                .OrderByDescending(l => l.Snippets.Count)
                .Take(BestLabelsCount)
                .Project()
                .To<LabelsMinifiedViewModel>();

            HomeViewModel viewModel = new HomeViewModel
            {
                LatestSnippets = snippets,
                LatestComments = comments,
                BestLabels = labels
            };

            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}