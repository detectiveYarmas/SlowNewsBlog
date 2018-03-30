﻿using SlowNewsBlog.Data.Interfaces;
using SlowNewsBlog.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlowNewsBlog.Data.Repos
{
    public class InMemoryBlogPostRepo:IBlogPostRepo
    {
        public static List<BlogPost> blogPosts = new List<BlogPost>()
        {
            new BlogPost (){BlogPostId = 1,Blog = "metapost",Title="Test1",Approved=true},
            new BlogPost (){BlogPostId= 2,Blog= "horseandbuggy",Title="Test2",Approved = true},
            new BlogPost (){BlogPostId=3, Blog="moon",Title="Test3", Approved = false},
            new BlogPost (){BlogPostId= 4, Blog= "y2k",Title="Test4",Approved = false}
        };
        public static List<BlogPostsBloggers> blogPostsBloggers = new List<BlogPostsBloggers>()
        {
            new BlogPostsBloggers(){UserName="1",BlogPostId=1},
            new BlogPostsBloggers(){BlogPostId=2,UserName="2"},
            new BlogPostsBloggers(){UserName= "1", BlogPostId=2},
            new BlogPostsBloggers(){BlogPostId =3,UserName="3"},
            new BlogPostsBloggers(){UserName = "4", BlogPostId=4}
        };
        

        public void AddBloggerToBlogPost(string blogger, int blogPost)
        {
            blogPostsBloggers.Add(new BlogPostsBloggers() { BlogPostId = blogPost, UserName = blogger });
        }

        public BlogPost AddNewBlogPost(BlogPost blogPost)
        {
            blogPost.BlogPostId = blogPosts.Max(id => id.BlogPostId) + 1;
            blogPosts.Add(blogPost);
            return blogPost;
        }

        public void ApproveBlog(int id)
        {
            BlogPost blogPost = blogPosts.Where(blog => blog.BlogPostId == id).FirstOrDefault();
            blogPost.Approved = true;
            UpdateBlogPost(blogPost);
        }

        public void DisapproveBlog(int id)
        {
            BlogPost blogPost = blogPosts.Where(blog => blog.BlogPostId == id).FirstOrDefault();
            blogPost.Approved = false;
            UpdateBlogPost(blogPost);
        }

        public List<BlogPost> GetAllApprovedBlogPosts()
        {
            return blogPosts.Where(blog => blog.Approved).ToList();
        }

        public List<BlogPost> GetAllBlogs()
        {
            return blogPosts;
        }

        public List<BlogPost> GetAllDisapprovedBlogPosts()
        {
            return blogPosts.Where(blog => blog.Approved==false).ToList();
        }

        public BlogPost GetBlog(int id)
        {
            return blogPosts.Where(blog => blog.BlogPostId == id).FirstOrDefault();
        }

        public List<BlogPost> GetBlogsByBlogger(string id)
        {
            List<BlogPost> toReturn = new List<BlogPost>();
            foreach (var group in blogPostsBloggers.Where(blogger => blogger.UserName == id).GroupBy(i => i.BlogPostId))
            {
                toReturn.Add(GetBlog(group.Key));
            }
            return toReturn;

        }

        public List<BlogPost> GetNewestBlogs()
        {
            return blogPosts.OrderByDescending(p => p.PublishedDate).Take(10).ToList();
        }
        public List<BlogPost> GetBlogsByCatagory(int id)
        {
            return blogPosts.Where(blog => blog.CatagoryId == id).ToList();
        }

        public void RemoveBloggerFromBlogPost(string bloggerId, int blogPostId)
        {
            blogPostsBloggers.Remove(blogPostsBloggers.Where(b => b.UserName == bloggerId && b.BlogPostId == blogPostId).FirstOrDefault());
        }

        public void UpdateBlogPost(BlogPost blogPost)
        {
            blogPosts.Remove(blogPosts.Where(blog => blog.BlogPostId == blogPost.BlogPostId).FirstOrDefault());
            blogPosts.Add(blogPost);
        }
    }
}
