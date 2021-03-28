using MrMatch.Common.AttributeHelper;
using MrMatch.Domain.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.Company.Inp
{
    public class AddOrUpdateContractInp : EntityDto
    {
        /// <summary>
        /// 企业ID
        /// </summary>
        [Validate("long", "企业id不能为空")]
        public long CompanyID { get; set; }
        /// <summary>
        /// 签约日期
        /// </summary>
        public DateTime ContractDate { get; set; }
        /// <summary>
        /// 签约生效日
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 签约截至日
        /// </summary>
        public DateTime ExpireDate { get; set; }
        /// <summary>
        /// 广告位数量
        /// </summary>
        [Validate("int", "广告位数量不能为空")]
        public int AdvertiseCount { get; set; }
        /// <summary>
        /// 每月可邀请数量
        /// </summary>
        [Validate("int", "每月可邀请数量不能为空")]
        public int InviteCount { get; set; }
        /// <summary>
        /// 每月邀请累加数量
        /// </summary>
        [Validate("int", "每月邀请累加数量不能为空")]
        public int AccumulateCount { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool OnEnabled { get; set; }
    }
}
