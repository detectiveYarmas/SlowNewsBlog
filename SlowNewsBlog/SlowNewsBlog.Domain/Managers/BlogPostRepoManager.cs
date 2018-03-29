using SlowNewsBlog.Data.Interfaces;
using SlowNewsBlog.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlowNewsBlog.Domain.Managers
{
    public class BlogPostRepoManager
    {
        private IBlogPostRepo repo { get; set; }

        public BlogPostRepoManager(IBlogPostRepo blogPostRepo)
        {
            repo = blogPostRepo;
        }

        public GetBlogResponse GetBlogById(int id)
        {
            var response = new GetBlogResponse();

            response.BlogPost = repo.GetBlog(1);

            if (response.BlogPost==null)
            {
                response.Success = false;
                response.Message = "There are no blogs with id = "+id;
                return response;
            }

            response.Message = "Returned post with id = " + id;
            response.Success = true;
            return response;
        }

        public GetAllBlogsResponse GetAllBlogs()
        {
            var response = new GetAllBlogsResponse();

            response.BlogPosts = repo.GetAllBlogs();

            if (response.BlogPosts == null)
            {
                response.Success = false;
                response.Message = "No Blogs Were Found.";
                return response;
            }

            response.Message = "Blogs Found";
            response.Success = true;
            return response;
        }

        public GetAllApprovedBlogPostsResponse GetAllApprovedBlogPosted()
        {
            var response = new GetAllApprovedBlogPostsResponse();
            response.Blogs = repo.GetAllApprovedBlogPosts();

            if (response.Blogs==null)
            {
                response.Success = false;
                response.Message = "There are no approved blogs.";
                return response;
            }
            else if (response.Blogs.Any(h => h.Approved == false))
            {
                response.Success = false;
                response.Message = "ERROR: Some blogs returned by GetAllApprovedBlogPosts were not approved.";
                return response;
            }
            response.Message = "Got Approved Blogs";
            response.Success = true;
            return response;
        }

        public GetAllDisapprovedBlogPostsResponse GetAllDisapprovedBlogs()
        {
            var response = new GetAllDisapprovedBlogPostsResponse();
            response.BlogPosts = repo.GetAllDisapprovedBlogPosts();

            if (response.BlogPosts==null)
            {
                response.Success = false;
                response.Message = "There are no unapproved blogs.";
                return response;
            }
            else if (response.BlogPosts.Any(h => h.Approved == true))
            {
                response.Success = false;
                response.Message = "ERROR: Some selected blogs were approved.";
                return response;
            }

            response.Success = true;
            return response;
        }


        public GetNewestBlogsResponse GetNewestBlogs()
        {
        
            var response = new GetNewestBlogsResponse();
            response.BlogPosts = repo.GetNewestBlogs();
            
            if (response.BlogPosts == null)
            {
                response.Success = false;
                response.Message = "ERROR: No blogs returned";
            }
            else
            {
                response.Success = true;
            }
            return response;
        }
    }
}
