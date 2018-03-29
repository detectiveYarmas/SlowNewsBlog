﻿using SlowNewsBlog.Data;
using SlowNewsBlog.Data.Repos;
using SlowNewsBlog.Domain.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlowNewsBlog.Domain.Factories
{
    public class BlogPostRepoFactory
    {
        public static BlogPostRepoManager Create()
        {
            switch (Settings.GetRepositoryType())
            {
                case "QA":
                    return new BlogPostRepoManager(new InMemoryBlogPostRepo(), new InMemoryCatagoryRepo());
                case "Prod":
                    return new BlogPostRepoManager( new BlogPostRepo(), new CatagoryRepo());
                default:
                    throw new Exception("Could not find a valid repo configuration.");
            }
        }
    }
}
