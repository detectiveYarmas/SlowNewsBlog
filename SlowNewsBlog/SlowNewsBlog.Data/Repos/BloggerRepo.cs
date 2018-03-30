using Dapper;
using SlowNewsBlog.Data.Interfaces;
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
    public class BloggerRepo:IBloggerRepo
    {
        public List<string> GetAll()
        {
            using (var sqlConnection = new SqlConnection())
            {
                sqlConnection.ConnectionString = ConfigurationManager
                    .ConnectionStrings["DefaultConnection"]
                    .ConnectionString;

                return sqlConnection.Query<string>("GetAllBloggers",
                    commandType: CommandType.StoredProcedure).AsList();
            }
        }
    }
}
