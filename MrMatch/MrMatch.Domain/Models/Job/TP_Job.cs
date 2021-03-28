namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// ְλ����
    /// </summary>
    public partial class TP_Job : Entity
    {
        /// <summary>
        /// ���ڳ���ID
        /// </summary>
        public long DistrictID { get; set; }
        /// <summary>
        /// ְ��ID
        /// </summary>
        public long FunctionID { get; set; }
        /// <summary>
        /// ��½�˻���ҵID
        /// </summary>
        public long BasicCompanyID { get; set; }
        /// <summary>
        /// ��ҵ��ԱID
        /// </summary>
        public long AccountID { get; set; }
        /// <summary>
        /// ְλ������ҵID
        /// </summary>
        public long JobCompanyID { get; set; }
        /// <summary>
        /// ְλ����
        /// </summary>
        [StringLength(50)]
        public string JobName { get; set; }
        /// <summary>
        /// �㱨����
        /// </summary>
        [StringLength(30)]
        public string Reporter { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        public int SubordinateCount { get; set; }
        /// <summary>
        /// ˰ǰ��н����
        /// </summary>
        public decimal MinAnnualSalary { get; set; }
        /// <summary>
        /// ˰ǰ��н����
        /// </summary>
        public decimal MaxAnnualSalary { get; set; }

        /// <summary>
        /// н���Ƿ񹫿�
        /// </summary>
        public bool SalaryOpen { get; set; }
        /// <summary>
        /// н�ʹ���
        /// </summary>
        [StringLength(30)]
        public string SalaryComposition { get; set; }
        /// <summary>
        /// н�ʹ���-����
        /// </summary>
        [StringLength(50)]
        public string SalaryCompositionExtra { get; set; }
        /// <summary>
        /// �籣����
        /// </summary>
        [StringLength(30)]
        public string SocialSecurity { get; set; }
        /// <summary>
        /// �籣����-����
        /// </summary>
        [StringLength(50)]
        public string SocialSecurityExtra { get; set; }
        /// <summary>
        /// ��ס����
        /// </summary>
        [StringLength(30)]
        public string LiveWelfare { get; set; }
        /// <summary>
        /// ��ס����-����
        /// </summary>
        [StringLength(50)]
        public string LiveWelfareExtra { get; set; }
        /// <summary>
        /// ��ٸ���
        /// </summary>
        [StringLength(30)]
        public string AnnualLeave { get; set; }
        /// <summary>
        /// ��ٸ���-����
        /// </summary>
        [StringLength(50)]
        public string AnnualLeaveExtra { get; set; }
        /// <summary>
        /// ͨѶ����
        /// </summary>
        [StringLength(30)]
        public string Subsidy { get; set; }
        /// <summary>
        /// ͨѶ����-����
        /// </summary>
        [StringLength(50)]
        public string SubsidyExtra { get; set; }
        /// <summary>
        /// ����Ҫ��
        /// </summary>
        [StringLength(30)]
        public string AgeRequirements { get; set; }
        /// <summary>
        /// רҵҪ��
        /// </summary>
        [StringLength(30)]
        public string MajorRequirements { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        public int WorkYears { get; set; }
        /// <summary>
        /// ѧ��
        /// </summary>
        public int Degree { get; set; }
        /// <summary>
        /// �Ƿ�ͳ��ȫ����
        /// </summary>
        public bool FullTime { get; set; }
        /// <summary>
        /// ����Ҫ��
        /// </summary>
        [StringLength(200)]
        public string Language { get; set; }
        /// <summary>
        /// ����Ҫ��-����
        /// </summary>
        [StringLength(200)]
        public string LanguageExtra { get; set; }
        /// <summary>
        /// ����Ҫ��
        /// </summary>
        [StringLength(500)]
        public string Skills { get; set; }
        /// <summary>
        /// ����Ҫ��-����
        /// </summary>
        [StringLength(500)]
        public string SkillsExtra { get; set; }
        /// <summary>
        /// ְλ����
        /// </summary>
        [StringLength(3000)]
        public string JobDescription { get; set; }
        /// <summary>
        /// ��λҪ��
        /// </summary>
        [StringLength(3000)]
        public string JobRequirements { get; set; }
        /// <summary>
        /// ����˵��
        /// </summary>
        [StringLength(3000)]
        public string ExtraInfo { get; set; }
        /// <summary>
        /// ���¼�
        /// </summary>
        public bool ActiveStatus { get; set; }
        /// <summary>
        /// �����̶�
        /// </summary>
        public int EmergencyLevel { get; set; }
        /// <summary>
        /// �Ƿ���ͷ��λ
        /// </summary>
        public bool AgentJob { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        public int SearchCount { get; set; }
        /// <summary>
        /// С������
        /// </summary>
        [StringLength(200)]
        public string WechatMiniPic { get; set; }
        /// <summary>
        /// �Ƿ�չʾ������Ϣ
        /// </summary>
        public bool ShowPersonal { get; set; }
        /// <summary>
        /// ������Ϣչʾ��
        /// </summary>
        public string ShowItems { get; set; }
    }
}
