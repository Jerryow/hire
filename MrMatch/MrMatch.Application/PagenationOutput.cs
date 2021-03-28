using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application
{
    public class PagenationOutput<TEntity>
    {
        public int PageIndex { get; set; }

        public int PageCount { get; set; }

        public int Total { get; set; }

        public List<TEntity> DataList { get; set; }

    }
}
