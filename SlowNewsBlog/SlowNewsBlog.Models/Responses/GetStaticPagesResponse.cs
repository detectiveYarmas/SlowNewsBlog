using SlowNewsBlog.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlowNewsBlog.Models.Responses
{
    public class GetStaticPagesResponse : Response
    {
        public List<StaticPage> Pages { get; set; }
    }
}
