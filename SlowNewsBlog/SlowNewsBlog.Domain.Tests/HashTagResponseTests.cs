﻿using NUnit.Framework;
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
            
        }
    }
}
