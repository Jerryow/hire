namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// 企业签约表
    /// </summary>
    public partial class TP_CompanyContract : Entity
    {
        /// <summary>
        /// 企业ID
        /// </summary>
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
        public int AdvertiseCount { get; set; }
        /// <summary>
        /// 每月可邀请数量
        /// </summary>
        public int InviteCount { get; set; }
        /// <summary>
        /// 每月邀请累加数量
        /// </summary>
        public int AccumulateCount { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool OnEnabled { get; set; }
    }
}
