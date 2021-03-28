namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// ��ҵ�û���
    /// </summary>
    public partial class TP_Account : Entity
    {
        /// <summary>
        /// ��ҵID
        /// </summary>
        public long CompanyID { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        [StringLength(100)]
        public string Email { get; set; }
        /// <summary>
        /// ��ϵ�绰
        /// </summary>
        [StringLength(20)]
        public string CellPhone { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        [StringLength(256)]
        public string Password { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        [StringLength(50)]
        public string AccountName { get; set; }
        /// <summary>
        /// ְλ
        /// </summary>
        [StringLength(50)]
        public string Position { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        [StringLength(100)]
        public string Domain { get; set; }
        /// <summary>
        /// �Ƿ����Ա
        /// </summary>
        public bool IsAdmin { get; set; }
        /// <summary>
        /// �Ƿ񼤻�
        /// </summary>
        public bool OnActive { get; set; }
        /// <summary>
        /// �ϴε�½ʱ��
        /// </summary>
        public DateTime LastLoginTime { get; set; }
        /// <summary>
        /// �ݸ���������
        /// </summary>
        public int DraftLimiteCount { get; set; }
        /// <summary>
        /// ����ģ����������
        /// </summary>
        public int TemplateLimiteCount { get; set; }
        /// <summary>
        /// �Ƿ������½
        /// </summary>
        public bool IsVerify { get; set; }
        /// <summary>
        /// ͷ��
        /// </summary>
        [StringLength(500)]
        public string AvatarUrl { get; set; }
        /// <summary>
        /// ΢�ź�
        /// </summary>
        [StringLength(300)]
        public string WechatAccount { get; set; }
        /// <summary>
        /// ΢�Ŷ�ά��
        /// </summary>
        [StringLength(500)]
        public string WechatContactUrl { get; set; }
        /// <summary>
        /// ��Ӣ����
        /// </summary>
        [StringLength(500)]
        public string LinkinUrl { get; set; }
        /// <summary>
        /// רע����
        /// </summary>
        [StringLength(500)]
        public string FocusArea { get; set; }
        /// <summary>
        /// ���˽���
        /// </summary>
        [StringLength(2000)]
        public string Introduction { get; set; }

    }
}
