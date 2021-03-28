namespace MrMatch.MysqlFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.mq_user",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        img = c.String(maxLength: 255, unicode: false),
                        background_img = c.String(maxLength: 255, unicode: false),
                        phone = c.String(maxLength: 255, unicode: false),
                        nick_name = c.String(maxLength: 255, unicode: false),
                        country_code = c.String(maxLength: 255, storeType: "nvarchar"),
                        sex = c.Int(),
                        user_type = c.Int(),
                        token = c.String(maxLength: 255, unicode: false),
                        salt = c.String(maxLength: 255, unicode: false),
                        login_name = c.String(maxLength: 255, unicode: false),
                        password = c.String(maxLength: 255, unicode: false),
                        verification_code = c.String(maxLength: 255, unicode: false),
                        individual_resume = c.String(unicode: false, storeType: "text"),
                        position = c.String(maxLength: 255, unicode: false),
                        company = c.String(maxLength: 255, unicode: false),
                        entry_time = c.DateTime(precision: 0),
                        resignation_time = c.DateTime(precision: 0),
                        school_name = c.String(maxLength: 255, unicode: false),
                        major_name = c.String(maxLength: 255, unicode: false),
                        education = c.String(maxLength: 255, unicode: false),
                        enrollment_time = c.DateTime(precision: 0),
                        graduation_time = c.DateTime(precision: 0),
                        create_time = c.DateTime(precision: 0),
                        update_time = c.DateTime(precision: 0),
                        create_by = c.Int(),
                        update_by = c.Long(),
                        is_delete = c.String(nullable: false, maxLength: 255, unicode: false),
                        pedagogical_experience = c.String(unicode: false, storeType: "text"),
                        work_experience = c.String(unicode: false, storeType: "text"),
                        real_name = c.String(maxLength: 255, unicode: false),
                        papers_type = c.String(maxLength: 255, unicode: false),
                        papers_number = c.String(maxLength: 255, unicode: false),
                        papers_img = c.String(maxLength: 255, unicode: false),
                        papers_img2 = c.String(maxLength: 255, unicode: false),
                        is_real = c.String(maxLength: 255, unicode: false),
                        device_id = c.String(maxLength: 50, unicode: false),
                        device_type = c.String(maxLength: 20, unicode: false),
                        status = c.Byte(),
                        remark = c.String(maxLength: 255, unicode: false),
                        latest_chat_time = c.DateTime(precision: 0),
                        IsVerify = c.Boolean(nullable: false),
                        ApproveStatus = c.Int(nullable: false),
                        ActiveStatus = c.Boolean(nullable: false),
                        ProfileSnap = c.Boolean(nullable: false),
                        IntentionFunctionIDs = c.String(maxLength: 100, storeType: "nvarchar"),
                        LocationIDs = c.String(maxLength: 100, storeType: "nvarchar"),
                        AnnualSalary = c.String(maxLength: 100, storeType: "nvarchar"),
                        Email = c.String(maxLength: 100, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.TP_AccessToken",
                c => new
                    {
                        PKID = c.Long(nullable: false, identity: true),
                        AppID = c.String(maxLength: 128, storeType: "nvarchar"),
                        Secret = c.String(maxLength: 256, storeType: "nvarchar"),
                        AccessToken = c.String(maxLength: 500, storeType: "nvarchar"),
                        ExpiresIn = c.Int(),
                        ExpiresTime = c.DateTime(precision: 0),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        LastModifiedTime = c.DateTime(nullable: false, precision: 0),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PKID);
            
            CreateTable(
                "dbo.TP_Account",
                c => new
                    {
                        PKID = c.Long(nullable: false, identity: true),
                        CompanyID = c.Long(nullable: false),
                        Email = c.String(maxLength: 100, storeType: "nvarchar"),
                        CellPhone = c.String(maxLength: 20, storeType: "nvarchar"),
                        Password = c.String(maxLength: 256, storeType: "nvarchar"),
                        AccountName = c.String(maxLength: 50, storeType: "nvarchar"),
                        Position = c.String(maxLength: 50, storeType: "nvarchar"),
                        Domain = c.String(maxLength: 100, storeType: "nvarchar"),
                        IsAdmin = c.Boolean(nullable: false),
                        OnActive = c.Boolean(nullable: false),
                        LastLoginTime = c.DateTime(nullable: false, precision: 0),
                        DraftLimiteCount = c.Int(nullable: false),
                        TemplateLimiteCount = c.Int(nullable: false),
                        IsVerify = c.Boolean(nullable: false),
                        AvatarUrl = c.String(maxLength: 500, storeType: "nvarchar"),
                        WechatAccount = c.String(maxLength: 300, storeType: "nvarchar"),
                        WechatContactUrl = c.String(maxLength: 500, storeType: "nvarchar"),
                        LinkinUrl = c.String(maxLength: 500, storeType: "nvarchar"),
                        FocusArea = c.String(maxLength: 500, storeType: "nvarchar"),
                        Introduction = c.String(maxLength: 2000, storeType: "nvarchar"),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        LastModifiedTime = c.DateTime(nullable: false, precision: 0),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PKID);
            
            CreateTable(
                "dbo.TP_AccountFolder",
                c => new
                    {
                        PKID = c.Long(nullable: false, identity: true),
                        AccountID = c.Long(nullable: false),
                        FolderName = c.String(maxLength: 100, storeType: "nvarchar"),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        LastModifiedTime = c.DateTime(nullable: false, precision: 0),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PKID);
            
            CreateTable(
                "dbo.TP_AccountFollow",
                c => new
                    {
                        PKID = c.Long(nullable: false, identity: true),
                        CompanyID = c.Long(nullable: false),
                        UserID = c.Long(nullable: false),
                        AccountID = c.Long(nullable: false),
                        FollowTime = c.DateTime(nullable: false, precision: 0),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        LastModifiedTime = c.DateTime(nullable: false, precision: 0),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PKID);
            
            CreateTable(
                "dbo.TP_AccountFollowFolder",
                c => new
                    {
                        PKID = c.Long(nullable: false, identity: true),
                        AccountID = c.Long(nullable: false),
                        UserID = c.Long(nullable: false),
                        FolderID = c.Long(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        LastModifiedTime = c.DateTime(nullable: false, precision: 0),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PKID);
            
            CreateTable(
                "dbo.TP_AccountInfoSearchLog",
                c => new
                    {
                        PKID = c.Long(nullable: false, identity: true),
                        CompanyID = c.Long(nullable: false),
                        AccountID = c.Long(nullable: false),
                        SearchUrl = c.String(maxLength: 200, storeType: "nvarchar"),
                        SearchName = c.String(maxLength: 200, storeType: "nvarchar"),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        LastModifiedTime = c.DateTime(nullable: false, precision: 0),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PKID);
            
            CreateTable(
                "dbo.TP_AccountInvitation",
                c => new
                    {
                        PKID = c.Long(nullable: false, identity: true),
                        CompanyID = c.Long(nullable: false),
                        Email = c.String(maxLength: 100, storeType: "nvarchar"),
                        Domain = c.String(maxLength: 100, storeType: "nvarchar"),
                        SenderID = c.Long(nullable: false),
                        IsRegisted = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        LastModifiedTime = c.DateTime(nullable: false, precision: 0),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PKID);
            
            CreateTable(
                "dbo.TP_AccountNotice",
                c => new
                    {
                        PKID = c.Long(nullable: false, identity: true),
                        AccountID = c.Long(nullable: false),
                        MessageID = c.Long(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        LastModifiedTime = c.DateTime(nullable: false, precision: 0),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PKID);
            
            CreateTable(
                "dbo.TP_AgentCompany",
                c => new
                    {
                        PKID = c.Long(nullable: false, identity: true),
                        BasicCompanyID = c.Long(nullable: false),
                        AccountID = c.Long(nullable: false),
                        CompanyName = c.String(maxLength: 100, storeType: "nvarchar"),
                        ShortName = c.String(maxLength: 50, storeType: "nvarchar"),
                        City = c.String(maxLength: 100, storeType: "nvarchar"),
                        CompnayType = c.Int(nullable: false),
                        EmployeeNum = c.Int(nullable: false),
                        Summary = c.String(maxLength: 1000, storeType: "nvarchar"),
                        Introduce = c.String(maxLength: 3000, storeType: "nvarchar"),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        LastModifiedTime = c.DateTime(nullable: false, precision: 0),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PKID);
            
            CreateTable(
                "dbo.TP_Company",
                c => new
                    {
                        PKID = c.Long(nullable: false, identity: true),
                        CompanyName = c.String(maxLength: 100, storeType: "nvarchar"),
                        ShortName = c.String(maxLength: 50, storeType: "nvarchar"),
                        CompnayType = c.Int(nullable: false),
                        FinancingStatus = c.Int(nullable: false),
                        Website = c.String(maxLength: 200, storeType: "nvarchar"),
                        EmployeeNum = c.Int(nullable: false),
                        RegisteredCapital = c.Int(nullable: false),
                        Summary = c.String(maxLength: 200, storeType: "nvarchar"),
                        Location = c.String(maxLength: 200, storeType: "nvarchar"),
                        Address = c.String(maxLength: 500, storeType: "nvarchar"),
                        Introduce = c.String(maxLength: 1000, storeType: "nvarchar"),
                        CheckedStatus = c.Int(nullable: false),
                        AgentOrNot = c.Boolean(nullable: false),
                        LicenseImageUrl = c.String(maxLength: 200, storeType: "nvarchar"),
                        LicenseAuthStatus = c.Int(nullable: false),
                        LogoImageUrl = c.String(maxLength: 200, storeType: "nvarchar"),
                        ContactName = c.String(maxLength: 50, storeType: "nvarchar"),
                        CellPhone = c.String(maxLength: 50, storeType: "nvarchar"),
                        Email = c.String(maxLength: 100, storeType: "nvarchar"),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        LastModifiedTime = c.DateTime(nullable: false, precision: 0),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PKID);
            
            CreateTable(
                "dbo.TP_CompanyContract",
                c => new
                    {
                        PKID = c.Long(nullable: false, identity: true),
                        CompanyID = c.Long(nullable: false),
                        ContractDate = c.DateTime(nullable: false, precision: 0),
                        StartDate = c.DateTime(nullable: false, precision: 0),
                        ExpireDate = c.DateTime(nullable: false, precision: 0),
                        AdvertiseCount = c.Int(nullable: false),
                        InviteCount = c.Int(nullable: false),
                        AccumulateCount = c.Int(nullable: false),
                        OnEnabled = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        LastModifiedTime = c.DateTime(nullable: false, precision: 0),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PKID);
            
            CreateTable(
                "dbo.TP_Country",
                c => new
                    {
                        PKID = c.Long(nullable: false, identity: true),
                        CountryName = c.String(maxLength: 50, storeType: "nvarchar"),
                        EngName = c.String(maxLength: 100, storeType: "nvarchar"),
                        Initial = c.String(maxLength: 5, storeType: "nvarchar"),
                        Initials = c.String(maxLength: 10, storeType: "nvarchar"),
                        Extra = c.String(maxLength: 50, storeType: "nvarchar"),
                        OrderNum = c.Int(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        LastModifiedTime = c.DateTime(nullable: false, precision: 0),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PKID);
            
            CreateTable(
                "dbo.TP_District",
                c => new
                    {
                        PKID = c.Long(nullable: false, identity: true),
                        CountryID = c.Long(nullable: false),
                        DistrictName = c.String(maxLength: 50, storeType: "nvarchar"),
                        ParentID = c.Long(nullable: false),
                        Initial = c.String(maxLength: 10, storeType: "nvarchar"),
                        Initials = c.String(maxLength: 50, storeType: "nvarchar"),
                        Pinyin = c.String(maxLength: 200, storeType: "nvarchar"),
                        Extra = c.String(maxLength: 50, storeType: "nvarchar"),
                        Suffix = c.String(maxLength: 50, storeType: "nvarchar"),
                        PostCode = c.String(maxLength: 50, storeType: "nvarchar"),
                        AreaCode = c.String(maxLength: 50, storeType: "nvarchar"),
                        OrderNum = c.Int(nullable: false),
                        OnShow = c.Boolean(nullable: false),
                        HotCity = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        LastModifiedTime = c.DateTime(nullable: false, precision: 0),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PKID);
            
            CreateTable(
                "dbo.TP_EducationSnap",
                c => new
                    {
                        PKID = c.Long(nullable: false, identity: true),
                        UserID = c.Long(nullable: false),
                        StartDate = c.DateTime(nullable: false, precision: 0),
                        ExpirationDate = c.String(maxLength: 100, storeType: "nvarchar"),
                        SchoolName = c.String(maxLength: 100, storeType: "nvarchar"),
                        MajorSubject = c.String(maxLength: 100, storeType: "nvarchar"),
                        Degree = c.Int(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        LastModifiedTime = c.DateTime(nullable: false, precision: 0),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PKID);
            
            CreateTable(
                "dbo.TP_Function",
                c => new
                    {
                        PKID = c.Long(nullable: false, identity: true),
                        ParentID = c.Long(nullable: false),
                        FunctionName = c.String(maxLength: 50, storeType: "nvarchar"),
                        OnEnabled = c.Boolean(nullable: false),
                        SortNum = c.Int(nullable: false),
                        IsPopular = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        LastModifiedTime = c.DateTime(nullable: false, precision: 0),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PKID);
            
            CreateTable(
                "dbo.TP_FunctionSkillsRelation",
                c => new
                    {
                        PKID = c.Long(nullable: false, identity: true),
                        SkillID = c.Long(nullable: false),
                        FunctionID = c.Long(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        LastModifiedTime = c.DateTime(nullable: false, precision: 0),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PKID);
            
            CreateTable(
                "dbo.TP_Job",
                c => new
                    {
                        PKID = c.Long(nullable: false, identity: true),
                        DistrictID = c.Long(nullable: false),
                        FunctionID = c.Long(nullable: false),
                        BasicCompanyID = c.Long(nullable: false),
                        AccountID = c.Long(nullable: false),
                        JobCompanyID = c.Long(nullable: false),
                        JobName = c.String(maxLength: 50, storeType: "nvarchar"),
                        Reporter = c.String(maxLength: 30, storeType: "nvarchar"),
                        SubordinateCount = c.Int(nullable: false),
                        MinAnnualSalary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MaxAnnualSalary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SalaryOpen = c.Boolean(nullable: false),
                        SalaryComposition = c.String(maxLength: 30, storeType: "nvarchar"),
                        SalaryCompositionExtra = c.String(maxLength: 50, storeType: "nvarchar"),
                        SocialSecurity = c.String(maxLength: 30, storeType: "nvarchar"),
                        SocialSecurityExtra = c.String(maxLength: 50, storeType: "nvarchar"),
                        LiveWelfare = c.String(maxLength: 30, storeType: "nvarchar"),
                        LiveWelfareExtra = c.String(maxLength: 50, storeType: "nvarchar"),
                        AnnualLeave = c.String(maxLength: 30, storeType: "nvarchar"),
                        AnnualLeaveExtra = c.String(maxLength: 50, storeType: "nvarchar"),
                        Subsidy = c.String(maxLength: 30, storeType: "nvarchar"),
                        SubsidyExtra = c.String(maxLength: 50, storeType: "nvarchar"),
                        AgeRequirements = c.String(maxLength: 30, storeType: "nvarchar"),
                        MajorRequirements = c.String(maxLength: 30, storeType: "nvarchar"),
                        WorkYears = c.Int(nullable: false),
                        Degree = c.Int(nullable: false),
                        FullTime = c.Boolean(nullable: false),
                        Language = c.String(maxLength: 200, storeType: "nvarchar"),
                        LanguageExtra = c.String(maxLength: 200, storeType: "nvarchar"),
                        Skills = c.String(maxLength: 500, storeType: "nvarchar"),
                        SkillsExtra = c.String(maxLength: 500, storeType: "nvarchar"),
                        JobDescription = c.String(maxLength: 3000, storeType: "nvarchar"),
                        JobRequirements = c.String(maxLength: 3000, storeType: "nvarchar"),
                        ExtraInfo = c.String(maxLength: 3000, storeType: "nvarchar"),
                        ActiveStatus = c.Boolean(nullable: false),
                        EmergencyLevel = c.Int(nullable: false),
                        AgentJob = c.Boolean(nullable: false),
                        SearchCount = c.Int(nullable: false),
                        WechatMiniPic = c.String(maxLength: 200, storeType: "nvarchar"),
                        ShowPersonal = c.Boolean(nullable: false),
                        ShowItems = c.String(unicode: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        LastModifiedTime = c.DateTime(nullable: false, precision: 0),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PKID);
            
            CreateTable(
                "dbo.TP_JobApply",
                c => new
                    {
                        PKID = c.Long(nullable: false, identity: true),
                        UserID = c.Long(nullable: false),
                        ApplyStatus = c.Int(nullable: false),
                        ExpirationDate = c.DateTime(nullable: false, precision: 0),
                        AccountID = c.Long(nullable: false),
                        JobID = c.Long(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        LastModifiedTime = c.DateTime(nullable: false, precision: 0),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PKID);
            
            CreateTable(
                "dbo.TP_JobComment",
                c => new
                    {
                        PKID = c.Long(nullable: false, identity: true),
                        UserID = c.Long(nullable: false),
                        CompanyID = c.Long(nullable: false),
                        InterviewID = c.Long(nullable: false),
                        AccountID = c.Long(nullable: false),
                        IsOpen = c.Boolean(nullable: false),
                        Comment = c.String(maxLength: 3000, storeType: "nvarchar"),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        LastModifiedTime = c.DateTime(nullable: false, precision: 0),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PKID);
            
            CreateTable(
                "dbo.TP_JobDraft",
                c => new
                    {
                        PKID = c.Long(nullable: false, identity: true),
                        BasicCompanyID = c.Long(nullable: false),
                        AccountID = c.Long(nullable: false),
                        JobCompanyID = c.Long(nullable: false),
                        JobName = c.String(maxLength: 50, storeType: "nvarchar"),
                        FunctionID = c.Long(nullable: false),
                        DistrictID = c.Long(nullable: false),
                        Reporter = c.String(maxLength: 30, storeType: "nvarchar"),
                        SubordinateCount = c.Int(nullable: false),
                        MaxAnnualSalary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MinAnnualSalary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SalaryOpen = c.Boolean(nullable: false),
                        SalaryComposition = c.String(maxLength: 30, storeType: "nvarchar"),
                        SalaryCompositionExtra = c.String(maxLength: 50, storeType: "nvarchar"),
                        SocialSecurity = c.String(maxLength: 30, storeType: "nvarchar"),
                        SocialSecurityExtra = c.String(maxLength: 50, storeType: "nvarchar"),
                        LiveWelfare = c.String(maxLength: 30, storeType: "nvarchar"),
                        LiveWelfareExtra = c.String(maxLength: 50, storeType: "nvarchar"),
                        AnnualLeave = c.String(maxLength: 30, storeType: "nvarchar"),
                        AnnualLeaveExtra = c.String(maxLength: 50, storeType: "nvarchar"),
                        Subsidy = c.String(maxLength: 30, storeType: "nvarchar"),
                        SubsidyExtra = c.String(maxLength: 50, storeType: "nvarchar"),
                        AgeRequirements = c.String(maxLength: 30, storeType: "nvarchar"),
                        MajorRequirements = c.String(maxLength: 30, storeType: "nvarchar"),
                        WorkYears = c.Int(nullable: false),
                        Degree = c.Int(nullable: false),
                        FullTime = c.Boolean(nullable: false),
                        Language = c.String(maxLength: 200, storeType: "nvarchar"),
                        LanguageExtra = c.String(maxLength: 200, storeType: "nvarchar"),
                        Skills = c.String(maxLength: 200, storeType: "nvarchar"),
                        SkillsExtra = c.String(maxLength: 200, storeType: "nvarchar"),
                        JobDescription = c.String(maxLength: 3000, storeType: "nvarchar"),
                        JobRequirements = c.String(maxLength: 3000, storeType: "nvarchar"),
                        ExtraInfo = c.String(maxLength: 3000, storeType: "nvarchar"),
                        ActiveStatus = c.Boolean(nullable: false),
                        EmergencyLevel = c.Int(nullable: false),
                        AgentJob = c.Boolean(nullable: false),
                        ShowPersonal = c.Boolean(nullable: false),
                        ShowItems = c.String(unicode: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        LastModifiedTime = c.DateTime(nullable: false, precision: 0),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PKID);
            
            CreateTable(
                "dbo.TP_JobInterview",
                c => new
                    {
                        PKID = c.Long(nullable: false, identity: true),
                        CompanyID = c.Long(nullable: false),
                        UserID = c.Long(nullable: false),
                        AccountID = c.Long(nullable: false),
                        InterviewStatus = c.Int(nullable: false),
                        InterviewDate = c.DateTime(precision: 0),
                        Comment = c.String(maxLength: 3000, storeType: "nvarchar"),
                        Remark = c.String(maxLength: 3000, storeType: "nvarchar"),
                        InviteLetter = c.String(maxLength: 3000, storeType: "nvarchar"),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        LastModifiedTime = c.DateTime(nullable: false, precision: 0),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PKID);
            
            CreateTable(
                "dbo.TP_JobResponse",
                c => new
                    {
                        PKID = c.Long(nullable: false, identity: true),
                        CompanyID = c.Long(nullable: false),
                        UserID = c.Long(nullable: false),
                        AccountID = c.Long(nullable: false),
                        RequestID = c.Long(nullable: false),
                        ResponseValue = c.String(maxLength: 200, storeType: "nvarchar"),
                        Remark = c.String(maxLength: 3000, storeType: "nvarchar"),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        LastModifiedTime = c.DateTime(nullable: false, precision: 0),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PKID);
            
            CreateTable(
                "dbo.TP_JobView",
                c => new
                    {
                        PKID = c.Long(nullable: false, identity: true),
                        JobID = c.Long(nullable: false),
                        UserID = c.Long(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        LastModifiedTime = c.DateTime(nullable: false, precision: 0),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PKID);
            
            CreateTable(
                "dbo.TP_LetterTemplate",
                c => new
                    {
                        PKID = c.Long(nullable: false, identity: true),
                        CompanyID = c.Long(nullable: false),
                        AccountID = c.Long(nullable: false),
                        TemplateName = c.String(maxLength: 50, storeType: "nvarchar"),
                        TemplateContent = c.String(maxLength: 3000, storeType: "nvarchar"),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        LastModifiedTime = c.DateTime(nullable: false, precision: 0),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PKID);
            
            CreateTable(
                "dbo.TP_MessageConfig",
                c => new
                    {
                        PKID = c.Long(nullable: false, identity: true),
                        ConfigType = c.Int(nullable: false),
                        ProviderName = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        ApiName = c.String(maxLength: 50, storeType: "nvarchar"),
                        ApiCode = c.String(maxLength: 50, storeType: "nvarchar"),
                        ApiUrl = c.String(maxLength: 200, storeType: "nvarchar"),
                        Port = c.Int(),
                        EnableSsl = c.Boolean(),
                        SenderName = c.String(maxLength: 50, storeType: "nvarchar"),
                        LoginName = c.String(maxLength: 50, storeType: "nvarchar"),
                        Password = c.String(maxLength: 256, storeType: "nvarchar"),
                        VerifyKey = c.String(maxLength: 256, storeType: "nvarchar"),
                        SignMark = c.String(maxLength: 50, storeType: "nvarchar"),
                        IsActivated = c.Boolean(nullable: false),
                        TimesLimit = c.Int(),
                        Remark = c.String(maxLength: 3000, storeType: "nvarchar"),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        LastModifiedTime = c.DateTime(nullable: false, precision: 0),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PKID);
            
            CreateTable(
                "dbo.TP_MessageTemplate",
                c => new
                    {
                        PKID = c.Long(nullable: false, identity: true),
                        TemplateType = c.Int(nullable: false),
                        TemplateCode = c.String(maxLength: 50, storeType: "nvarchar"),
                        TemplateTitle = c.String(maxLength: 50, storeType: "nvarchar"),
                        TemplateContent = c.String(maxLength: 3000, storeType: "nvarchar"),
                        IsActivated = c.Boolean(nullable: false),
                        Remark = c.String(maxLength: 3000, storeType: "nvarchar"),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        LastModifiedTime = c.DateTime(nullable: false, precision: 0),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PKID);
            
            CreateTable(
                "dbo.TP_Notice",
                c => new
                    {
                        PKID = c.Long(nullable: false, identity: true),
                        MessageSubject = c.String(maxLength: 100, storeType: "nvarchar"),
                        MessageContent = c.String(maxLength: 3000, storeType: "nvarchar"),
                        MessageType = c.Int(nullable: false),
                        UserIDs = c.String(maxLength: 3000, storeType: "nvarchar"),
                        Remark = c.String(maxLength: 3000, storeType: "nvarchar"),
                        SendClient = c.Int(nullable: false),
                        IsFinish = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        LastModifiedTime = c.DateTime(nullable: false, precision: 0),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PKID);
            
            CreateTable(
                "dbo.TP_NoticeUser",
                c => new
                    {
                        PKID = c.Long(nullable: false, identity: true),
                        MessageID = c.Long(nullable: false),
                        UserID = c.Long(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        LastModifiedTime = c.DateTime(nullable: false, precision: 0),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PKID);
            
            CreateTable(
                "dbo.TP_ProfileSnap",
                c => new
                    {
                        PKID = c.Long(nullable: false, identity: true),
                        UserID = c.Long(nullable: false),
                        RealName = c.String(maxLength: 50, storeType: "nvarchar"),
                        Gender = c.Int(nullable: false),
                        Birthday = c.DateTime(nullable: false, precision: 0),
                        Education = c.String(maxLength: 50, storeType: "nvarchar"),
                        BirthPlace = c.String(maxLength: 100, storeType: "nvarchar"),
                        Residence = c.String(maxLength: 100, storeType: "nvarchar"),
                        CensusRegister = c.String(maxLength: 100, storeType: "nvarchar"),
                        CurrentCompany = c.String(maxLength: 100, storeType: "nvarchar"),
                        CurrentPosition = c.String(maxLength: 50, storeType: "nvarchar"),
                        WorkYears = c.Int(nullable: false),
                        CurrentAnnualSalary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrentOnOpen = c.Boolean(nullable: false),
                        JobStatus = c.Int(nullable: false),
                        Introduction = c.String(maxLength: 1000, storeType: "nvarchar"),
                        FunctionIDs = c.String(maxLength: 100, storeType: "nvarchar"),
                        LocationIDs = c.String(maxLength: 100, storeType: "nvarchar"),
                        IntentionAnnualSalary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IntentionOnOpen = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        LastModifiedTime = c.DateTime(nullable: false, precision: 0),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PKID);
            
            CreateTable(
                "dbo.TP_ProfileView",
                c => new
                    {
                        PKID = c.Long(nullable: false, identity: true),
                        UserID = c.Long(nullable: false),
                        AgentID = c.Long(nullable: false),
                        CompanyID = c.Long(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        LastModifiedTime = c.DateTime(nullable: false, precision: 0),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PKID);
            
            CreateTable(
                "dbo.TP_School",
                c => new
                    {
                        PKID = c.Long(nullable: false, identity: true),
                        CountryID = c.Long(nullable: false),
                        DistrictID = c.Long(nullable: false),
                        CityID = c.Long(nullable: false),
                        SchoolType = c.Int(nullable: false),
                        SchoolGrade = c.String(maxLength: 50, storeType: "nvarchar"),
                        IsPublic = c.Boolean(nullable: false),
                        SchoolName = c.String(maxLength: 200, storeType: "nvarchar"),
                        SchoolNameEN = c.String(maxLength: 200, storeType: "nvarchar"),
                        AnotherName = c.String(maxLength: 100, storeType: "nvarchar"),
                        ShortName = c.String(maxLength: 50, storeType: "nvarchar"),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        LastModifiedTime = c.DateTime(nullable: false, precision: 0),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PKID);
            
            CreateTable(
                "dbo.TP_SiteConfig",
                c => new
                    {
                        PKID = c.Long(nullable: false, identity: true),
                        ConfigName = c.String(maxLength: 50, storeType: "nvarchar"),
                        ConfigCode = c.String(maxLength: 50, storeType: "nvarchar"),
                        ConfigValue = c.String(maxLength: 1000, storeType: "nvarchar"),
                        OnInternal = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        LastModifiedTime = c.DateTime(nullable: false, precision: 0),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PKID);
            
            CreateTable(
                "dbo.TP_Skills",
                c => new
                    {
                        PKID = c.Long(nullable: false, identity: true),
                        SkillName = c.String(maxLength: 50, storeType: "nvarchar"),
                        OnEnabled = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        LastModifiedTime = c.DateTime(nullable: false, precision: 0),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PKID);
            
            CreateTable(
                "dbo.TP_SystemUser",
                c => new
                    {
                        PKID = c.Long(nullable: false, identity: true),
                        LoginName = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        UserName = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        Email = c.String(maxLength: 100, storeType: "nvarchar"),
                        Password = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                        Remark = c.String(maxLength: 200, storeType: "nvarchar"),
                        OnInternal = c.Boolean(nullable: false),
                        OnActive = c.Boolean(nullable: false),
                        ActiveTime = c.DateTime(nullable: false, precision: 0),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        LastModifiedTime = c.DateTime(nullable: false, precision: 0),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PKID);
            
            CreateTable(
                "dbo.TP_Tags",
                c => new
                    {
                        PKID = c.Long(nullable: false, identity: true),
                        ParentID = c.Long(nullable: false),
                        Tags = c.String(maxLength: 200, storeType: "nvarchar"),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        LastModifiedTime = c.DateTime(nullable: false, precision: 0),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PKID);
            
            CreateTable(
                "dbo.TP_UserAvoid",
                c => new
                    {
                        PKID = c.Long(nullable: false, identity: true),
                        UserID = c.Long(nullable: false),
                        AvoidDomain = c.String(maxLength: 200, storeType: "nvarchar"),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        LastModifiedTime = c.DateTime(nullable: false, precision: 0),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PKID);
            
            CreateTable(
                "dbo.TP_UserEducation",
                c => new
                    {
                        PKID = c.Long(nullable: false, identity: true),
                        UserID = c.Long(nullable: false),
                        StartDate = c.DateTime(nullable: false, precision: 0),
                        ExpirationDate = c.String(maxLength: 100, storeType: "nvarchar"),
                        SchoolName = c.String(maxLength: 100, storeType: "nvarchar"),
                        MajorSubject = c.String(maxLength: 100, storeType: "nvarchar"),
                        Degree = c.Int(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        LastModifiedTime = c.DateTime(nullable: false, precision: 0),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PKID);
            
            CreateTable(
                "dbo.TP_UserFeedback",
                c => new
                    {
                        PKID = c.Long(nullable: false, identity: true),
                        ParentID = c.Long(nullable: false),
                        UserID = c.Long(nullable: false),
                        FeedbackTitle = c.String(maxLength: 256, storeType: "nvarchar"),
                        FeedbackContent = c.String(maxLength: 3000, storeType: "nvarchar"),
                        FeedbackResponse = c.String(maxLength: 3000, storeType: "nvarchar"),
                        Status = c.Int(nullable: false),
                        Remark = c.String(maxLength: 3000, storeType: "nvarchar"),
                        From = c.Int(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        LastModifiedTime = c.DateTime(nullable: false, precision: 0),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PKID);
            
            CreateTable(
                "dbo.TP_UserGuidance",
                c => new
                    {
                        PKID = c.Long(nullable: false, identity: true),
                        UserID = c.Long(nullable: false),
                        GuidanceType = c.Int(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        LastModifiedTime = c.DateTime(nullable: false, precision: 0),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PKID);
            
            CreateTable(
                "dbo.TP_UserJobFollow",
                c => new
                    {
                        PKID = c.Long(nullable: false, identity: true),
                        JobID = c.Long(nullable: false),
                        UserID = c.Long(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        LastModifiedTime = c.DateTime(nullable: false, precision: 0),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PKID);
            
            CreateTable(
                "dbo.TP_UserJobIntention",
                c => new
                    {
                        PKID = c.Long(nullable: false, identity: true),
                        UserID = c.Long(nullable: false),
                        FunctionIDs = c.String(maxLength: 100, storeType: "nvarchar"),
                        LocationIDs = c.String(maxLength: 100, storeType: "nvarchar"),
                        AnnualSalary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OnOpen = c.Boolean(nullable: false),
                        IsUpdated = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        LastModifiedTime = c.DateTime(nullable: false, precision: 0),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PKID);
            
            CreateTable(
                "dbo.TP_UserMessage",
                c => new
                    {
                        PKID = c.Long(nullable: false, identity: true),
                        RecieverID = c.Long(nullable: false),
                        MessageSubject = c.String(maxLength: 256, storeType: "nvarchar"),
                        MessageContent = c.String(maxLength: 3000, storeType: "nvarchar"),
                        IsSent = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        LastModifiedTime = c.DateTime(nullable: false, precision: 0),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PKID);
            
            CreateTable(
                "dbo.TP_UserMessageRead",
                c => new
                    {
                        PKID = c.Long(nullable: false, identity: true),
                        MessageID = c.Long(nullable: false),
                        UserID = c.Long(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        LastModifiedTime = c.DateTime(nullable: false, precision: 0),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PKID);
            
            CreateTable(
                "dbo.TP_UserProfile",
                c => new
                    {
                        PKID = c.Long(nullable: false, identity: true),
                        UserID = c.Long(nullable: false),
                        RealName = c.String(maxLength: 50, storeType: "nvarchar"),
                        Gender = c.Int(nullable: false),
                        Birthday = c.DateTime(nullable: false, precision: 0),
                        Education = c.String(maxLength: 50, storeType: "nvarchar"),
                        BirthPlace = c.String(maxLength: 100, storeType: "nvarchar"),
                        Residence = c.String(maxLength: 100, storeType: "nvarchar"),
                        CensusRegister = c.String(maxLength: 100, storeType: "nvarchar"),
                        CurrentCompany = c.String(maxLength: 100, storeType: "nvarchar"),
                        CurrentPosition = c.String(maxLength: 50, storeType: "nvarchar"),
                        WorkYears = c.Int(nullable: false),
                        AnnualSalary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OnOpen = c.Boolean(nullable: false),
                        JobStatus = c.Int(nullable: false),
                        Introduction = c.String(maxLength: 1000, storeType: "nvarchar"),
                        IsUpdated = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        LastModifiedTime = c.DateTime(nullable: false, precision: 0),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PKID);
            
            CreateTable(
                "dbo.TP_UserReport",
                c => new
                    {
                        PKID = c.Long(nullable: false, identity: true),
                        UserID = c.Long(nullable: false),
                        JobID = c.Long(nullable: false),
                        Reason = c.String(maxLength: 100, storeType: "nvarchar"),
                        ReasonExtra = c.String(maxLength: 3000, storeType: "nvarchar"),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        LastModifiedTime = c.DateTime(nullable: false, precision: 0),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PKID);
            
            CreateTable(
                "dbo.TP_UserTags",
                c => new
                    {
                        PKID = c.Long(nullable: false, identity: true),
                        TagID = c.Long(nullable: false),
                        UserID = c.Long(nullable: false),
                        Tags = c.String(maxLength: 200, storeType: "nvarchar"),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        LastModifiedTime = c.DateTime(nullable: false, precision: 0),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PKID);
            
            CreateTable(
                "dbo.TP_UserWorkExperience",
                c => new
                    {
                        PKID = c.Long(nullable: false, identity: true),
                        UserID = c.Long(nullable: false),
                        StartDate = c.DateTime(nullable: false, precision: 0),
                        ExpirationDate = c.String(maxLength: 100, storeType: "nvarchar"),
                        CompanyName = c.String(maxLength: 100, storeType: "nvarchar"),
                        Position = c.String(maxLength: 50, storeType: "nvarchar"),
                        Introduction = c.String(maxLength: 3000, storeType: "nvarchar"),
                        FunctionIDs = c.String(maxLength: 500, storeType: "nvarchar"),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        LastModifiedTime = c.DateTime(nullable: false, precision: 0),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PKID);
            
            CreateTable(
                "dbo.TP_WechatMessage",
                c => new
                    {
                        PKID = c.Long(nullable: false, identity: true),
                        EventType = c.String(maxLength: 50, storeType: "nvarchar"),
                        MessageType = c.String(maxLength: 50, storeType: "nvarchar"),
                        MessageName = c.String(maxLength: 50, storeType: "nvarchar"),
                        MessageCode = c.String(maxLength: 50, storeType: "nvarchar"),
                        MessageLink = c.String(maxLength: 500, storeType: "nvarchar"),
                        ImgUrl = c.String(maxLength: 2000, storeType: "nvarchar"),
                        Description = c.String(maxLength: 500, storeType: "nvarchar"),
                        StartDate = c.DateTime(precision: 0),
                        EndDate = c.DateTime(precision: 0),
                        OnActive = c.Boolean(),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        LastModifiedTime = c.DateTime(nullable: false, precision: 0),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PKID);
            
            CreateTable(
                "dbo.TP_WechatUser",
                c => new
                    {
                        PKID = c.Long(nullable: false, identity: true),
                        AppID = c.String(maxLength: 128, storeType: "nvarchar"),
                        UnionID = c.String(maxLength: 128, storeType: "nvarchar"),
                        OpenID = c.String(maxLength: 128, storeType: "nvarchar"),
                        NickName = c.String(maxLength: 50, storeType: "nvarchar"),
                        AvatarUrl = c.String(maxLength: 500, storeType: "nvarchar"),
                        City = c.String(maxLength: 100, storeType: "nvarchar"),
                        Province = c.String(maxLength: 100, storeType: "nvarchar"),
                        Country = c.String(maxLength: 100, storeType: "nvarchar"),
                        Language = c.String(maxLength: 100, storeType: "nvarchar"),
                        Remark = c.String(maxLength: 3000, storeType: "nvarchar"),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        LastModifiedTime = c.DateTime(nullable: false, precision: 0),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PKID);
            
            CreateTable(
                "dbo.TP_WorkExperienceSnap",
                c => new
                    {
                        PKID = c.Long(nullable: false, identity: true),
                        UserID = c.Long(nullable: false),
                        StartDate = c.DateTime(nullable: false, precision: 0),
                        ExpirationDate = c.String(maxLength: 100, storeType: "nvarchar"),
                        CompanyName = c.String(maxLength: 100, storeType: "nvarchar"),
                        Position = c.String(maxLength: 50, storeType: "nvarchar"),
                        Introduction = c.String(maxLength: 3000, storeType: "nvarchar"),
                        FunctionIDs = c.String(maxLength: 500, storeType: "nvarchar"),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        LastModifiedTime = c.DateTime(nullable: false, precision: 0),
                        Valid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PKID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TP_WorkExperienceSnap");
            DropTable("dbo.TP_WechatUser");
            DropTable("dbo.TP_WechatMessage");
            DropTable("dbo.TP_UserWorkExperience");
            DropTable("dbo.TP_UserTags");
            DropTable("dbo.TP_UserReport");
            DropTable("dbo.TP_UserProfile");
            DropTable("dbo.TP_UserMessageRead");
            DropTable("dbo.TP_UserMessage");
            DropTable("dbo.TP_UserJobIntention");
            DropTable("dbo.TP_UserJobFollow");
            DropTable("dbo.TP_UserGuidance");
            DropTable("dbo.TP_UserFeedback");
            DropTable("dbo.TP_UserEducation");
            DropTable("dbo.TP_UserAvoid");
            DropTable("dbo.TP_Tags");
            DropTable("dbo.TP_SystemUser");
            DropTable("dbo.TP_Skills");
            DropTable("dbo.TP_SiteConfig");
            DropTable("dbo.TP_School");
            DropTable("dbo.TP_ProfileView");
            DropTable("dbo.TP_ProfileSnap");
            DropTable("dbo.TP_NoticeUser");
            DropTable("dbo.TP_Notice");
            DropTable("dbo.TP_MessageTemplate");
            DropTable("dbo.TP_MessageConfig");
            DropTable("dbo.TP_LetterTemplate");
            DropTable("dbo.TP_JobView");
            DropTable("dbo.TP_JobResponse");
            DropTable("dbo.TP_JobInterview");
            DropTable("dbo.TP_JobDraft");
            DropTable("dbo.TP_JobComment");
            DropTable("dbo.TP_JobApply");
            DropTable("dbo.TP_Job");
            DropTable("dbo.TP_FunctionSkillsRelation");
            DropTable("dbo.TP_Function");
            DropTable("dbo.TP_EducationSnap");
            DropTable("dbo.TP_District");
            DropTable("dbo.TP_Country");
            DropTable("dbo.TP_CompanyContract");
            DropTable("dbo.TP_Company");
            DropTable("dbo.TP_AgentCompany");
            DropTable("dbo.TP_AccountNotice");
            DropTable("dbo.TP_AccountInvitation");
            DropTable("dbo.TP_AccountInfoSearchLog");
            DropTable("dbo.TP_AccountFollowFolder");
            DropTable("dbo.TP_AccountFollow");
            DropTable("dbo.TP_AccountFolder");
            DropTable("dbo.TP_Account");
            DropTable("dbo.TP_AccessToken");
            DropTable("dbo.mq_user");
        }
    }
}
