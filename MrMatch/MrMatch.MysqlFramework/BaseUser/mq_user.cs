namespace MrMatch.MysqlFramework
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    
    public partial class mq_user : CommunityEntity
    {

        [StringLength(255)]
        public string img { get; set; }

        [StringLength(255)]
        public string background_img { get; set; }

        [StringLength(255)]
        public string phone { get; set; }

        [StringLength(255)]
        public string nick_name { get; set; }
        [StringLength(255)]
        public string country_code { get; set; }

        public int? sex { get; set; }

        public int? user_type { get; set; }

        [StringLength(255)]
        public string token { get; set; }

        [StringLength(255)]
        public string salt { get; set; }

        [StringLength(255)]
        public string login_name { get; set; }

        [StringLength(255)]
        public string password { get; set; }

        [StringLength(255)]
        public string verification_code { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string individual_resume { get; set; }

        [StringLength(255)]
        public string position { get; set; }

        [StringLength(255)]
        public string company { get; set; }

        public DateTime? entry_time { get; set; }

        public DateTime? resignation_time { get; set; }

        [StringLength(255)]
        public string school_name { get; set; }

        [StringLength(255)]
        public string major_name { get; set; }

        [StringLength(255)]
        public string education { get; set; }

        public DateTime? enrollment_time { get; set; }

        public DateTime? graduation_time { get; set; }

        public DateTime? create_time { get; set; }

        public DateTime? update_time { get; set; }

        public int? create_by { get; set; }

        public long? update_by { get; set; }

        [Required]
        [StringLength(255)]
        public string is_delete { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string pedagogical_experience { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string work_experience { get; set; }

        [StringLength(255)]
        public string real_name { get; set; }

        [StringLength(255)]
        public string papers_type { get; set; }

        [StringLength(255)]
        public string papers_number { get; set; }

        [StringLength(255)]
        public string papers_img { get; set; }

        [StringLength(255)]
        public string papers_img2 { get; set; }

        [StringLength(255)]
        public string is_real { get; set; }

        [StringLength(50)]
        public string device_id { get; set; }

        [StringLength(20)]
        public string device_type { get; set; }

        public sbyte? status { get; set; }

        [StringLength(255)]
        public string remark { get; set; }

        public DateTime? latest_chat_time { get; set; }
        /// <summary>
        /// �Ƿ������½
        /// </summary>
        public bool IsVerify { get; set; }
        /// <summary>
        /// ���״̬
        /// </summary>
        public int ApproveStatus { get; set; }
        /// <summary>
        /// �ϼ�״̬
        /// </summary>
        public bool ActiveStatus { get; set; }
        /// <summary>
        /// �Ƿ��м�������
        /// </summary>
        public bool ProfileSnap { get; set; }
        /// <summary>
        /// ����ְ��ID �ö��Ÿ���
        /// </summary>
        [StringLength(100)]
        public string IntentionFunctionIDs { get; set; }
        /// <summary>
        /// �������ID �ö��Ÿ���
        /// </summary>
        [StringLength(100)]
        public string LocationIDs { get; set; }
        /// <summary>
        /// ������н
        /// </summary>
        [StringLength(100)]
        public string AnnualSalary { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        [StringLength(100)]
        public string Email { get; set; }
    }
}
