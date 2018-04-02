using SlowNewsBlog.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SlowNewsBlog.Models
{
    public class SingleBlogPostViewModel
    {
        public BlogPost BlogPost { get; set; }
        public List<Catagory> Catagories { get;set; }
		public List<HashTag> HashTags { get; set; }
	}
}