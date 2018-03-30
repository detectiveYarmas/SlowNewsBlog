using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlowNewsBlog.Models.Tables
{
    public class BlogPost
    {
        public int BlogPostId { get; set; }
        public string Blog { get; set; }
        public string Title { get; set; }
        public bool Approved { get; set; }
        public int? CatagoryId { get; set; }
        public DateTime PublishedDate { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
