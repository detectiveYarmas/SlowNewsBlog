using Dapper;
using SlowNewsBlog.Data.Interfaces;
using SlowNewsBlog.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlowNewsBlog.Data.Repos
{
    public class StaticPageRepo : IStaticPageRepo
    {
        public StaticPage AddStaticPage(StaticPage page)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("AddStaticPage", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@staticPageId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);
                cmd.Parameters.AddWithValue("@title", page.Title);
                cmd.Parameters.AddWithValue("@body", page.Body);

                cn.Open();
                cmd.ExecuteNonQuery();

                page.StaticPageId = (int)param.Value;
            }

            return page;
        }

        public bool DeleteStaticPage(int staticPageId)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var param = new DynamicParameters();
                param.Add("@staticPageId", staticPageId);
                cn.Execute("DeleteStaticPage", param, commandType: CommandType.StoredProcedure);
            }

            return true;
        }

        public StaticPage GetStaticPageById(int staticPageId)
        {
            var page = new StaticPage();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@staticPageId", staticPageId);

                page = cn.Query<StaticPage>("GetStaticPageById", parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
            }

            return page;
        }

        public List<StaticPage> GetStaticPages()
        {
            var pages = new List<StaticPage>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                pages = cn.Query<StaticPage>("GetStaticPages", commandType: CommandType.StoredProcedure).ToList();
            }

            return pages;
        }

        public StaticPage UpdateStaticPage(StaticPage page)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@staticPageId", page.StaticPageId);
                parameters.Add("@title", page.Title);
                parameters.Add("@body", page.Body);

                cn.Execute("UpdateStaticPage", parameters, commandType: CommandType.StoredProcedure);
                return page;
            }
        }
    }
}
