namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// ϵͳ�û���
    /// </summary>
    public partial class TP_SystemUser : Entity
    {
        /// <summary>
        /// ��¼��
        /// </summary>
        [Required]
        [StringLength(50)]
        public string LoginName { get; set; }
        /// <summary>
        /// �˻���
        /// </summary>
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }
        /// <summary>
        /// ��ϵ����
        /// </summary>
        [StringLength(100)]
        public string Email { get; set; }
        /// <summary>
        /// ��½����
        /// </summary>
        [Required]
        [StringLength(256)]
        public string Password { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        [StringLength(200)]
        public string Remark { get; set; }
        /// <summary>
        /// �Ƿ��ڲ��û�
        /// </summary>
        public bool OnInternal { get; set; }
        /// <summary>
        /// �Ƿ񼤻�
        /// </summary>
        public bool OnActive { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime ActiveTime { get; set; }
    }
}
