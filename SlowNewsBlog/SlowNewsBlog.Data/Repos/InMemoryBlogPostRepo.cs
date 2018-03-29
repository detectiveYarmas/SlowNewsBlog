using SlowNewsBlog.Data.Interfaces;
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

        public void AddBloggerToBlogPost(int blogger, int blogPost)
        {
            throw new NotImplementedException();
        }

        public void AddNewBlogPost(BlogPost blogPost)
        {
            blogPost.BlogPostId = blogPosts.Max(id => id.BlogPostId) + 1;
            blogPosts.Add(blogPost);
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

        public List<BlogPost> GetBlogsByBlogger(int id)
        {
            throw new NotImplementedException();
        }

        public List<BlogPost> GetNewestBlogs()
        public List<BlogPost> GetBlogsByCatagory(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveBloggerFromBlogPost(int bloggerId, int blogPostId)
        {
            throw new NotImplementedException();
        }

        public void UpdateBlogPost(BlogPost blogPost)
        {
            blogPosts.Remove(blogPosts.Where(blog => blog.BlogPostId == blogPost.BlogPostId).FirstOrDefault());
            blogPosts.Add(blogPost);
        }
    }
}
