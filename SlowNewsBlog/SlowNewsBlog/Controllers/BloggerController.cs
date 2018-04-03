using Microsoft.AspNet.Identity;
using SlowNewsBlog.Domain.Factories;
using SlowNewsBlog.Domain.Managers;
using SlowNewsBlog.Models;
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
        private CategoryRepoManager catRepo = CategoryRepoManagerFactory.Create();
        private HashtagRepoManager hashRepo = HashTagManagerFactory.Create();

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

        [ValidateInput(false)]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(new AddBlogViewModel()
            {
                BlogPost = repo.GetBlogById(id).BlogPost,
                Catagories = catRepo.GetAllCategories().Catagories.Select(m => new SelectListItem
                {
                    Text = m.CatagoryName,
                    Value = m.CatagoryId.ToString()
                }),
                HashTags = hashRepo.GetAllHashTags().HashTags.Select(m => new SelectListItem
                {
                    Text = m.HashTagName,
                    Value = m.HashTagId.ToString()
                }),
            }
            );
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(AddBlogViewModel model)
        {
            BlogPost blog = new BlogPost();
            blog = model.BlogPost;
            repo.UpdateBlogPost(blog);
            return Redirect("EditPost");
        }

    }
}