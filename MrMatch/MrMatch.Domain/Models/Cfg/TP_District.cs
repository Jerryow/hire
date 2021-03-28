namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// ������
    /// </summary>
    public partial class TP_District : Entity
    {
        /// <summary>
        /// ����ID
        /// </summary>
        public long CountryID { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        [StringLength(50)]
        public string DistrictName { get; set; }
        /// <summary>
        /// ��ID
        /// </summary>
        public long ParentID { get; set; }
        /// <summary>
        /// ����ĸ
        /// </summary>
        [StringLength(10)]
        public string Initial { get; set; }
        /// <summary>
        /// ��д
        /// </summary>
        [StringLength(50)]
        public string Initials { get; set; }
        /// <summary>
        /// ƴ��
        /// </summary>
        [StringLength(200)]
        public string Pinyin { get; set; }
        /// <summary>
        /// ������Ϣ
        /// </summary>
        [StringLength(50)]
        public string Extra { get; set; }
        /// <summary>
        /// ��׺
        /// </summary>
        [StringLength(50)]
        public string Suffix { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        [StringLength(50)]
        public string PostCode { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        [StringLength(50)]
        public string AreaCode { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public int OrderNum { get; set; }
        /// <summary>
        /// �Ƿ���ʾ(��ְλ�Ŀɼ�������)
        /// </summary>
        public bool OnShow { get; set; }
        /// <summary>
        /// �Ƿ����ų���
        /// </summary>
        public bool HotCity { get; set; }
    }
}
