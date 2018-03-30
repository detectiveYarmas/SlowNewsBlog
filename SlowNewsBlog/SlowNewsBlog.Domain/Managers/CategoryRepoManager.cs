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
    public class CategoryRepoManager
    {
        private ICategory repo { get; set; }
        private IBlogPostRepo repoBlog { get; set; }


        public CategoryRepoManager(ICategory category, IBlogPostRepo blogPostRepo)
        {
            repo = category;
            repoBlog = blogPostRepo;
        }

        public GetAllCategoriesResponse GetAllCategories()
        {
            GetAllCategoriesResponse response = new GetAllCategoriesResponse();
            response.Catagories=repo.GetAllCategories();
            if (response.Catagories == null)
            {
                response.Success = false;
                response.Message = "No Catagories Found";
                return response;
            }
            response.Success = true;
            response.Message = "Success";
            return response;
        }

        public Response AddNewCatagory(Catagory category)
        {
            Response response = new Response();
            if (GetAllCategories().Catagories.Any(cat => cat.CatagoryName == category.CatagoryName))
            {
                response.Success = false;
                response.Message = "ERROR: Catagory Already Exists";
                return response;
            }

            repo.AddNewCatagory(category);
            response.Success = true;
            response.Message = "Catagory Added";
            return response;
        }

        public Response AddCatagoryToBlogPost(int blogPostId, int categoryId)
        {
            Response response = new Response();            

            if (repo.GetAllCategories().All(cat => cat.CatagoryId != categoryId))
            {
                response.Message = "No catagories have id=" + categoryId;
                response.Success = false;
                return response;
            }
            else if (repoBlog.GetAllBlogs().All(blog => blog.BlogPostId != blogPostId))
            {
                response.Message = "No blogs have id=" + blogPostId;
                response.Success = false;
                return response;
            }
            repo.AddCatagoryToBlogPost(blogPostId, categoryId);
            response.Message = "Catagory added to Post";
            response.Success = true;
            return response;            
        }

        public GetCatagoryResponse GetCatagory(int id)
        {
            GetCatagoryResponse getCatagoryResponse = new GetCatagoryResponse();
            if (repo.GetAllCategories().All(cat => cat.CatagoryId != id))
            {
                getCatagoryResponse.Success = false;
                getCatagoryResponse.Message = "ERROR: No ids match " + id;
                return getCatagoryResponse;
            }
            getCatagoryResponse.CatagoryGot = repo.GetCatagory(id);
            getCatagoryResponse.Success = true;
            getCatagoryResponse.Message = "Success";
            return getCatagoryResponse;
        }

        public Response RemoveCatagory(int id)
        {
            Response response = new Response();
            if (repo.GetAllCategories().All(cat => cat.CatagoryId != id))
            {
                response.Success = false;
                response.Message = "No ids match" + id;
                return response;
            }
            repo.RemoveCatagory(id);
            response.Success = true;
            response.Message = "Success";
            return response;   
        }

        public Response RemoveCatagoryFromBlogPost(int blogId, int categoryId)
        {
            Response response = new Response();
            if ((repo.GetAllCategories().Where(cat => cat.CatagoryId == categoryId).Count()) == 0)
            {
                response.Message = "ERROR: no catagories with id " + categoryId;
                response.Success = false;
                return response;
            }

            else if ((repoBlog.GetAllBlogs().Where(cat => cat.BlogPostId == blogId).Count()) == 0)
            {
                response.Message = "ERROR: no blogs with id " + blogId;
                response.Success = false;
                return response;
            }

            else if (repoBlog.GetBlogsByCatagory(categoryId).All(cat => cat.BlogPostId != blogId))
            {
                response.Success = false;
                response.Message = "ERROR: no blogs in catagory " + categoryId + " have id " + blogId;
                return response;
            }
            repo.RemoveCatagoryFromBlogPost(blogId, categoryId);
            response.Success = true;
            response.Message = "catagory " + repo.GetCatagory(categoryId).CatagoryName + " removed from post with Id= "+blogId;
            return response;
        }

        public Response UpdateCatagory(Catagory catagory)
        {
            Response response = new Response();

            if (repo.GetAllCategories().All(cat => cat.CatagoryId != catagory.CatagoryId))
            {
                response.Success = false;
                response.Message = "No Ids Match " + catagory.CatagoryId;
                return response;
            }
            repo.UpdateCatagory(catagory);
            response.Success = true;
            response.Message = "Catagory Edited";
            return response;
        }
    }
}
