using SlowNewsBlog.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SlowNewsBlog.Models
{
    public class GroupedBlogViewModel
    {
        public List<BlogPost> ApprovedBlogs { get; set; }
        public List<BlogPost> UnApprovedBlogs { get; set; }
    }
}