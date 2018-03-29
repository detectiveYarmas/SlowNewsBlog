using SlowNewsBlog.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlowNewsBlog.Data.Interfaces
{
    public interface ICategory
    {
        List<Catagory> GetAllCategories();
        void AddNewCatagory(Catagory category);
        void AddCatagoryToBlogPost(int blogPostId, int categoryId);//
        List<BlogPost> GetBlogsByCatagory(int id);//
        Catagory GetCatagory(int id);
        void RemoveCatagory(int id);
        void RemoveCatagoryFromBlogPost(int blogId, int categoryId);//
        void UpdateCatagory(Catagory catagory);
    }
}
