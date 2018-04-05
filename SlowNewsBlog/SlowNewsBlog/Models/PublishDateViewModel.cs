using SlowNewsBlog.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SlowNewsBlog.Models
{
    public class PublishDateViewModel
    {
        public int BlogPostId { get; set; }
        public string Blog { get; set; }
        public string Title { get; set; }
        public bool Approved { get; set; }
        public int CatagoryId { get; set; }

        [DataType(DataType.Date)]
        public DateTime? PublishedDate { get; set; }
        public DateTime DateAdded { get; set; }
        public string Id { get; set; }
        public string HeaderImage { get; set; }
        public ICollection<HashTag> BlogPostHashTags { get; set; }
        public string UserName { get; set; }
    }

}