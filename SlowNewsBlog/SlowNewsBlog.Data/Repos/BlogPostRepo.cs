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

        public BlogPost AddNewBlogPost(BlogPost post)
        {
            using (var sqlConnection = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("AddNewBlogPost", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@BlogPostId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);
                cmd.Parameters.AddWithValue("@blog", post.Blog);
                cmd.Parameters.AddWithValue("@title", post.Title);
                cmd.Parameters.AddWithValue("@CatagoryId", post.CatagoryId);
                cmd.Parameters.AddWithValue("@HeaderImage", post.HeaderImage);

                sqlConnection.Open();

                cmd.ExecuteNonQuery();

                post.BlogPostId = (int)param.Value;
            }

            return post;
        }

         public List<BlogPost> GetNewestBlogs()
        {
            using (var sqlConn = new SqlConnection(Settings.GetConnectionString()))
            {
                return sqlConn.Query<BlogPost>("GetNewestBlogs",
                    commandType: CommandType.StoredProcedure).AsList();
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

        

        public List<BlogPost> GetBlogsByBlogger(string id)
        {
            using (var sqlConnection = new SqlConnection())
            {
                sqlConnection.ConnectionString = ConfigurationManager
                    .ConnectionStrings["DefaultConnection"]
                    .ConnectionString;

                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);

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
        

        public List<BlogPost> GetBlogsByCatagory(int id)
        {
            using (var sqlConnection = new SqlConnection())
            {
                sqlConnection.ConnectionString = ConfigurationManager
                    .ConnectionStrings["DefaultConnection"]
                    .ConnectionString;

                var parameters = new DynamicParameters();
                parameters.Add("@catagoryId", id);

                return sqlConnection.Query<BlogPost>("GetBlogsByCatagory",
                   parameters,
                   commandType: CommandType.StoredProcedure).AsList();
            }
        }

        public List<BlogPost> GetBlogsByHashTag(int hashId)
        {
            using (var sqlConn = new SqlConnection(Settings.GetConnectionString()))
            {
                var param = new DynamicParameters();
                param.Add("@hashTagId", hashId);
                return sqlConn.Query<BlogPost>("GetBlogsByHashTag", param, commandType: CommandType.StoredProcedure).AsList();

            }
        }
    }
}
