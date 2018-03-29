using SlowNewsBlog.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using System.Data;
using SlowNewsBlog.Models.Tables;

namespace SlowNewsBlog.Data
{
    public class HashTagRepo : IHashTagRepo
    {
        public HashTag AddHashTag(HashTag hashtag)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@hashTagId", hashtag.HashTagId, DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@hashTagName", hashtag.HashTagName);

                cn.Execute("AddNewHashTag", parameters, commandType: CommandType.StoredProcedure);

                return hashtag;
            }
        }

        public HashTag EditHashTag(HashTag hash)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@hashTagId", hash.HashTagId);
                parameters.Add("@hashTagName", hash.HashTagName);
                parameters.Add("@Approved", hash.Approved);

                cn.Execute("HashTagUpdate", parameters, commandType: CommandType.StoredProcedure);
                return hash;
            }               
        }

        public List<HashTag> GetAllHashtags()
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var hashtags = cn.Query<HashTag>("GetAllHashTags", commandType: CommandType.StoredProcedure).ToList();
                return hashtags;
            }
        }

        public List<HashTag> GetApprovedHashtags()
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var hashtags = cn.Query<HashTag>("GetAllApprovedHashTags", commandType: CommandType.StoredProcedure).ToList();
                return hashtags;
            }
        }

        public HashTag GetHashTag(int id)
        {
            
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@hashTagId", id);

                var hashtag = cn.Query<HashTag>("GetHashTag", parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
                return hashtag;
            }

        }

        public List<HashTag> GetHashTagsForBlog(int blogPostId)
        {
            using (var con = new SqlConnection(Settings.GetConnectionString()))
            {
                var param = new DynamicParameters();
                param.Add("@blogPostId", blogPostId);

                var hashtags = con.Query<HashTag>("GetHashTagsForBlogPost", param, commandType: CommandType.StoredProcedure).AsList();
                return hashtags;
            }
            
        }

        public List<HashTag> GetUnapprovedHashtags()
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var hashtags = cn.Query<HashTag>("GetAllUnapprovedHashTags", commandType: CommandType.StoredProcedure).ToList();
                return hashtags;
            }
        }

        public void RemoveHashTag(int id)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("RemoveHashTag", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@hashTagId", id);
                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }
    }
}
