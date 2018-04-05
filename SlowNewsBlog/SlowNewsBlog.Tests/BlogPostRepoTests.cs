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
    class BlogPostRepoTests
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
        public void CanDisapproveBlog()
        {
            var repo = new BlogPostRepo();
            Assert.IsTrue(repo.GetAllApprovedBlogPosts().Count == 2);
            repo.DisapproveBlog(1);
            Assert.IsTrue(repo.GetAllApprovedBlogPosts().Count == 1);
        }

        [Test]
        public void CanApproveBlog()
        {
            var repo = new BlogPostRepo();
            repo.DisapproveBlog(1);
            Assert.IsTrue(repo.GetAllApprovedBlogPosts().Count == 1);
            repo.ApproveBlog(1);
            Assert.IsTrue(repo.GetAllApprovedBlogPosts().Count == 2);
        }


        [Test]
        public void CanAddNewBlogPost()
        {
            DateTime dateTime = new DateTime().ToLocalTime();

            BlogPost blogPost = new BlogPost()
            {
                Id = "ec322f27-8f78-4aac-b593-5961688fdcbd",
                HeaderImage = "gogog",
                Approved=false,
                Blog="This is a test Blog",
                DateAdded = dateTime,
                Title="Test",  
                CatagoryId = 1
            };
            var repo = new BlogPostRepo();
            repo.AddNewBlogPost(blogPost);

            Assert.AreEqual(repo.GetAllBlogs().LastOrDefault().Title, "Test");
        }

        [Test]
        public void CanGetAllBlogs()
        {
            var repo = new BlogPostRepo();
            Assert.AreEqual(3, repo.GetAllBlogs().Count);
        }

        [Test]
        public void CanGetBlog()
        {
            var repo = new BlogPostRepo();
            Assert.AreEqual("The Great Moon Escape", repo.GetBlog(1).Title);
        }

       
        [Test]
        public void CanGetBlogsByCatagory()
        {
            var repo = new BlogPostRepo();
            Assert.AreEqual(1, repo.GetBlogsByCatagory(2).Count);
        }

        [Test]
        public void CanUpdateBlogPost()
        {
            DateTime dateTime = new DateTime().ToLocalTime();

            BlogPost blogPost = new BlogPost()
            {
                BlogPostId = 1,
                Approved = false,
                Blog = "This is a test Blog",
                DateAdded = dateTime,
                Title = "Test",
                CatagoryId = 1,
            };

            var repo = new BlogPostRepo();
            Assert.AreEqual(repo.GetBlog(1).Blog, "<p>Vivamus tortor felis, ornare ut tempus ac, imperdiet accumsan ante. Etiam ut porta leo, tempus euismod magna. Aliquam interdum rhoncus feugiat. Sed lacinia interdum sapien. Suspendisse viverra, ipsum non fringilla scelerisque, ipsum augue pellentesque est, nec vehicula quam augue nec urna. Nullam nulla turpis, imperdiet a porttitor vitae, tempus non nisi. Nulla non placerat nunc. Nam vitae orci sit amet ipsum blandit imperdiet et sit amet enim. Ut eget posuere massa, quis ultrices magna. Proin euismod ligula tempus mi pulvinar, non molestie lectus ornare. In dignissim lacus ligula, maximus efficitur nunc sodales sed. Proin ac ligula bibendum, porta arcu eget, commodo tortor. Nulla vitae magna semper lacus egestas cursus. Proin feugiat dignissim sapien, eget sagittis diam condimentum eu. Morbi porttitor augue vel ante varius placerat. Proin aliquam a odio sit amet sagittis.</p><p>Sed gravida dui ut nulla mollis, non malesuada massa feugiat. Etiam finibus odio ultricies mauris pretium, in dictum lorem aliquet. Phasellus vulputate sollicitudin tempor. Aliquam erat volutpat. Etiam tincidunt orci eu sapien aliquet, eu semper elit facilisis. Suspendisse potenti. Nulla facilisi. Mauris vel lectus ut ex porttitor lacinia at eget justo. Donec in odio varius, dapibus augue id, vehicula lorem. Suspendisse venenatis risus in magna dictum suscipit. Curabitur vestibulum leo volutpat, tempor nunc a, posuere mauris. Sed auctor justo non turpis tempus maximus. Phasellus arcu sapien, laoreet ut libero ut, mattis mollis ante. Sed scelerisque congue porttitor.</p><p>Vivamus accumsan, justo eget venenatis viverra, orci arcu efficitur purus, eu pellentesque diam lacus sit amet mauris. Pellentesque ut pharetra leo. Quisque odio purus, lacinia eu eros in, placerat tincidunt dolor. Proin magna eros, tempor vel nulla at, rutrum lacinia libero. Sed varius fringilla condimentum. Donec sit amet varius ex, nec pellentesque risus. Donec in tortor at purus dapibus aliquet.</p><p>Aliquam malesuada orci non lacus egestas fermentum. Sed aliquet, dui eu malesuada aliquam, nibh dolor hendrerit est, sit amet pretium lacus metus nec lectus. Cras eleifend velit non magna dapibus sagittis. Donec commodo malesuada tellus non commodo. Vivamus ac velit metus. Duis ac vestibulum leo. Vivamus cursus lorem felis, placerat accumsan metus sagittis id. Nulla neque neque, lacinia id iaculis placerat, ultrices nec enim. Curabitur porta volutpat massa, vitae lobortis ex malesuada suscipit. Duis rutrum, lacus ut auctor fringilla, sem nisl aliquet dui, quis feugiat libero ligula et ligula. Ut ex mi, rutrum eget vestibulum et, euismod eu augue. Maecenas dolor est, rutrum eu mattis dignissim, finibus vehicula odio. Suspendisse eget velit sit amet est tincidunt condimentum ut ac dui. Nullam tincidunt nulla elit, at efficitur turpis tristique id.</p><p>Quisque euismod mauris sit amet augue gravida, sed eleifend felis vestibulum. Cras vel dignissim arcu, sit amet ultricies tellus. Aenean imperdiet venenatis sapien ac eleifend. Praesent eu accumsan nisl, non pretium mi. Vivamus aliquam ligula non volutpat malesuada. In non elit eleifend, tempor turpis at, dictum lacus. Integer arcu dui, faucibus consectetur dui eu, dictum suscipit tellus. Praesent sit amet ante convallis, congue tortor ut, consequat erat. Sed non felis in diam pulvinar cursus. Cras molestie dui ut rhoncus sollicitudin. Donec posuere nulla sed nisl fermentum rutrum. Donec vitae rhoncus ex, at tempus felis.</p><p>Nunc non commodo eros. Quisque pellentesque mi vel magna dapibus ullamcorper elementum nec leo. Nunc accumsan lorem ipsum. Duis porttitor, nibh id laoreet euismod, felis diam rutrum neque, non viverra est erat eu ex. Mauris dictum velit a purus mattis rhoncus. Donec luctus malesuada tortor, sit amet blandit lectus accumsan at. Proin placerat vehicula leo, vel dapibus ligula dictum non. Maecenas sapien magna, venenatis eget dictum sagittis, maximus ac mauris. Aenean sit amet dictum justo. Sed nec egestas turpis, ac porta nibh. Integer ultrices orci sed lacus consectetur faucibus. Suspendisse nec neque at justo malesuada euismod ut ut arcu. Vestibulum finibus lacinia nisi. Fusce consequat tellus ut ipsum euismod, eu scelerisque lacus lacinia. In placerat nisl vel lorem euismod consequat.</p><p>Etiam tempor volutpat turpis, ut vulputate magna eleifend ut. Duis felis ipsum, pulvinar ac ligula eget, semper lacinia turpis. Pellentesque tincidunt risus et sapien pellentesque rutrum. Nunc eu libero porta, posuere ex ut, ornare dolor. Vivamus non interdum velit, in elementum eros. Quisque vitae laoreet nisi, et vestibulum orci. In sit amet tincidunt tellus, nec tincidunt lorem. Fusce at nisl id massa ultricies congue. Proin elit leo, pretium nec lobortis ac, fermentum sit amet est. Maecenas nec ultrices nulla, sit amet mattis nunc.</p><p>Fusce et tempor sapien, et pretium neque. Donec laoreet vulputate mollis. Donec ipsum tellus, pulvinar ut sapien nec, tincidunt ultrices dui. Vestibulum in neque arcu. Aliquam tincidunt odio eget lectus accumsan, eu commodo quam mattis. Integer ultricies enim at ex ullamcorper vulputate. Suspendisse potenti. Phasellus eu molestie purus. Donec non viverra elit. Sed gravida imperdiet volutpat. Phasellus ornare rutrum nisi vel tincidunt.</p>");
            repo.UpdateBlogPost(blogPost);
            Assert.AreEqual(repo.GetBlog(1).Blog, "This is a test Blog");
        }

        [Test]
        public void CanGetAllApprovedBlogPosts()
        {
            var repo = new BlogPostRepo();
            Assert.AreEqual(repo.GetAllApprovedBlogPosts().Count,2);
        }

        [Test]
        public void CanGetAllDisapprovedBlogPosts()
        {
            var repo = new BlogPostRepo();
            Assert.AreEqual(repo.GetAllDisapprovedBlogPosts().Count, 1);
        }

        [Test]
        public void CanGetNewestBlogs()
        {
            var repo = new BlogPostRepo();
            Assert.AreEqual(1, repo.GetNewestBlogs().Count);
        }

        [Test]
        public void CanDeleteBlog()
        {
            var repo = new BlogPostRepo();
            repo.RemoveBlog(1);
            var blog = repo.GetBlog(1);

            Assert.IsNull(blog);
        }

        [Test]
        public void CanUpdatePublishDate()
        {
            DateTime publishDate = DateTime.Parse("2/1/2010");
            var repo = new BlogPostRepo();
            repo.SetPublishDate(1, publishDate);
            Assert.AreSame(repo.GetBlog(1).PublishedDate, publishDate);
        }
    }
}
