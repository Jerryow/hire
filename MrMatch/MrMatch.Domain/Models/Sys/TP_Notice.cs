namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// ��Ϣ�������ͱ�
    /// </summary>
    public partial class TP_Notice : Entity
    {
        /// <summary>
        /// ��Ϣ����
        /// </summary>
        [StringLength(100)]
        public string MessageSubject { get; set; }
        /// <summary>
        /// ��Ϣ����
        /// </summary>
        [StringLength(3000)]
        public string MessageContent { get; set; }
        /// <summary>
        /// ��Ϣ���� 1:�����û�  2:ָ���û�
        /// </summary>
        public int MessageType { get; set; }
        /// <summary>
        /// ������,0�����û�/������û�ID
        /// </summary>
        [StringLength(3000)]
        public string UserIDs { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        [StringLength(3000)]
        public string Remark { get; set; }
        /// <summary>
        /// ���ն�,1:B�� 2:C��
        /// </summary>
        public int SendClient { get; set; }
        /// <summary>
        /// �Ƿ���
        /// </summary>
        public bool IsFinish { get; set; }
    }
}
