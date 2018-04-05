using Microsoft.AspNet.Identity;
using SlowNewsBlog.Domain.Factories;
using SlowNewsBlog.Domain.Managers;
using SlowNewsBlog.Models;
using SlowNewsBlog.Models.Responses;
using SlowNewsBlog.Models.Tables;
using System;
using System.Collections.Generic;
using System.IO;
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
            model.BlogPost.BlogPostHashTags = new List<HashTag>();

            if(model.SelectedHashtagIds != null)
            {
                hashRepo.RemoveHashTagsFromBlog(model.BlogPost.BlogPostId);

                foreach (var item in model.SelectedHashtagIds)
                {
                    if (hashRepo.GetHashTag(item).Success)
                    {
                        var response = hashRepo.GetHashTag(item);
                        model.BlogPost.BlogPostHashTags.Add(response.HashTag);
                        hashRepo.AddHashToPost(item, model.BlogPost.BlogPostId);
                    }
                }
                BlogPost blog = blog = model.BlogPost;
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
                else
                {
                    blog.HeaderImage = repo.GetBlogById(model.BlogPost.BlogPostId).BlogPost.HeaderImage;
                }
                
                blog.CatagoryId = model.Catagory.CatagoryId;
                blog.BlogPostHashTags = model.BlogPost.BlogPostHashTags;
                repo.UpdateBlogPost(blog);
                return RedirectToAction("EditPost");
            }
            return RedirectToAction("EditPost");
        }

        public ActionResult AddPost()
        {
            var model = new AddBlogViewModel();
            var hashMgr = HashTagManagerFactory.Create();
            var hashTags = hashMgr.GetAllHashTags();
            var cataMgr = CategoryRepoManagerFactory.Create();
            var categories = cataMgr.GetAllCategories();


            model.HashTags = new SelectList(hashTags.HashTags, "HashTagId", "HashTagName");

            model.Catagories = new SelectList(categories.Catagories, "CatagoryId", "CatagoryName");


            return View(model);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult AddPost(AddBlogViewModel model)
        {
            var hashMgr = HashTagManagerFactory.Create();
            var cateMgr = CategoryRepoManagerFactory.Create();
            var cResponse = cateMgr.GetCatagory(model.CatagoryId);
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

                return RedirectToAction("EditPost", "Blogger", new { blogId = model.BlogPost.BlogPostId });
            }

            return View(model);
        }
    }
}