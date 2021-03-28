namespace MrMatch.MysqlFramework
{
    using System.Data.Entity;
    using MrMatch.Domain.Models;
    using MrMatch.MysqlFramework.BaseContext;

    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public partial class MrMatchDbContext : DbContext,IDbContext
    {
        public MrMatchDbContext()
            : base("MrMatchDbContext")
        {
            //�Ӳ��������ݿ�
            //Database.SetInitializer<MrMatchDbContext>(null);
            //���ݿⲻ����ʱ���´������ݿ�
            Database.SetInitializer<MrMatchDbContext>(new CreateDatabaseIfNotExists<MrMatchDbContext>());
            //ÿ������Ӧ�ó���ʱ�������ݿ�
            //Database.SetInitializer<MrMatchDbContext>(new DropCreateDatabaseAlways<MrMatchDbContext>());
            //ģ�͸���ʱ���´������ݿ�
            //Database.SetInitializer<MrMatchDbContext>(new DropCreateDatabaseIfModelChanges<MrMatchDbContext>());
        }

        #region System_ϵͳ��
        public virtual DbSet<TP_SystemUser> TP_SystemUser { get; set; }
        public virtual DbSet<TP_MessageConfig> TP_MessageConfig { get; set; }
        public virtual DbSet<TP_MessageTemplate> TP_MessageTemplate { get; set; }
        public virtual DbSet<TP_SiteConfig> TP_SiteConfig { get; set; }
        public virtual DbSet<TP_Notice> TP_Notice { get; set; }
        public virtual DbSet<TP_NoticeUser> TP_NoticeUser { get; set; }
        #endregion


        #region Config_�����������ñ�
        public virtual DbSet<TP_Country> TP_Country { get; set; }
        public virtual DbSet<TP_District> TP_District { get; set; }
        public virtual DbSet<TP_Function> TP_Function { get; set; }
        public virtual DbSet<TP_School> TP_School { get; set; }
        public virtual DbSet<TP_FunctionSkillsRelation> TP_FunctionSkillsRelation { get; set; }
        public virtual DbSet<TP_Skills> TP_Skills { get; set; }
        public virtual DbSet<TP_Tags> TP_Tags { get; set; }
        #endregion


        #region Company_��ҵ���ݱ�
        public virtual DbSet<TP_Account> TP_Account { get; set; }
        public virtual DbSet<TP_AccountFolder> TP_AccountFolder { get; set; }
        public virtual DbSet<TP_AccountFollow> TP_AccountFollow { get; set; }
        public virtual DbSet<TP_AccountFollowFolder> TP_AccountFollowFolder { get; set; }
        public virtual DbSet<TP_AccountInfoSearchLog> TP_AccountInfoSearchLog { get; set; }
        public virtual DbSet<TP_AccountInvitation> TP_AccountInvitation { get; set; }
        public virtual DbSet<TP_AccountNotice> TP_AccountNotice { get; set; }
        //public virtual DbSet<TP_AccountVerify> TP_AccountVerify { get; set; }
        public virtual DbSet<TP_AgentCompany> TP_AgentCompany { get; set; }
        public virtual DbSet<TP_Company> TP_Company { get; set; }
        public virtual DbSet<TP_CompanyContract> TP_CompanyContract { get; set; }
        public virtual DbSet<TP_LetterTemplate> TP_LetterTemplate { get; set; }
        public virtual DbSet<TP_ProfileView> TP_ProfileView { get; set; }
        #endregion


        #region Job_ְλ���ݱ�
        public virtual DbSet<TP_Job> TP_Job { get; set; }
        public virtual DbSet<TP_JobApply> TP_JobApply { get; set; }
        public virtual DbSet<TP_JobComment> TP_JobComment { get; set; }
        public virtual DbSet<TP_JobDraft> TP_JobDraft { get; set; }
        public virtual DbSet<TP_JobInterview> TP_JobInterview { get; set; }
        public virtual DbSet<TP_JobResponse> TP_JobResponse { get; set; }
        public virtual DbSet<TP_JobView> TP_JobView { get; set; }
        #endregion


        #region Log_����/�ʼ����ͼ�¼��
        //public virtual DbSet<TP_MailMessageLog> TP_MailMesageLog { get; set; }
        //public virtual DbSet<TP_PhoneMessageLog> TP_PhoneMessageLog { get; set; }
        #endregion


        #region User
        #region CommonUser
        public virtual DbSet<mq_user> mq_user { get; set; }
        #endregion
        public virtual DbSet<TP_UserAvoid> TP_UserAvoid { get; set; }
        public virtual DbSet<TP_UserEducation> TP_UserEducation { get; set; }
        public virtual DbSet<TP_UserFeedback> TP_UserFeedback { get; set; }
        public virtual DbSet<TP_UserGuidance> TP_UserGuidance { get; set; }
        public virtual DbSet<TP_UserJobFollow> TP_UserJobFollow { get; set; }
        public virtual DbSet<TP_UserJobIntention> TP_UserJobIntention { get; set; }
        public virtual DbSet<TP_UserMessage> TP_UserMessage { get; set; }
        public virtual DbSet<TP_UserMessageRead> TP_UserMessageRead { get; set; }
        public virtual DbSet<TP_UserProfile> TP_UserProfile { get; set; }
        public virtual DbSet<TP_UserReport> TP_UserReport { get; set; }
        public virtual DbSet<TP_UserTags> TP_UserTags { get; set; }
        //public virtual DbSet<TP_UserVerify> TP_UserVerify { get; set; }
        public virtual DbSet<TP_UserWorkExperience> TP_UserWorkExperience { get; set; }
        #endregion


        #region Wechat_΢�����

        public virtual DbSet<TP_AccessToken> TP_AccessToken { get; set; }
        public virtual DbSet<TP_WechatMessage> TP_WechatMessage { get; set; }
        public virtual DbSet<TP_WechatUser> TP_WechatUser { get; set; }
        #endregion


        #region Snapshot_��������
        public virtual DbSet<TP_EducationSnap> TP_EducationSnap { get; set; }
        public virtual DbSet<TP_WorkExperienceSnap> TP_WorkExperienceSnap { get; set; }
        public virtual DbSet<TP_ProfileSnap> TP_ProfileSnap { get; set; }
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<mq_user>()
                .Property(e => e.img)
                .IsUnicode(false);

            modelBuilder.Entity<mq_user>()
                .Property(e => e.background_img)
                .IsUnicode(false);

            modelBuilder.Entity<mq_user>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<mq_user>()
                .Property(e => e.nick_name)
                .IsUnicode(false);

            modelBuilder.Entity<mq_user>()
                .Property(e => e.token)
                .IsUnicode(false);

            modelBuilder.Entity<mq_user>()
                .Property(e => e.salt)
                .IsUnicode(false);

            modelBuilder.Entity<mq_user>()
                .Property(e => e.login_name)
                .IsUnicode(false);

            modelBuilder.Entity<mq_user>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<mq_user>()
                .Property(e => e.verification_code)
                .IsUnicode(false);

            modelBuilder.Entity<mq_user>()
                .Property(e => e.individual_resume)
                .IsUnicode(false);

            modelBuilder.Entity<mq_user>()
                .Property(e => e.position)
                .IsUnicode(false);

            modelBuilder.Entity<mq_user>()
                .Property(e => e.company)
                .IsUnicode(false);

            modelBuilder.Entity<mq_user>()
                .Property(e => e.school_name)
                .IsUnicode(false);

            modelBuilder.Entity<mq_user>()
                .Property(e => e.major_name)
                .IsUnicode(false);

            modelBuilder.Entity<mq_user>()
                .Property(e => e.education)
                .IsUnicode(false);

            modelBuilder.Entity<mq_user>()
                .Property(e => e.is_delete)
                .IsUnicode(false);

            modelBuilder.Entity<mq_user>()
                .Property(e => e.pedagogical_experience)
                .IsUnicode(false);

            modelBuilder.Entity<mq_user>()
                .Property(e => e.work_experience)
                .IsUnicode(false);

            modelBuilder.Entity<mq_user>()
                .Property(e => e.real_name)
                .IsUnicode(false);

            modelBuilder.Entity<mq_user>()
                .Property(e => e.papers_type)
                .IsUnicode(false);

            modelBuilder.Entity<mq_user>()
                .Property(e => e.papers_number)
                .IsUnicode(false);

            modelBuilder.Entity<mq_user>()
                .Property(e => e.papers_img)
                .IsUnicode(false);

            modelBuilder.Entity<mq_user>()
                .Property(e => e.papers_img2)
                .IsUnicode(false);

            modelBuilder.Entity<mq_user>()
                .Property(e => e.is_real)
                .IsUnicode(false);

            modelBuilder.Entity<mq_user>()
                .Property(e => e.device_id)
                .IsUnicode(false);

            modelBuilder.Entity<mq_user>()
                .Property(e => e.device_type)
                .IsUnicode(false);

            modelBuilder.Entity<mq_user>()
                .Property(e => e.remark)
                .IsUnicode(false);
        }
    }
}
