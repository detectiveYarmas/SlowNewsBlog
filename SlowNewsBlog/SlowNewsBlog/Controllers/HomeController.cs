using SlowNewsBlog.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SlowNewsBlog.Models;
using SlowNewsBlog.Domain.Managers;
using SlowNewsBlog.Domain.Factories;

namespace SlowNewsBlog.Controllers
{
    public class HomeController : Controller
    {
        BlogPostRepoManager _blogManager = BlogPostRepoManagerFactory.Create();
        CategoryRepoManager _categoryManager = CategoryRepoManagerFactory.Create();
        HashtagRepoManager _hashManager = HashTagManagerFactory.Create();


        [AllowAnonymous]
        public ActionResult Index()
        {
            
            MultipleBlogPostViewModel model = new MultipleBlogPostViewModel();
            var blogResponse = _blogManager.GetNewestBlogs();
            var categoryResponse = _categoryManager.GetAllCategories();            
            if (blogResponse.Success)
            {
                model.BlogPosts = blogResponse.BlogPosts;
                Dictionary<int, List<HashTag>> hashtags = new Dictionary<int, List<HashTag>>();
                foreach( var blah in blogResponse.BlogPosts)
                {
                    var hashResponse = _hashManager.GetHashTagsForBlog(blah.BlogPostId);
                    if (hashResponse.Success)
                    {
                        hashtags.Add(blah.BlogPostId, hashResponse.HashTags);
                    }
                 
                }
                model.HashTagsForBlogPosts = hashtags;
            }
            if (categoryResponse.Success)
            {
                model.Categories = categoryResponse.Catagories;
            }
                return View(model);
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

        [AllowAnonymous]
        public ActionResult Posts()
        {
            
            return View(_blogManager.GetAllBlogs());
        }

        
        public ActionResult Create()
        {
            BlogPost blogPost = new BlogPost();
            return View(blogPost);
        }

        [HttpPost]
        public ActionResult Create(BlogPost post)
        {
            //_blogManager.AddNewBlogPost(post);

            return RedirectToAction("Post");
        }
    }
}