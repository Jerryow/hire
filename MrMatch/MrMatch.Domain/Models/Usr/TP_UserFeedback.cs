namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// �û�������
    /// </summary>
    public partial class TP_UserFeedback : Entity
    {
        /// <summary>
        /// ��ID
        /// </summary>
        public long ParentID { get; set; }
        /// <summary>
        /// ��ѡ��ID
        /// </summary>
        public long UserID { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        [StringLength(256)]
        public string FeedbackTitle { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        [StringLength(3000)]
        public string FeedbackContent { get; set; }
        /// <summary>
        /// �ظ�����
        /// </summary>
        [StringLength(3000)]
        public string FeedbackResponse { get; set; }
        /// <summary>
        /// ״̬ 0�ѹر� 1���ظ�  2������  3�Ѵ���
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        [StringLength(3000)]
        public string Remark { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        public int From { get; set; }
    }
}
