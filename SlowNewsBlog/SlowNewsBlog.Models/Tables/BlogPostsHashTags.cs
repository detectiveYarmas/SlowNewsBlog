using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlowNewsBlog.Models.Tables
{
    public class BlogPostsHashTags
    {
        public int HashTagId { get; set; }
        public int BlogPostId { get; set; }
        public int VoteCount { get; set; }
        public bool Approved { get; set; }
    }
}
