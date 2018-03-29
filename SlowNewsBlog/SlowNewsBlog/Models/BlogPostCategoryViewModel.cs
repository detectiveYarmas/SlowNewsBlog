using SlowNewsBlog.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SlowNewsBlog.Models
{
    public class BlogPostCatagoryViewModel
    {
        public BlogPost Blog { get; set; }
        public Catagory Catagory { get; set; }
    }
}