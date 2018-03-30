using NUnit.Framework;
using SlowNewsBlog.Domain.Factories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlowNewsBlog.Models.Responses;
using SlowNewsBlog.Models.Tables;

namespace SlowNewsBlog.Domain.Tests
{
    [TestFixture]
    public class HashTagResponseTests
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
        public void CanGetHashTags()
        {
            var manager = HashTagManagerFactory.Create();
            var response = manager.GetAllHashTags();

            Assert.IsNotNull(response.HashTags);
            Assert.IsTrue(response.Success);
        }

        [Test]
        public void CanGetHashtagById()
        {
            var manager = HashTagManagerFactory.Create();
            var response = new GetHashTagResponse();
            response = manager.GetHashTag(1);

            Assert.IsNotNull(response.HashTag);
            Assert.IsTrue(response.Success);
            Assert.AreEqual(1, response.HashTag.HashTagId);
            Assert.AreEqual("#metapost", response.HashTag.HashTagName);
        }

        [Test]
        public void CanGetApprovedHashTags()
        {
            var manager = HashTagManagerFactory.Create();
            var response = manager.GetApprovedHashtags();

            Assert.IsNotNull(response.HashTags);
            Assert.IsTrue(response.Success);
            Assert.AreEqual(1, response.HashTags[0].HashTagId);
            Assert.AreEqual("#metapost", response.HashTags[0].HashTagName);
            Assert.IsTrue(response.HashTags[0].Approved);
        }

        [Test]
        public void CanGetUnapprovedHashTags()
        {
            var manager = HashTagManagerFactory.Create();
            var response = manager.GetUnapprovedHashtags();

            Assert.IsNotNull(response.HashTags);
            Assert.IsTrue(response.Success);
            Assert.AreEqual(3, response.HashTags[0].HashTagId);
            Assert.AreEqual("#moon", response.HashTags[0].HashTagName);
            Assert.IsFalse(response.HashTags[0].Approved);
        }
        [Test]
        public void CanAddHashTag()
        {
            HashTag hashtag = new HashTag(5, "#fart", false);
            var manager = HashTagManagerFactory.Create();
            var response = manager.AddHashTag(hashtag);

            Assert.IsNotNull(response.Hashtag);
            Assert.IsTrue(response.Success);
            Assert.AreEqual(5, response.Hashtag.HashTagId);
            Assert.AreEqual("#fart", response.Hashtag.HashTagName);
            Assert.IsFalse(response.Hashtag.Approved);
        }

        [Test]
        public void CanEditHashTag()
        {
            var manager = HashTagManagerFactory.Create();
            var response = manager.GetHashTag(1);
            response.HashTag.HashTagName = "#poop";
            response.HashTag.Approved = true;

            var edited = manager.EditHashTag(response.HashTag);

            Assert.IsNotNull(edited.HashTag);
            Assert.IsTrue(edited.Success);
            Assert.AreEqual(1, edited.HashTag.HashTagId);
            Assert.AreEqual("#poop", edited.HashTag.HashTagName);
            Assert.AreEqual(true, edited.HashTag.Approved);
        }
    }
}
