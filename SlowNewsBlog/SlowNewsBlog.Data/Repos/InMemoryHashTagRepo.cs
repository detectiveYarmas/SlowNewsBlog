using SlowNewsBlog.Data.Interfaces;
using SlowNewsBlog.Data.Repos;
using SlowNewsBlog.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlowNewsBlog.Data.InMemoryRepos
{
    public class InMemoryHashTagRepo : IHashTagRepo
    {
        public static List<HashTag> hashTags = new List<HashTag>()
        {
            new HashTag (1, "#metapost", true),
            new HashTag (2, "#horseandbuggy", true),
            new HashTag (3, "#moon", false),
            new HashTag (4, "#y2k", false)
        };

        public HashTag AddHashTag(HashTag hashtag)
        {
            hashtag.HashTagId = 5;
            hashtag.HashTagName = "#fart";
            hashtag.Approved = false;

            var hashtags = GetAllHashtags();
            hashtags.Add(hashtag);

            return hashtag;
        }

        public void AddHashTagToBlog(int hash, int post)
        {
            throw new NotImplementedException();
        }

        public HashTag EditHashTag(HashTag hash)
        {
            var oldHashTag = GetHashTag(hash.HashTagId);
            HashTag newHashTag = new HashTag();

            newHashTag.HashTagId = oldHashTag.HashTagId;
            newHashTag.HashTagName = oldHashTag.HashTagName;
            newHashTag.Approved = oldHashTag.Approved;

            return newHashTag;

        }

        public List<HashTag> GetAllHashtags()
        {
            return hashTags;
        }

        public List<HashTag> GetApprovedHashtags()
        {
            List<HashTag> approved = new List<HashTag>();

            var hashtags = GetAllHashtags();

            foreach(var hashtag in hashTags)
            {
                if(hashtag.Approved == true)
                {
                    approved.Add(hashtag);
                }
            }

            return approved;
        }

        public HashTag GetHashTag(int id)
        {
            HashTag hashTag = new HashTag();
            var hashtags = GetAllHashtags();

            hashTag = hashtags.Where(h => h.HashTagId == id).SingleOrDefault();
            return hashTag;
        }

        public List<HashTag> GetHashTagsForBlog(int blogPostId)
        {
            var hashtags = new List<HashTag>();

            return hashtags;
        }

        public List<HashTag> GetUnapprovedHashtags()
        {
            List<HashTag> unApproved = new List<HashTag>();
            var hashtags = GetAllHashtags();
            unApproved = hashtags.Where(h => h.Approved == false).ToList();

            return unApproved;
        }

        public bool RemoveHashTag(int id)
        {
            var hashtags = GetAllHashtags();
            var toRemove = GetHashTag(id);

            hashtags.Remove(toRemove);
            return true;
        }

        public void RemoveHashTagsFromBlog(int blog)
        {
            throw new NotImplementedException();
        }
    }
}
