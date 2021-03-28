namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// ������֪ͨ��Ϣ���ñ�
    /// </summary>
    public partial class TP_MessageConfig : Entity
    {
        /// <summary>
        /// ���� 10:���� 20�ʼ�
        /// </summary>
        public int ConfigType { get; set; }
        /// <summary>
        /// ��Ӧ������
        /// </summary>
        [Required]
        [StringLength(50)]
        public string ProviderName { get; set; }
        /// <summary>
        /// �ӿ�����
        /// </summary>
        [StringLength(50)]
        public string ApiName { get; set; }
        /// <summary>
        /// �ӿڱ���
        /// </summary>
        [StringLength(50)]
        public string ApiCode { get; set; }
        /// <summary>
        /// �ӿڵ�ַ
        /// </summary>
        [StringLength(200)]
        public string ApiUrl { get; set; }
        /// <summary>
        /// �˿�
        /// </summary>
        public int? Port { get; set; }
        /// <summary>
        /// �Ƿ�����ssl��Ĭ��false��
        /// </summary>
        public bool? EnableSsl { get; set; }
        /// <summary>
        /// ����ǩ��
        /// </summary>
        [StringLength(50)]
        public string SenderName { get; set; }
        /// <summary>
        /// ��¼��
        /// </summary>
        [StringLength(50)]
        public string LoginName { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        [StringLength(256)]
        public string Password { get; set; }
        /// <summary>
        /// ��֤��Կ
        /// </summary>
        [StringLength(256)]
        public string VerifyKey { get; set; }
        /// <summary>
        /// ǩ��
        /// </summary>
        [StringLength(50)]
        public string SignMark { get; set; }
        /// <summary>
        /// �Ƿ�����
        /// </summary>
        public bool IsActivated { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        public int? TimesLimit { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        [StringLength(3000)]
        public string Remark { get; set; }
    }
}
