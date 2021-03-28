using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application
{
    public class PagenationInput
    {
        private PagenationInput() { }
        public PagenationInput(int pageIndex, int pageSize, string keywords, bool isAsc)
        {
            GetParas(pageIndex, pageSize, keywords, isAsc);
        }
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public string KeyWords { get; set; }

        public bool IsAsc { get; set; }
        private void GetParas(int pageIndex, int pageSize, string keywords, bool isAsc)
        {
            if (pageIndex == 0 && pageSize == 0)
            {
                this.PageIndex = 1;
                this.PageSize = 10;
            }
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.KeyWords = keywords;
            this.IsAsc = isAsc;
        }
    }
}
