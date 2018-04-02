using SlowNewsBlog.Data.Interfaces;
using SlowNewsBlog.Models.Responses;
using SlowNewsBlog.Models.Tables;
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

        

        public GetBlogByBlogerResponse GetBlogByBloger(string blogger)
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

        public AddBlogResponse AddBlog(BlogPost blog)
        {
            var response = new AddBlogResponse();
            var blogs = blogRepo.GetAllBlogs();

            if(blogs == null)
            {
                response.Success = false;
                response.Message = "There are no blogs.";
            }
            else if(blogs.Any(b => b.BlogPostId == blog.BlogPostId))
            {
                response.Success = false;
                response.Message = $"{blog.BlogPostId} already exists.";
            }
            else if (blogs.Any(b => b.Title == blog.Title))
            {
                response.Success = false;
                response.Message = $"{blog.Title} already exists.";
            }
            else
            {
                response.Blog = blogRepo.AddNewBlogPost(blog);
                
                if(response.Blog == null)
                {
                    response.Success = false;
                    response.Message = $"{response.Blog} is not valid.";
                }
                else
                {
                    response.Success = true;
                }
            }

            return response;
        }
    }
}
