using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SlowNewsBlog.Models
{
    public class GroupedViewModel
    {
        public List<UserViewModel> Bloggers { get; set; }
        public List<UserViewModel> Admins { get; set; }
    }
}