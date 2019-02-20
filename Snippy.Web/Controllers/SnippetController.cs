using System.Web.Mvc;

namespace Snippy.Web.Controllers
{
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using Models.ViewModels;

    public class SnippetController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            var snippet = this.Data
                .Snippets
                .All()
                .Where(s => s.Id == id)
                .Project()
                .To<SnippetDetailedViewModel>()
                .FirstOrDefault();

            return View(snippet);
        }

        public ActionResult All()
        {
            var labels = this.Data
                .Snippets
                .All()
                .OrderByDescending(s => s.CreatedOn)
                .Project()
                .To<SnippetViewModel>();

            return View(labels);
        }
    }
}