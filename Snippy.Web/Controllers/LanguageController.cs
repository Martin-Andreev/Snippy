using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Snippy.Web.Controllers
{
    using AutoMapper.QueryableExtensions;
    using Models.ViewModels;

    public class LanguageController : BaseController
    {
        // GET: Language
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AllSnippets(int id)
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            var snippets = this.Data
                .Snippets
                .All()
                .Where(s => s.LanguageId == id)
                .Project()
                .To<SnippetDetailedViewModel>()
                .FirstOrDefault();

            return this.View(snippets);
        }
    }
}