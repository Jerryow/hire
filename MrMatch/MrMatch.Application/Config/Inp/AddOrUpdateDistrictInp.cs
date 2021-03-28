using MrMatch.Domain.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.Config.Inp
{
    public class AddOrUpdateDistrictInp : EntityDto
    {
        /// <summary>
        /// 国家ID
        /// </summary>
        public long CountryID { get; set; }
        /// <summary>
        /// 地区名称
        /// </summary>
        public string DistrictName { get; set; }
        /// <summary>
        /// 父ID
        /// </summary>
        public long ParentID { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int OrderNum { get; set; }
        /// <summary>
        /// 是否显示(做职位的可检索地区)
        /// </summary>
        public bool OnShow { get; set; }
        /// <summary>
        /// 是否热门
        /// </summary>
        public bool HotCity { get; set; }
    }
}
