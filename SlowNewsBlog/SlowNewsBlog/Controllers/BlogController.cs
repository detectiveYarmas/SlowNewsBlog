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

            if (ModelState.IsValid)
            {
                model.BlogPost.BlogPostHashTags = new List<HashTag>();

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

                var cResponse = cateMgr.GetCatagory(model.Catagory.CatagoryId);
                model.Catagory = cResponse.CatagoryGot;


                var blogMgr = BlogPostRepoManagerFactory.Create();
                blogMgr.AddBlog(model.BlogPost);

                return RedirectToAction("Blogs", "Admin", new { id = model.BlogPost.BlogPostId });
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
    }
}


    
