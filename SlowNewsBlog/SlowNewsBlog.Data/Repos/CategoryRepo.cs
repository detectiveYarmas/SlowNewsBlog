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
    public class CategoryRepo:ICategory
    {

        
        public void AddCatagoryToBlogPost(int categoryId,int blogId)
        {
            using (var sqlConnection = new SqlConnection())
            {
                sqlConnection.ConnectionString = ConfigurationManager
                    .ConnectionStrings["DefaultConnection"]
                    .ConnectionString;

                var parameters = new DynamicParameters();
                parameters.Add("@catagoryId", categoryId);
                parameters.Add("@blogPostId", blogId);

                sqlConnection.Query<BlogPost>("AddCatagoryToBlogPost", parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void AddNewCatagory(Catagory category)
        {
            using (var sqlConnection = new SqlConnection())
            {
                sqlConnection.ConnectionString = ConfigurationManager
                    .ConnectionStrings["DefaultConnection"]
                    .ConnectionString;

                var parameters = new DynamicParameters();
                parameters.Add("@catagoryId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@catagoryName", category.CatagoryName);

                sqlConnection.Query<BlogPost>("AddNewCatagory", parameters,
                    commandType: CommandType.StoredProcedure);

                category.CatagoryId = parameters.Get<int>("@catagoryId");
            }
        }

        public List<Catagory> GetAllCategories()
        {
            using (var sqlConnection = new SqlConnection())
            {
                sqlConnection.ConnectionString = ConfigurationManager
                    .ConnectionStrings["DefaultConnection"]
                    .ConnectionString;

                return sqlConnection.Query<Catagory>("GetAllCatagories",
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

                return sqlConnection.Query<BlogPost>("GetBlogsByCatagory", parameters,
                    commandType: CommandType.StoredProcedure).AsList();
            }        
        }

        public Catagory GetCatagory(int id)
        {
            using (var sqlConnection = new SqlConnection())
            {
                sqlConnection.ConnectionString = ConfigurationManager
                   .ConnectionStrings["DefaultConnection"]
                   .ConnectionString;

                var parameters = new DynamicParameters();
                parameters.Add("@catagoryId", id);

                return sqlConnection.Query<Catagory>("GetCatagory", parameters,
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public void RemoveCatagory(int id)
        {
            using (var sqlConnection = new SqlConnection())
            {
                sqlConnection.ConnectionString = ConfigurationManager
                   .ConnectionStrings["DefaultConnection"]
                   .ConnectionString;

                var parameters = new DynamicParameters();
                parameters.Add("@catagoryId", id);

                sqlConnection.Query<Catagory>("RemoveCatagory", parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void RemoveCatagoryFromBlogPost(int blogId, int categoryId)
        {
            using (var sqlConnection = new SqlConnection())
            {
                sqlConnection.ConnectionString = ConfigurationManager
                   .ConnectionStrings["DefaultConnection"]
                   .ConnectionString;

                var parameters = new DynamicParameters();
                parameters.Add("@blogPostId", blogId);
                parameters.Add("@catagoryId", categoryId);


                sqlConnection.Query<Catagory>("RemoveCatagoryFromBlogPost", parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void UpdateCatagory(Catagory catagory)
        {
            using (var sqlConnection = new SqlConnection())
            {
                sqlConnection.ConnectionString = ConfigurationManager
                   .ConnectionStrings["DefaultConnection"]
                   .ConnectionString;

                var parameters = new DynamicParameters();
                parameters.Add("@catagoryName", catagory.CatagoryName);
                parameters.Add("@catagoryId", catagory.CatagoryId);

                sqlConnection.Query<Catagory>("UpdateCatagory", parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
