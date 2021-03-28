namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// �û�ָ����¼��
    /// </summary>
    public partial class TP_UserGuidance : Entity
    {
        /// <summary>
        /// �û�ID
        /// </summary>
        public long UserID { get; set; }
        /// <summary>
        /// ָ������ 1��½ 2������� 3�������100 4������� 5���� 6Ǳˮ
        /// </summary>
        public int GuidanceType { get; set; }
    }
}
