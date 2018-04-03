using Microsoft.AspNet.Identity;
using SlowNewsBlog.Domain.Factories;
using SlowNewsBlog.Domain.Managers;
using SlowNewsBlog.Models.Responses;
using SlowNewsBlog.Models.Tables;
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
            return View(byBlogerResponse.BlogsByBlogger);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(repo.GetBlogById(id).BlogPost);
        }
        [HttpPost]
        public ActionResult Edit(BlogPost model)
        {
            repo.UpdateBlogPost(model);
            return Redirect("EditPost");
        }

    }
}