using SlowNewsBlog.Data.Interfaces;
using SlowNewsBlog.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlowNewsBlog.Data.Repos
{
    public class InMemoryStaticPageRepo : IStaticPageRepo
    {
        public static List<StaticPage> pages = new List<StaticPage>()
        {
            new StaticPage (1, "Hi", "Hi"),
            new StaticPage (2, "Bye", "Bye"),
            new StaticPage (3, "Hola", "Hola")
        };

        public StaticPage AddStaticPage(StaticPage page)
        {
            page.StaticPageId = 4;
            page.Title = "fart";
            page.Body = "fart";

            var staticPages = GetStaticPages();
            staticPages.Add(page);

            return page;
        }

        public bool DeleteStaticPage(int staticPageId)
        {
            var pages = GetStaticPages();
            var toRemove = GetStaticPageById(staticPageId);

            pages.Remove(toRemove);
            return true;
        }

        public StaticPage GetStaticPageById(int staticPageId)
        {
            var page = new StaticPage();
            var pages = GetStaticPages();

            page = pages.Where(p => p.StaticPageId == page.StaticPageId).SingleOrDefault();

            return page;
        }

        public List<StaticPage> GetStaticPages()
        {
            return pages;
        }

        public StaticPage UpdateStaticPage(StaticPage page)
        {
            var oldPage = GetStaticPageById(page.StaticPageId);
            var newPage = new StaticPage();

            newPage.StaticPageId = oldPage.StaticPageId;
            newPage.Title = oldPage.Title;
            newPage.Body = oldPage.Body;

            return newPage;
        }
    }
}
