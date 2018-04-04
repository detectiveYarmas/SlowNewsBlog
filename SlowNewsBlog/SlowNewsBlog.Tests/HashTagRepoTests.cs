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
    public class HashTagRepoTests
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
            var repo = new HashTagRepo();
            var hashtags = repo.GetAllHashtags();

            Assert.AreEqual(4, hashtags.Count);
            Assert.AreEqual(1, hashtags[0].HashTagId);
            Assert.AreEqual("#metapost", hashtags[0].HashTagName);
        }

        [Test]
        public void CanGetApprovedHashTags()
        {
            var repo = new HashTagRepo();
            var hashtags = repo.GetApprovedHashtags();

            Assert.AreEqual(2, hashtags.Count);
            Assert.AreEqual(1, hashtags[0].HashTagId);
            Assert.AreEqual("#metapost", hashtags[0].HashTagName);
            Assert.IsTrue(hashtags[0].Approved);
        }

        [Test]
        public void CanGetUnapprovedHashTags()
        {
            var repo = new HashTagRepo();
            var hashtags = repo.GetUnapprovedHashtags();

            Assert.AreEqual(2, hashtags.Count);
            Assert.AreEqual(3, hashtags[0].HashTagId);
            Assert.AreEqual("#moon", hashtags[0].HashTagName);
            Assert.IsFalse(hashtags[0].Approved);
        }

        [Test]
        public void CanGetHashtagById()
        {
            var repo = new HashTagRepo();
            var hashtag = repo.GetHashTag(1);

            Assert.AreEqual(1, hashtag.HashTagId);
            Assert.AreEqual("#metapost", hashtag.HashTagName);
        }

        [Test]
        public void CanRemoveHashtag()
        {
            var repo = new HashTagRepo();
            var hashtag = new HashTag();

            hashtag.HashTagId = 1;
            hashtag.HashTagName = "#farts";

            repo.AddHashTag(hashtag);

            var loaded = repo.GetHashTag(5);
            Assert.IsNotNull(loaded);

            repo.RemoveHashTag(5);
            loaded = repo.GetHashTag(5);

            Assert.IsNull(loaded);
        }


        [Test]
        public void CanAddHashtag()
        {
            var repo = new HashTagRepo();
            var hashtag = new HashTag();

            hashtag.HashTagId = 1;
            hashtag.HashTagName = "#farts";
            hashtag.Approved = false;

            repo.AddHashTag(hashtag);
            var hashtags = repo.GetAllHashtags();

            Assert.AreEqual(5, hashtags.Count);
            Assert.AreEqual(5, hashtags[4].HashTagId);
            Assert.AreEqual("#farts", hashtags[4].HashTagName);
            Assert.IsFalse(hashtags[4].Approved);
        }

        [Test]
        public void CanUpdateHashTag()
        {
            HashTag hash = new HashTag();
            var repo = new HashTagRepo();

            hash.HashTagId = 1;
            hash.HashTagName = "#farts";
            hash.Approved = false;

            repo.AddHashTag(hash);

            hash.HashTagId = 1;
            hash.HashTagName = "#farty";
            hash.Approved = true;

            repo.EditHashTag(hash);
            var updatedHash = repo.GetHashTag(hash.HashTagId);

            Assert.AreEqual(1, hash.HashTagId);
            Assert.AreEqual("#farty", hash.HashTagName);
            Assert.IsTrue(hash.Approved);
        }
    }
}
