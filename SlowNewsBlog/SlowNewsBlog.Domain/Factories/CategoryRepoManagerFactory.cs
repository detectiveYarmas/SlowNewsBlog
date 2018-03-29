using SlowNewsBlog.Data;
using SlowNewsBlog.Data.Repos;
using SlowNewsBlog.Domain.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlowNewsBlog.Domain.Factories
{
    public class CategoryRepoManagerFactory
    {
        public static CategoryRepoManager Create()
        {
            switch (Settings.GetRepositoryType())
            {
                case "QA":
                    return new CategoryRepoManager(new InMemoryCatagoryRepo(),new InMemoryBlogPostRepo());
                case "Prod":
                    return new CategoryRepoManager(new CategoryRepo(),new BlogPostRepo());
                default:
                    throw new Exception("Could not find a valid repo configuration.");
            }
        }
    }
}
