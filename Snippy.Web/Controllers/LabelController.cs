using System.Web.Mvc;

namespace Snippy.Web.Controllers
{
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using Models.ViewModels;

    public class LabelController : BaseController
    {
        // GET: Label
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            var label = this.Data
                .Labels
                .All()
                .Where(l => l.Id == id)
                .Project()
                .To<LabelDetailedViewModel>()
                .FirstOrDefault();

            return this.View(label);
        }
    }
}