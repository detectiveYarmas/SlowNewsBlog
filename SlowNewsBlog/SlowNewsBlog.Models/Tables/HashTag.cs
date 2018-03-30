using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlowNewsBlog.Models.Tables
{
    public class HashTag
    {
        public int HashTagId { get; set; }
        public string HashTagName { get; set; }
        public bool Approved { get; set; }
        public ICollection<BlogPost> HashTagBlogPosts { get; set; }

        public HashTag(int hashTagId, string hashTagName, bool approved)
        {
            HashTagId = hashTagId;
            HashTagName = hashTagName;
            Approved = approved;
        }

        public HashTag()
        {
        }
    }
}
