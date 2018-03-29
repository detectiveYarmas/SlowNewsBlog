using SlowNewsBlog.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlowNewsBlog.Models.Responses
{
    public class GetBlogsByCatagoryResponse:Response
    {
        public List<BlogPost> BlogsInCatagory { get; set; }
    }
}
