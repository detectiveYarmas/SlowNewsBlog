﻿using SlowNewsBlog.Domain.Factories;
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
        public ActionResult AddPost()
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
        public ActionResult AddPost(AddBlogViewModel model)
        {
            var hashMgr = HashTagManagerFactory.Create();
            var cateMgr = CategoryRepoManagerFactory.Create();

            if (ModelState.IsValid)
            {
                model.BlogPost.BlogPostHashTags = new List<HashTag>();

                foreach(var id in model.SelectedHashtagIds)
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
        }

        [HttpGet]
        public ActionResult BlogsByHashTag(int hashId)
        {
            var blogMngr = BlogPostRepoManagerFactory.Create();
            var blogs = blogMngr.GetBlogsByHashTag(hashId);
            var cateMngr = CategoryRepoManagerFactory.Create();
            var cates = cateMngr.GetAllCategories();
            var hashMngr = HashTagManagerFactory.Create();
            var model = new MultipleBlogPostViewModel();
            var hashtagsForBlogPosts = new Dictionary<int, List<HashTag>>();

            if(!blogs.Success)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var blog in blogs.BlogPosts)
                {
                    var hashtags = hashMngr.GetHashTagsForBlog(blog.BlogPostId);
                    if (hashtags.Success)
                    {
                        hashtagsForBlogPosts.Add(blog.BlogPostId, hashtags.HashTags);
                    }
                }
                model.HashTagsForBlogPosts = hashtagsForBlogPosts;
            }
            if (cates.Success)
            {
                model.Categories = cates.Catagories;
            }
            if (blogs.Success)
            {
                model.BlogPosts = blogs.BlogPosts;
            }

            return View(model);
        }
    }
}