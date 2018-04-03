using SlowNewsBlog.Domain.Factories;
using SlowNewsBlog.Domain.Managers;
using SlowNewsBlog.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SlowNewsBlog.Controllers
{
    public class BloggerController : Controller
    {
        
        private BlogPostRepoManager repo = BlogPostRepoManagerFactory.Create();
        // GET: Blogger
        public ActionResult EditPost()
        {
            GetBlogByBlogerResponse byBlogerResponse = repo.GetBlogByBloger(User.Identity.Name);
            if (!byBlogerResponse.Success)
            {
                return RedirectToAction("index", "Home");
            }

            return View();
        }
    }
}