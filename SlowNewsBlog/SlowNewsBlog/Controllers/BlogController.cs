
using SlowNewsBlog.Domain.Factories;
using SlowNewsBlog.Models;
using SlowNewsBlog.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SlowNewsBlog.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        public ActionResult AddBlog()
        {
            var model = new AddBlogViewModel();
            var hashMgr = HashTagManagerFactory.Create();
            var hashTags = hashMgr.GetApprovedHashtags();
            var cataMgr = CategoryRepoManagerFactory.Create();
            var categories = cataMgr.GetAllCategories();


            model.HashTags = hashTags.HashTags.Select(m => new SelectListItem
            {
                Text = m.HashTagName,
                Value = m.HashTagId.ToString()
            });

            model.Catagories = categories.Catagories.Select(m => new SelectListItem
            {
                Text = m.CatagoryName,
                Value = m.CatagoryId.ToString()
            });

            return View(model);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult AddBlog(AddBlogViewModel model)
        {
            var hashMgr = HashTagManagerFactory.Create();
            var cateMgr = CategoryRepoManagerFactory.Create();

            if (ModelState.IsValid)
            {
                model.BlogPost.BlogPostHashTags = new List<HashTag>();

                foreach (var id in model.SelectedHashtagIds)
                {
                    var response = hashMgr.GetHashTag(id);

                    model.BlogPost.BlogPostHashTags.Add(response.HashTag);
                }

                var cResponse = cateMgr.GetCatagory(model.Catagory.CatagoryId);
                model.Catagory = cResponse.CatagoryGot;


                var blogMgr = BlogPostRepoManagerFactory.Create();
                blogMgr.AddBlog(model.BlogPost);

                return RedirectToAction("Blogs", "Admin", new { id = model.BlogPost.BlogPostId });
            }

            return View(model);

        [HttpGet]
        public ActionResult BlogPost(int blogId)
        {
            var blogResponse = _blogManager.GetBlogById(blogId);
            var model = new SingleBlogPostViewModel();
            
            if (blogResponse.Success)
            {
                model.BlogPost = blogResponse.BlogPost;
                var hashTagResponse = _hashManager.GetHashTagsForBlog(blogResponse.BlogPost.BlogPostId);
                if (hashTagResponse.Success)
                {
                    model.HashTags = hashTagResponse.HashTags;
                }
            }
            var cates = _categoryManager.GetAllCategories();
            if (cates.Success)
            {
                model.Catagories = cates.Catagories;
            }

            return View(model);
        }

        [ValidateInput(false)]
        [HttpGet]
        public ActionResult BlogsByCatagory(int id)
        {
            BlogPostRepoManager manager = BlogPostRepoManagerFactory.Create();
            if (!manager.GetBlogsByCatagory(id).Success)
            {
                return RedirectToAction("Index", "Home");                
            }
            IEnumerable<BlogPost> model = manager.GetBlogsByCatagory(id).BlogsInCatagory;
            return View(model);
        }

    }
}
