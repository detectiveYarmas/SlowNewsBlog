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
    public class StaticPageManagerFactory
    {
        public static StaticPageRepoManager Create()
        {
            switch (Settings.GetRepositoryType())
            {
                case "Prod":
                    return new StaticPageRepoManager(new StaticPageRepo());
                default:
                    throw new Exception("Could not find a valid repo configuration");
            }
        }
    }
}
