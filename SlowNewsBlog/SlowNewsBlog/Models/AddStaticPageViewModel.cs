﻿using SlowNewsBlog.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SlowNewsBlog.Models
{
    public class AddStaticPageViewModel
    {
        [AllowHtml]
        public StaticPage StaticPage { get; set; }
    }
}