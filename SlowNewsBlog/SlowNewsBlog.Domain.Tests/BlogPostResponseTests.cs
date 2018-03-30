using NUnit.Framework;
using SlowNewsBlog.Data.Repos;
using SlowNewsBlog.Domain.Managers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlowNewsBlog.Domain.Tests
{
    [TestFixture]
    class BlogPostResponseTests
    {
        [SetUp]
        public void Init()
        {
            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                var cmd = new SqlCommand();
                cmd.CommandText = "DbReset";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Connection = cn;
                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        [Test]
        public void ManagerGetBlogsByBlogger()
        {
            BlogPostRepoManager blogPostRepoManager = new BlogPostRepoManager(new BlogPostRepo(),new CategoryRepo());
            Assert.IsTrue(blogPostRepoManager.GetBlogByBloger(1).BlogsByBlogger.Count == 1);
        }

        [Test]
        public void ManagerAddBloggerToBlogPost()
        {
            BlogPostRepoManager blogPostRepoManager = new BlogPostRepoManager(new BlogPostRepo(), new CategoryRepo());
            Assert.IsTrue(blogPostRepoManager.AddBloggerToBlogPost(1, 2).Success);
            Assert.IsFalse(blogPostRepoManager.AddBloggerToBlogPost(1,27).Success);
        }

        [Test]
        public void ManagerRemoveBloggerFromBlogPost()
        {
            BlogPostRepoManager blogPostRepoManager = new BlogPostRepoManager(new BlogPostRepo(), new CategoryRepo());
            Assert.IsTrue(blogPostRepoManager.RemoveBloggerFromBlogPost(1, 1).Success);
        }
    }
}
