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

        // GET: Blog
        public ActionResult AddPost()
        {
            var model = new AddBlogViewModel();
            var hashMgr = HashTagManagerFactory.Create();
            var hashTags = hashMgr.GetAllHashTags();
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
        public ActionResult AddPost(AddBlogViewModel model)
        {
            var hashMgr = HashTagManagerFactory.Create();
            var cateMgr = CategoryRepoManagerFactory.Create();
            var cResponse = cateMgr.GetCatagory(model.Catagory.CatagoryId);
            model.Catagory = cResponse.CatagoryGot;
            model.BlogPost.CatagoryId = model.Catagory.CatagoryId;
            model.BlogPost.BlogPostHashTags = new List<HashTag>();
            model.BlogPost.Approved = false;
            model.BlogPost.Id = User.Identity.GetUserId();
            model.BlogPost.UserName = User.Identity.Name;
            foreach (var id in model.SelectedHashtagIds)
            {
                var response = hashMgr.GetHashTag(id);

                model.BlogPost.BlogPostHashTags.Add(response.HashTag);
            }

            if (model.ImageUpload != null && model.ImageUpload.ContentLength > 0)
            {
                var savepath = Server.MapPath("~/Images");

                string fileName = Path.GetFileNameWithoutExtension(model.ImageUpload.FileName);
                string extension = Path.GetExtension(model.ImageUpload.FileName);

                var filePath = Path.Combine(savepath, fileName + extension);

                int counter = 1;
                while (System.IO.File.Exists(filePath))
                {
                    filePath = Path.Combine(savepath, fileName + counter.ToString() + extension);
                    counter++;
                }

                model.ImageUpload.SaveAs(filePath);
                model.BlogPost.HeaderImage = Path.GetFileName(filePath);
            }

            if (ModelState.IsValid)
            {
                var blogMgr = BlogPostRepoManagerFactory.Create();
                blogMgr.AddBlog(model.BlogPost);

                return RedirectToAction("BlogPost", "Blog");
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


    
