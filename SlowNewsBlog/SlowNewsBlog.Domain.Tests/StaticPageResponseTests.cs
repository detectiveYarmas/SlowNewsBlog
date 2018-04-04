using NUnit.Framework;
using SlowNewsBlog.Data.Repos;
using SlowNewsBlog.Models.Responses;
using SlowNewsBlog.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlowNewsBlog.Domain.Tests
{
    [TestFixture]
    public class StaticPageResponseTests
    {
        [Test]
        public void GetStaticPagesResponseTest()
        {
            var repo = new InMemoryStaticPageRepo();

            var response = new GetStaticPagesResponse();

            response.Pages = repo.GetStaticPages();

            Assert.IsNotNull(response.Pages);
            Assert.AreEqual(3, response.Pages.Count);
            Assert.AreEqual(1, response.Pages[0].StaticPageId);
            Assert.AreEqual("Hi", response.Pages[0].Title);
            Assert.AreEqual("Hi", response.Pages[0].Body);
        }

        [Test]
        public void AddStaticPageResponse()
        {
            var page = new StaticPage(1, "butt", "butt");

            var repo = new InMemoryStaticPageRepo();
            var response = new AddStaticPageResponse();

            response.Page = repo.AddStaticPage(page);

            Assert.IsNotNull(response.Page);
            Assert.IsTrue(response.Success);
            Assert.AreEqual(4, response.Page.StaticPageId);
            Assert.AreEqual("butt", response.Page.Title);
            Assert.AreEqual("butt", response.Page.Body);
        }
    }
}
