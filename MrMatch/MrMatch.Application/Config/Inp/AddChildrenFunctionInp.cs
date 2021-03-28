using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.Config.Inp
{
    public class AddChildrenFunctionInp
    {

        /// <summary>
        /// 父ID
        /// </summary>
        public long ParentID { get; set; }
        /// <summary>
        /// 职能名称
        /// </summary>
        public string FunctionName { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool OnEnabled { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int SortNum { get; set; }
    }
}
