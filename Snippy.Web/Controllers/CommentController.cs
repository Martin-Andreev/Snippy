using System.Web.Mvc;

namespace Snippy.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNet.Identity;
    using Models.BindingModels;
    using Models.ViewModels;
    using Snippy.Models;

    public class CommentController : BaseController
    {
        // GET: Comment
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(CommentBindingModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var snippet = this.Data.Snippets.Find(model.SnippetId);
                if (snippet == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid comment!");
                }

                string currentUserId = this.User.Identity.GetUserId();
                var comment = new Comment
                {
                    AuthorId = currentUserId,
                    SnippetId = snippet.Id,
                    CreatedOn = DateTime.Now,
                    Content = model.Content
                };

                this.Data.Comments.Add(comment);
                this.Data.SaveChanges();

                var commentViewModel = this.Data.Comments
                    .All()
                    .Where(x => x.Id == comment.Id)
                    .Project()
                    .To<CommentMinifiedViewModel>()
                    .FirstOrDefault();
                return this.PartialView("DisplayTemplates/CommentMinifiedViewModel", commentViewModel);
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid comment!");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var commentToDel = this.Data.Comments.All()
                .Where(c => c.Id == id)
                .Project()
                .To<CommentViewModel>()
                .FirstOrDefault();

            return View(commentToDel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int snippetId)
        {
            var commentToDel = this.Data.Comments.Find(id);
            if (commentToDel.AuthorId == this.HttpContext.User.Identity.GetUserId())
            {
                this.Data.Comments.Delete(id);
                this.Data.SaveChanges();
            }

            return this.RedirectToAction("Index", "Home");
        }
    }
}