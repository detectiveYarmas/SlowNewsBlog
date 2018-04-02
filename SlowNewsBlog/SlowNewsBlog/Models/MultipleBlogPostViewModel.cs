using SlowNewsBlog.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SlowNewsBlog.Models
{
    public class MultipleBlogPostViewModel
    {
        public Dictionary<int, List<HashTag>> HashTagsForBlogPosts { get; set; }
        public List<BlogPost> BlogPosts { get; set; }
        public List<Catagory> Categories { get; set; }
    }
}
