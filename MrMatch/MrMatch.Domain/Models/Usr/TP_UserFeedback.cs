namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// 用户反馈表
    /// </summary>
    public partial class TP_UserFeedback : Entity
    {
        /// <summary>
        /// 父ID
        /// </summary>
        public long ParentID { get; set; }
        /// <summary>
        /// 候选人ID
        /// </summary>
        public long UserID { get; set; }
        /// <summary>
        /// 反馈标题
        /// </summary>
        [StringLength(256)]
        public string FeedbackTitle { get; set; }
        /// <summary>
        /// 反馈内容
        /// </summary>
        [StringLength(3000)]
        public string FeedbackContent { get; set; }
        /// <summary>
        /// 回复内容
        /// </summary>
        [StringLength(3000)]
        public string FeedbackResponse { get; set; }
        /// <summary>
        /// 状态 0已关闭 1待回复  2处理中  3已处理
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(3000)]
        public string Remark { get; set; }
        /// <summary>
        /// 发起者
        /// </summary>
        public int From { get; set; }
    }
}
