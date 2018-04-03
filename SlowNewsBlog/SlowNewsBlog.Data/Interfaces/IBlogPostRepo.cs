using SlowNewsBlog.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlowNewsBlog.Data.Interfaces
{
    public interface IBlogPostRepo
    {
        List<BlogPost> GetBlogsByBlogger(string id);
        BlogPost AddNewBlogPost(BlogPost blogPost);
        void DisapproveBlog(int id);
        void ApproveBlog(int id);
        BlogPost GetBlog(int id);
        List<BlogPost> GetAllBlogs();
        void UpdateBlogPost(BlogPost blogPost);
        List<BlogPost> GetAllApprovedBlogPosts();
        List<BlogPost> GetAllDisapprovedBlogPosts();
        List<BlogPost> GetBlogsByCatagory(int id);//
        List<BlogPost> GetNewestBlogs();
        List<BlogPost> GetBlogsByHashTag(int hashId);
        void RemoveBlog(int blogPostId);
    }
}
