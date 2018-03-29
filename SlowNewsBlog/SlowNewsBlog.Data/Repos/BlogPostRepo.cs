using Dapper;
using SlowNewsBlog.Data.Interfaces;
using SlowNewsBlog.Models.Tables;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlowNewsBlog.Data.Repos
{
    public class BlogPostRepo:IBlogPostRepo
    {

        public List<BlogPost> GetAllBlogs()
        {
            using (var sqlConnection = new SqlConnection())
            {
                sqlConnection.ConnectionString = ConfigurationManager
                    .ConnectionStrings["DefaultConnection"]
                    .ConnectionString;

                return sqlConnection.Query<BlogPost>("GetAllBlogs",
                    commandType: CommandType.StoredProcedure).AsList();
            }
        }

        public void AddNewBlogPost(BlogPost post)
        {
            using (var sqlConnection = new SqlConnection())
            {
                sqlConnection.ConnectionString = ConfigurationManager
                    .ConnectionStrings["DefaultConnection"]
                    .ConnectionString;

                var parameters = new DynamicParameters();
                parameters.Add("@blog", post.Blog);
                parameters.Add("@title", post.Title);
                parameters.Add("@blogPostId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            

                sqlConnection.Query<BlogPost>("AddNewBlogPost", 
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void UpdateBlogPost(BlogPost blogPost)
        {
            using (var sqlConnection = new SqlConnection())
            {
                sqlConnection.ConnectionString = ConfigurationManager
                    .ConnectionStrings["DefaultConnection"]
                    .ConnectionString;

                var parameters = new DynamicParameters();
                parameters.Add("@blogPostId", blogPost.BlogPostId);
                parameters.Add("@blogPost", blogPost.Blog);

                sqlConnection.Query<BlogPost>("UpdateBlogPost",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public BlogPost GetBlog(int id)
        {
            using (var sqlConnection = new SqlConnection())
            {
                sqlConnection.ConnectionString = ConfigurationManager
                    .ConnectionStrings["DefaultConnection"]
                    .ConnectionString;

                var parameters = new DynamicParameters();
                parameters.Add("@blogId", id);

                return sqlConnection.Query<BlogPost>("GetBlog",
                    parameters,
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public List<BlogPost> GetAllApprovedBlogPosts()
        {
            using (var sqlConnection = new SqlConnection())
            {
                sqlConnection.ConnectionString = ConfigurationManager
                    .ConnectionStrings["DefaultConnection"]
                    .ConnectionString;


                return sqlConnection.Query<BlogPost>("GetAllApprovedBlogPosts",
                    
                    commandType: CommandType.StoredProcedure).AsList();
            }
        }

        public List<BlogPost> GetAllDisapprovedBlogPosts()
        {
            using (var sqlConnection = new SqlConnection())
            {
                sqlConnection.ConnectionString = ConfigurationManager
                    .ConnectionStrings["DefaultConnection"]
                    .ConnectionString;

                return sqlConnection.Query<BlogPost>("GetAllDisapprovedBlogPosts",

                    commandType: CommandType.StoredProcedure).AsList();
            }
        }

        public void AddBloggerToBlogPost(int blogger, int blogPost)
        {
            using (var sqlConnection = new SqlConnection())
            {
                sqlConnection.ConnectionString = ConfigurationManager
                    .ConnectionStrings["DefaultConnection"]
                    .ConnectionString;

                var parameters = new DynamicParameters();
                parameters.Add("@bloggerId", blogger);
                parameters.Add("@blogPostId", blogPost);

                sqlConnection.Query<BlogPost>("AddBloggerToBlogPost",
                    parameters,
                    commandType: CommandType.StoredProcedure).AsList();
            }
        }

        public List<BlogPost> GetBlogsByBlogger(int id)
        {
            using (var sqlConnection = new SqlConnection())
            {
                sqlConnection.ConnectionString = ConfigurationManager
                    .ConnectionStrings["DefaultConnection"]
                    .ConnectionString;

                var parameters = new DynamicParameters();
                parameters.Add("@bloggerId", id);

                return sqlConnection.Query<BlogPost>("GetBlogsByBlogger",
                    parameters,
                    commandType: CommandType.StoredProcedure).AsList();
            }
        }

        public void DisapproveBlog(int id)
        {
            using (var sqlConnection = new SqlConnection())
            {
                sqlConnection.ConnectionString = ConfigurationManager
                    .ConnectionStrings["DefaultConnection"]
                    .ConnectionString;

                var parameters = new DynamicParameters();
                parameters.Add("@blogPostId", id);

                 sqlConnection.Query<BlogPost>("DisapproveBlog",
                    parameters,
                    commandType: CommandType.StoredProcedure).AsList();
            }
        }

        public void ApproveBlog(int id)
        {
            using (var sqlConnection = new SqlConnection())
            {
                sqlConnection.ConnectionString = ConfigurationManager
                    .ConnectionStrings["DefaultConnection"]
                    .ConnectionString;

                var parameters = new DynamicParameters();
                parameters.Add("@blogPostId", id);

                sqlConnection.Query<BlogPost>("ApproveBlog",
                   parameters,
                   commandType: CommandType.StoredProcedure).AsList();
            }
        }

        public void RemoveBloggerFromBlogPost(int bloggerId, int blogPostId)
        {
            using (var sqlConnection = new SqlConnection())
            {
                sqlConnection.ConnectionString = ConfigurationManager
                    .ConnectionStrings["DefaultConnection"]
                    .ConnectionString;

                var parameters = new DynamicParameters();
                parameters.Add("@blogPostId", blogPostId);
                parameters.Add("@bloggerId", bloggerId);

                sqlConnection.Query<BlogPost>("RemoveBloggerFromBlogPost",
                   parameters,
                   commandType: CommandType.StoredProcedure);
            }
        }

        public List<BlogPost> GetNewestBlogs()
        {
            using (var sqlConn = new SqlConnection(Settings.GetConnectionString()))
            {
                return sqlConn.Query<BlogPost>("GetNewestBlogs",
                    commandType: CommandType.StoredProcedure).AsList();
            }
        }
    }
}
