using NUnit.Framework;
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
    public class StaticPageRepoTests
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
        public void CanAddStaticPage()
        {
            var repo = new StaticPageRepo();
            var page = new StaticPage();

            page.StaticPageId = 1;
            page.Title = "Disclaimer";
            page.Body = "this is a test.";

            repo.AddStaticPage(page);
            var pages = repo.GetStaticPages();

            Assert.AreEqual(3, pages.Count);
            Assert.AreEqual(3, pages[2].StaticPageId);
            Assert.AreEqual("Disclaimer", pages[2].Title);
            Assert.AreEqual("this is a test.", pages[2].Body);
        }

        [Test]
        public void CanGetStaticPages()
        {
            var repo = new StaticPageRepo();
            var pages = repo.GetStaticPages();

            Assert.AreEqual(2, pages.Count);
            Assert.AreEqual(2, pages[1].StaticPageId);
            Assert.AreEqual("Appologies", pages[1].Title);
            Assert.AreEqual("<p>Nam rutrum aliquam consequat. Pellentesque quis <strong>tortor</strong> vitae neque pretium interdum. Maecenas pellentesque eget mauris ac bibendum. Cras feugiat tincidunt eros, et euismod felis malesuada id. Suspendisse imperdiet porttitor est, ac ultricies urna sagittis pharetra. Quisque vitae orci sit amet diam fermentum pretium. Ut et congue magna. Curabitur leo enim, aliquet a dignissim id, vestibulum non diam. Donec facilisis ullamcorper porta. Cras cursus libero eu dolor convallis pharetra. Pellentesque finibus commodo lacus, sit amet luctus lacus gravida eget.</p><p>Nulla ullamcorper massa vitae maximus fringilla. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Nulla lacinia sapien ut sem faucibus ultrices. Proin et felis elementum neque hendrerit consectetur. Nunc non porta arcu. Nunc facilisis non augue a placerat. Suspendisse vel aliquam nisi. Donec in velit consequat, scelerisque metus quis, ornare ipsum. Maecenas at lobortis elit. Praesent placerat maximus mi. Donec non volutpat lacus. Cras rhoncus odio eget facilisis auctor. Integer venenatis orci eu cursus mattis.</p><p>Morbi condimentum elementum magna quis commodo. Nulla in lectus nisi. Duis scelerisque vulputate turpis dignissim eleifend. Nulla fringilla eget ligula ac ultricies. Maecenas maximus imperdiet ex, et auctor dolor pellentesque nec. Quisque cursus metus non elit pulvinar commodo. Nunc dui quam, luctus eget risus a, tristique tempus sapien. Aenean in semper mauris. Nulla facilisi. Curabitur ullamcorper lorem nibh, id porta nulla consectetur sit amet. Fusce at elit a ligula elementum tempus at eget ex. Nam eu pharetra enim, eget congue orci. Proin rutrum vel arcu ac ornare.</p>", pages[1].Body);
        }

        [Test]
        public void CanRemoveStaticPage()
        {
            var repo = new StaticPageRepo();
            var page = new StaticPage();

            page.StaticPageId = 1;
            page.Title = "fart";
            page.Body = "Hi";

            repo.AddStaticPage(page);

            var loaded = repo.GetStaticPageById(3);
            Assert.IsNotNull(loaded);

            repo.DeleteStaticPage(3);
            loaded = repo.GetStaticPageById(3);

            Assert.IsNull(loaded);
        }

        [Test]
        public void CanUpdateStaticPage()
        {
            var page = new StaticPage();
            var repo = new StaticPageRepo();

            page.StaticPageId = 3;
            page.Title = "Hi";
            page.Body = "Hi";

            repo.AddStaticPage(page);

            page.StaticPageId = 3;
            page.Title = "Bye";
            page.Body = "Bye";

            repo.UpdateStaticPage(page);
            var updatedPage = repo.GetStaticPageById(page.StaticPageId);

            Assert.AreEqual(3, page.StaticPageId);
            Assert.AreEqual("Bye", page.Title);
            Assert.AreEqual("Bye", page.Body);
        }
    }
}
