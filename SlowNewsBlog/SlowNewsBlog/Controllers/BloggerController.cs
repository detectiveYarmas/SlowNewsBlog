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
                BlogPost blog = new BlogPost();
                blog = model.BlogPost;
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
                blog.CatagoryId = model.Catagory.CatagoryId;
                blog.BlogPostHashTags = model.BlogPost.BlogPostHashTags;
                blog.Approved = false;
                repo.UpdateBlogPost(blog);
                return RedirectToAction("EditPost");
            }
            return RedirectToAction("EditPost");
        }

    }
}