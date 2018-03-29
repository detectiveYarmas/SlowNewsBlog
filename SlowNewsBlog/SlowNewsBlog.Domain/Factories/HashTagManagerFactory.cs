using SlowNewsBlog.Data;
using SlowNewsBlog.Domain.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlowNewsBlog.Data.InMemoryRepos;

namespace SlowNewsBlog.Domain.Factories
{
    public class HashTagManagerFactory
    {
        public static HashtagRepoManager Create()
        {
            switch (Settings.GetRepositoryType())
            {
                case "QA":
                    return new HashtagRepoManager(new InMemoryHashTagRepo());
                case "Prod":
                    return new HashtagRepoManager(new HashTagRepo());
                default:
                    throw new Exception("Could not find valid repo configuration.");
            }
        }
    }
}
