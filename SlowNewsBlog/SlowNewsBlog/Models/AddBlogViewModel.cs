using SlowNewsBlog.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SlowNewsBlog.Models
{

    public class AddBlogViewModel
    {
        [AllowHtml]
        public Catagory Catagory { get; set; }
        [AllowHtml]
        public IEnumerable<SelectListItem> Catagories { get; set; }
        [AllowHtml]
        public BlogPost BlogPost { get; set; }
        [AllowHtml]
        public IEnumerable<SelectListItem> HashTags { get; set; }
        [AllowHtml]
        public List<int> SelectedHashtagIds { get; set; }
    }
}