﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlowNewsBlog.Models.Tables
{
    public class BlogPost
    {
        public int BlogPostId { get; set; }
        public string Blog { get; set; }
        public string Title { get; set; }
        public bool Approved { get; set; }
        public int CatagoryId { get; set; }
        public DateTime? PublishDate { get; set; }
        public DateTime DateAdded { get; set; }
        public string Id { get; set; }
        public string HeaderImage { get; set; }
        public ICollection<HashTag> BlogPostHashTags { get; set; }
        public string UserName { get; set; }
    }
}
