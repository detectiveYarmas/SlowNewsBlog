using SlowNewsBlog.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlowNewsBlog.Domain.Managers
{
    public class CategoryRepoManager
    {
        private ICategory repo { get; set; }

        public CategoryRepoManager(ICategory category)
        {
            repo = category;
        }
    }
}
