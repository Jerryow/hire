namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// ѧУ��
    /// </summary>
    public partial class TP_School : Entity
    {
        /// <summary>
        /// ����ID
        /// </summary>
        public long CountryID { get; set; }
        /// <summary>
        /// ����ID
        /// </summary>
        public long DistrictID { get; set; }
        /// <summary>
        /// ����ID
        /// </summary>
        public long CityID { get; set; }
        /// <summary>
        /// ѧУ����
        /// </summary>
        public int SchoolType { get; set; }
        /// <summary>
        /// ѧУ����  985.211
        /// </summary>
        [StringLength(50)]
        public string SchoolGrade { get; set; }
        /// <summary>
        /// �Ƿ���
        /// </summary>
        public bool IsPublic { get; set; }
        /// <summary>
        /// ѧУ����
        /// </summary>
        [StringLength(200)]
        public string SchoolName { get; set; }
        /// <summary>
        /// Ӣ������
        /// </summary>
        [StringLength(200)]
        public string SchoolNameEN { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        [StringLength(100)]
        public string AnotherName { get; set; }
        /// <summary>
        /// ���
        /// </summary>
        [StringLength(50)]
        public string ShortName { get; set; }
    }
}
