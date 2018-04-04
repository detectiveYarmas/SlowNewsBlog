using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlowNewsBlog.Models.Tables
{
    public class StaticPage
    {
        public int StaticPageId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        public StaticPage(int staticPageId, string title, string body)
        {
            StaticPageId = staticPageId;
            Title = title;
            Body = body;
        }

        public StaticPage()
        {
        }
    }
}
