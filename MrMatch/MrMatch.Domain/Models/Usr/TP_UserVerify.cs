namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// ���ɺ�ѡ���ֻ���֤��
    /// </summary>
    public partial class TP_UserVerify : Entity
    {
        /// <summary>
        /// �ֻ���
        /// </summary>
        [Required]
        [StringLength(100)]
        public string SendTo { get; set; }
        /// <summary>
        /// ��֤��
        /// </summary>
        [Required]
        [StringLength(20)]
        public string VerifyCode { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime ExpiredTime { get; set; }
        /// <summary>
        /// �Ƿ�ʹ��
        /// </summary>
        public bool IsUsed { get; set; }
    }
}
