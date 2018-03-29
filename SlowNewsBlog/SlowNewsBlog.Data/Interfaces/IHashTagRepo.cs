﻿using SlowNewsBlog.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlowNewsBlog.Data.Interfaces
{
    public interface IHashTagRepo
    {
        HashTag GetHashTag(int id);
        void RemoveHashTag(int id);
        HashTag AddHashTag(HashTag hashtag);
        List<HashTag> GetAllHashtags();
        List<HashTag> GetApprovedHashtags();
        List<HashTag> GetUnapprovedHashtags();
        HashTag EditHashTag(HashTag hash);
        List<HashTag> GetHashTagsForBlog(int blogPostId);
    }
}
