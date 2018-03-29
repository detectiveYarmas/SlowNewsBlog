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
        public static List<BlogPost> catagories = new List<BlogPost>()
        {
            new BlogPost (){BlogPostId = 1,Blog = "metapost"},
            new BlogPost (){BlogPostId= 2,Blog= "horseandbuggy"},
            new BlogPost (){BlogPostId=3, Blog="moon"},
            new BlogPost (){BlogPostId= 4, Blog= "y2k" }
        };

        public void AddBloggerToBlogPost(int blogger, int blogPost)
        {
            throw new NotImplementedException();
        }

        public void AddNewBlogPost(BlogPost blogPost)
        {
            throw new NotImplementedException();
        }

        public void ApproveBlog(int id)
        {
            throw new NotImplementedException();
        }

        public void DisapproveBlog(int id)
        {
            throw new NotImplementedException();
        }

        public List<BlogPost> GetAllApprovedBlogPosts()
        {
            throw new NotImplementedException();
        }

        public List<BlogPost> GetAllBlogs()
        {
            throw new NotImplementedException();
        }

        public List<BlogPost> GetAllDisapprovedBlogPosts()
        {
            throw new NotImplementedException();
        }

        public BlogPost GetBlog(int id)
        {
            throw new NotImplementedException();
        }

        public List<BlogPost> GetBlogsByBlogger(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveBloggerFromBlogPost(int bloggerId, int blogPostId)
        {
            throw new NotImplementedException();
        }

        public void UpdateBlogPost(BlogPost blogPost)
        {
            throw new NotImplementedException();
        }
    }
}
