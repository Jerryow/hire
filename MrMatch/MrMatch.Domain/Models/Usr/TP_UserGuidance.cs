namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// 用户指引记录表
    /// </summary>
    public partial class TP_UserGuidance : Entity
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserID { get; set; }
        /// <summary>
        /// 指引类型 1登陆 2点击简历 3简历完成100 4点击锁定 5求发现 6潜水
        /// </summary>
        public int GuidanceType { get; set; }
    }
}
