using MrMatch.Domain.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.Config.Oup
{
    public class CountryOup : EntityDto
    {
        /// <summary>
        /// 国家名称
        /// </summary>
        public string CountryName { get; set; }
        /// <summary>
        /// 英文名称
        /// </summary>
        public string EngName { get; set; }
        /// <summary>
        /// 首字母
        /// </summary>
        public string Initial { get; set; }
        /// <summary>
        /// 缩写
        /// </summary>
        public string Initials { get; set; }
        /// <summary>
        /// 附加信息
        /// </summary>
        public string Extra { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int OrderNum { get; set; }
    }
}
