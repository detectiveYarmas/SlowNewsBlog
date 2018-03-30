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
        private IBlogPostRepo blogRepo { get; set; }
        private ICategory categoryRepo { get; set; }

        public BlogPostRepoManager(IBlogPostRepo blogPostRepo, ICategory category)
        {
            blogRepo = blogPostRepo;
            categoryRepo = category;
        }

        public Response AddBloggerToBlogPost(int blogger, int blog)
        {
            Response response = new Response();
            if (blogRepo.GetAllBlogs().All(b => b.BlogPostId != blog))
            {
                response.Success = false;
                response.Message = "ERROR: No Blogs with ID=" + blog;
                return response;
            }
            blogRepo.AddBloggerToBlogPost(blogger, blog);
            response.Message = "BloggerAddedToPost";
            response.Success = true;
            return response;
        }

        public GetBlogByBlogerResponse GetBlogByBloger(int blogger)
        {
            GetBlogByBlogerResponse getBlogByBlogerResponse = new GetBlogByBlogerResponse();
           

            getBlogByBlogerResponse.BlogsByBlogger = blogRepo.GetBlogsByBlogger(blogger);
            getBlogByBlogerResponse.Message = "Success";
            getBlogByBlogerResponse.Success = true;
            return getBlogByBlogerResponse;
        }

        public GetBlogResponse GetBlogById(int id)
        {
            var response = new GetBlogResponse();

            response.BlogPost = blogRepo.GetBlog(1);

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

        public Response RemoveBloggerFromBlogPost(int blogger,int post)
        {
            Response response = new Response();
            if (blogRepo.GetAllBlogs().All(b => b.BlogPostId != post))
            {
                response.Message = "ERROR: Blog with ID " + post + " DNE";
                response.Success = false;
                return response;
            }
            blogRepo.RemoveBloggerFromBlogPost(blogger, post);
            response.Success = true;
            response.Message = "Success";
            return response;
        }

        public GetAllBlogsResponse GetAllBlogs()
        {
            var response = new GetAllBlogsResponse();

            response.BlogPosts = blogRepo.GetAllBlogs();

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
            response.Blogs = blogRepo.GetAllApprovedBlogPosts();

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
            response.BlogPosts = blogRepo.GetAllDisapprovedBlogPosts();

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
            response.BlogPosts = blogRepo.GetNewestBlogs();
            
            if (response.BlogPosts == null)
            {
                response.Success = false;
                response.Message = "ERROR: No blogs returned";
            }
            else
            {
                response.BlogPosts = blogRepo.GetNewestBlogs();
                response.Success = true;
                response.Message = "Got newest posts";
            }
            return response;
        }
    }
}
