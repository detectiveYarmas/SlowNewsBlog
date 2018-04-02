using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SlowNewsBlog.Controllers
{
    public class BloggerController : Controller
    {
        // GET: Blogger
        public ActionResult Index()
        {
            return View();
        }
    }
}