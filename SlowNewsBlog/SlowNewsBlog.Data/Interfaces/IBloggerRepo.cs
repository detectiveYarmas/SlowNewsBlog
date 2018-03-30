using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlowNewsBlog.Data.Interfaces
{
    public interface IBloggerRepo
    {
        List<int> GetAll();
    }
}
