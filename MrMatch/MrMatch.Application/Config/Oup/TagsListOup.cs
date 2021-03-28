using MrMatch.Domain.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.Config.Oup
{
    public class TagsListOup : EntityDto
    {
        /// <summary>
        /// 父ID
        /// </summary>
        public long ParentID { get; set; }
        /// <summary>
        /// 标签名称
        /// </summary>
        public string Tags { get; set; }
        /// <summary>
        /// 父标签名称
        /// </summary>
        public string ParentTag { get; set; }
    }
}
