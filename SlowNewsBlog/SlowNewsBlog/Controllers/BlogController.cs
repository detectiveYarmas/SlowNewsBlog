using Microsoft.AspNet.Identity;
using SlowNewsBlog.Data.Interfaces;
using SlowNewsBlog.Data.Repos;
using SlowNewsBlog.Domain.Factories;
using SlowNewsBlog.Domain.Managers;
using SlowNewsBlog.Models;
using SlowNewsBlog.Models.Tables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SlowNewsBlog.Controllers
{
    public class BlogController : Controller
    {
        BlogPostRepoManager _blogManager = BlogPostRepoManagerFactory.Create();
        CategoryRepoManager _categoryManager = CategoryRepoManagerFactory.Create();
        HashtagRepoManager _hashManager = HashTagManagerFactory.Create();


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

        [HttpGet]
        public ActionResult BlogPost(int blogId)
        {
            var blogMgr = BlogPostRepoManagerFactory.Create();
            var blogResponse = blogMgr.GetBlogById(blogId);
            var model = new SingleBlogPostViewModel();
            var hashMgr = HashTagManagerFactory.Create();
            var cateMgr = CategoryRepoManagerFactory.Create();
            

            if (blogResponse.Success)
            {
                model.BlogPost = blogResponse.BlogPost;
                var hashTagResponse = hashMgr.GetHashTagsForBlog(blogResponse.BlogPost.BlogPostId);
                if (hashTagResponse.Success)
                {
                    model.HashTags = hashTagResponse.HashTags;
                }
            }
            var cates = cateMgr.GetAllCategories();
            if (cates.Success)
            {
                model.Catagories = cates.Catagories;
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult BlogsByHashTag(int hashTagId)
        {
            MultipleBlogPostViewModel model = new MultipleBlogPostViewModel();
            var blogResponse = _blogManager.GetBlogsByHashTag(hashTagId);
            var categoryResponse = _categoryManager.GetAllCategories();
            if (blogResponse.Success)
            {
                model.BlogPosts = blogResponse.BlogPosts;
                Dictionary<int, List<HashTag>> hashtags = new Dictionary<int, List<HashTag>>();
                foreach (var blah in blogResponse.BlogPosts)
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
            //else
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            return View(model);
        }
    }
}


    
