using NUnit.Framework;
using SlowNewsBlog.Data;
using SlowNewsBlog.Data.Repos;
using SlowNewsBlog.Models.Tables;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlowNewsBlog.Tests
{
    [TestFixture]
    public class CatagoryRepoTests
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
        public void CanGetAllCategories()
        {
            var repo = new CategoryRepo();
            var catagory = repo.GetAllCategories();

            Assert.AreEqual(8, catagory.Count);
            Assert.AreEqual(1, catagory[0].CatagoryId);
            Assert.AreEqual("Politics", catagory.FirstOrDefault().CatagoryName);
        }

        [Test]
        public void CanAddNewCatagory()
        {
            var newCat = new Catagory()
            {
                CatagoryName = "The Pentagon Papers"
            };

            var repo = new CategoryRepo();
            var catagories = repo.GetAllCategories();
            Assert.AreEqual(8, catagories.Count);
            repo.AddNewCatagory(newCat);
            catagories = repo.GetAllCategories();
            Assert.AreEqual(9, catagories.Count);
            Assert.AreEqual("The Pentagon Papers", catagories[8].CatagoryName);
        }

        [Test]
        public void CanGetCatagory()
        {
            var repo = new CategoryRepo();
            Assert.AreEqual("Politics", repo.GetCatagory(1).CatagoryName);
        }

        [Test]
        public void CanRemoveCatagory()
        {
            var repo = new CategoryRepo();
            Assert.AreEqual("Politics", repo.GetAllCategories().FirstOrDefault().CatagoryName);
            repo.RemoveCatagory(1);
            Assert.IsFalse(repo.GetAllCategories().Any(c => c.CatagoryName == "Politics"));
            Assert.AreEqual(7, repo.GetAllCategories().Count);
        }

    

        [Test]
        public void CanUpdateCatagory()
        {
            var repo = new CategoryRepo();
            var cat = new Catagory()
            {
                CatagoryId = 1,
                CatagoryName ="Jobs"
            };
            var list = repo.GetAllCategories();
            Assert.AreEqual(8, list.Count);
            Assert.IsTrue(list.FirstOrDefault().CatagoryName == "Politics");
            repo.UpdateCatagory(cat);
            list = repo.GetAllCategories();
            Assert.IsTrue(list.FirstOrDefault().CatagoryName == "Jobs");
        }

        [Test]
        public void CanGetBlogsByCatagory()
        {
            var repo = new CategoryRepo();
            Assert.AreEqual(8, repo.GetAllCategories().Count);
            Assert.AreEqual(1, repo.GetBlogsByCatagory(2).Count);
        }

        [Test]
        public void CanRemoveCatagoryFromBlogPost()
        {
            var repo = new CategoryRepo();
            Assert.AreEqual(8, repo.GetAllCategories().Count);
            Assert.AreEqual(1, repo.GetBlogsByCatagory(2).Count);
            repo.RemoveCatagoryFromBlogPost(1, 2);
            Assert.AreEqual(0, repo.GetBlogsByCatagory(2).Count);
        }
    }
}
