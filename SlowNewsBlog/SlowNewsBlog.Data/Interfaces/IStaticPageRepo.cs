using SlowNewsBlog.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlowNewsBlog.Data.Interfaces
{
    public interface IStaticPageRepo
    {
        StaticPage GetStaticPageById(int staticPageId);
        List<StaticPage> GetStaticPages();
        StaticPage AddStaticPage(StaticPage page);
        StaticPage UpdateStaticPage(StaticPage page);
        bool DeleteStaticPage(int staticPageId);
    }
}
