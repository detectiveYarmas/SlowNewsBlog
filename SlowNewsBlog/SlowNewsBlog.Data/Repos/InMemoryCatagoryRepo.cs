using SlowNewsBlog.Data.Interfaces;
using SlowNewsBlog.Models.Tables;
using System.Collections.Generic;
using System.Linq;

namespace SlowNewsBlog.Data.Repos
{
    public class InMemoryCatagoryRepo:ICategory
    {
        public static List<BlogPost> blogPosts = InMemoryBlogPostRepo.blogPosts;
        public static List<Catagory> catagories = new List<Catagory>()
        {
            new Catagory (){CatagoryId = 1,CatagoryName = "Science"},
            new Catagory (){CatagoryId= 2,CatagoryName= "Sports"},
            new Catagory (){CatagoryId=3, CatagoryName="Politics"},
            new Catagory (){CatagoryId= 4, CatagoryName= "Entertainment" }
        };

        public void AddCatagoryToBlogPost(int blogPostId, int categoryId)
        {
            BlogPost toSwapFor = blogPosts.Where(b => b.BlogPostId == blogPostId).FirstOrDefault();
            toSwapFor.CatagoryId = categoryId;
            InMemoryBlogPostRepo inMemoryBlogPostRepo = new InMemoryBlogPostRepo();
            inMemoryBlogPostRepo.UpdateBlogPost(toSwapFor);
        }

        public void AddNewCatagory(Catagory catagory)
        {
            catagory.CatagoryId = catagories.Max(id => id.CatagoryId)+1;
            catagories.Add(catagory);
        }
   

        public List<Catagory> GetAllCategories()
        {
            return catagories;
        }

        public Catagory GetCatagory(int id)
        {
            var catagories = GetAllCategories();
            return catagories.Where(h => h.CatagoryId == id).SingleOrDefault();

        }

        public void RemoveCatagory(int id)
        {
            var allCatagories = GetAllCategories();
            var toRemove = GetCatagory(id);

            allCatagories.Remove(toRemove);
        }

        public void RemoveCatagoryFromBlogPost(int blogId, int categoryId)
        {
            BlogPost toSwapFor = blogPosts.Where(b => b.BlogPostId == blogId).FirstOrDefault();
            toSwapFor.CatagoryId = null;
            InMemoryBlogPostRepo inMemoryBlogPostRepo = new InMemoryBlogPostRepo();
            inMemoryBlogPostRepo.UpdateBlogPost(toSwapFor);
        }

        public void UpdateCatagory(Catagory catagory)
        {
            catagories.Remove(catagories.Where(cat => cat.CatagoryId == catagory.CatagoryId).FirstOrDefault());
            catagories.Add(catagory);
        }
    }
}
