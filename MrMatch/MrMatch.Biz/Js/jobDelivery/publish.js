
var radioOther = {
    props: ['name', 'value', 'label'],
    data: function () {
        return {
            checked: '',
            inputValue: '',
            inputLabel: '',
        }
    },
    template:
        `
        <label class="switch switch-default switch-danger-outline switch-pill mr-2 switch-other" >
            <el-radio  v-model="inputLabel" label="其它">其它</el-radio>
            <div class="" v-if="inputLabel=='其它'" style="width:160px">
                <input type="text" class="switch-other--text el-input__inner" style="width:160px;" v-model="inputValue" />
            </div> 
        </label>
     `,
    watch: {
        value(newVal, oldVal) {
            this.inputValue = newVal
        },
        label(newVal, oldVal) {
            this.inputLabel = newVal
        },
        inputLabel(newVal, oldVal) {
            this.$emit("change", { name: this.name, value: newVal == '其它' ? this.value : '', label: newVal, type: 'radio' });
        },
        inputValue(newVal, oldVal) {
            this.$emit("change", { name: this.name, value: newVal, type: 'radio' });
        }
    },
    mounted: function () { },
    methods: {}
}
var checkboxOther = {
    model: 'value',
    props: ['name', 'value'],
    data: function () {
        return {
            checked: false,
            inputValue: '',
        }
    },
    template:
        `
        <label class="switch switch-default switch-danger-outline switch-pill mr-2 switch-other" >
            <el-checkbox v-model="checked">其它</el-checkbox>
            <div class="" v-if="checked" style="width:160px">
                <input type="text" class="switch-other--text el-input__inner" style="width:160px;" v-model="inputValue" />
            </div> 
        </label>
    `,
    watch: {
        checked(newVal, oldVal) {
            this.$emit("change", { name: this.name, value: newVal ? this.value : '', type: 'checkbox' });
        },
        value(newVal, oldVal) {
            this.inputValue = newVal
        },
        inputValue(newVal, oldVal) {
            if (newVal) this.checked = true
            this.$emit("change", { name: this.name, value: newVal, type: 'checkbox' });
        }
    },
    mounted: function () { },
    methods: {}
}
window.vm = new Vue({
    el: "#appPublish",
    components: {
        'checkbox-other': checkboxOther,
        'radio-other': radioOther
    },
    data: {
        pageType: 'job',
        ruleForm: {
            JobCompanyID: "",
            agentShortName: "",
            basicName: "",
            basicShortName: "",
            JobName: "",
            funcListDefault: "",
            distListDefault: "",
            Reporter: "",
            SubordinateCount: "",
            salaryOn: "",
            salaryOff: "",
            salaryOpen: false,
            MajorRequirements: "",
            ageOn: "",
            ageOff: "",
            WorkYears: '',
            Degree: "",
            JobDescription: "",
            JobRequirements: "",
        },
        rules: {
            JobCompanyID: [
                { required: true, message: '请选择企业', trigger: 'change' }
            ],
            basicName: [
                { required: true, message: '请选择职能', trigger: 'blur' }
            ],
            JobName: [
                { required: true, message: '请输入职位名称', trigger: 'change' }
            ],
            funcListDefault: [
                { required: true, message: '请选择职能', trigger: 'change' }
            ],
            distListDefault: [
                { required: true, message: '请选择地点', trigger: 'change' }
            ],
            salaryOn: [
                { required: true, message: '请输入年薪范围', trigger: 'change' }
            ],
            salaryOff: [
                { required: true, message: '请输入年薪范围', trigger: 'change' }
            ],
            salaryOpen: [
                { required: true, message: '请选择薪资是否公开', trigger: 'change' }
            ],
            WorkYears: [
                { required: true, message: '请输入工作年限', trigger: 'change' }
            ],
            Degree: [
                { required: true, message: '请选择学历', trigger: 'change' }
            ],
            JobDescription: [
                { required: true, message: '请输入职位介绍', trigger: 'change' }
            ],
            JobRequirements: [
                { required: true, message: '请输入岗位要求', trigger: 'change' }
            ]
        },
        basicData: {
            agent: false,
            basicID: 0,
            agentCompany: [],
            funcList: [],
            districtList: [],
            ageOn: "",
            ageOff: "",
            fromType: "",
            accountID: 0
        },
        educationType: [
            { label: "不限", value: 0 },
            { label: "大专", value: 40 },
            { label: "本科", value: 50 },
            { label: "硕士", value: 60 },
            { label: "MBA/EMBA", value: 70 },
            { label: "博士", value: 80 }
        ],
        composition: {
            salaryComposition: [],
            salaryCompositionExtra: '',
            socialSecurity: [],
            socialSecurityExtra: '',
            liveWelfare: [],
            liveWelfareExtra: '',
            annualLeave: [],
            annualLeaveExtra: '',
            subsidy: "",
            subsidyExtra: '',
            FullTime: false,
            Language: [],
            LanguageExtra: "",
            SkillsExtra: "",
            ExtraInfo: "",
            ActiveStatus: 0
        },
        form: {
            PKID: 0,
            BasicCompanyID: 0,
            AccountID: 0,
            JobCompanyID: "",
            JobName: "",
            FunctionID: 0,
            DistrictID: 0,
            Reporter: "",
            SubordinateCount: "",
            MinAnnualSalary: "",
            MaxAnnualSalary: "",
            SalaryOpen: false,
            SalaryComposition: "",
            SalaryCompositionExtra: "",
            SocialSecurity: "",
            SocialSecurityExtra: "",
            LiveWelfare: "",
            LiveWelfareExtra: "",
            AnnualLeave: "",
            AnnualLeaveExtra: "",
            Subsidy: "",
            SubsidyExtra: "",
            AgeRequirements: "",
            MajorRequirements: "",
            WorkYears: 0,
            Degree: 0,
            FullTime: false,
            Language: "",
            LanguageExtra: "",
            Skills: "",
            SkillsExtra: "",
            JobDescription: "",
            JobRequirements: "",
            ExtraInfo: "",
            ActiveStatus: 0,
            AgentJob: false,
            ShowPersonal: true,
            ShowItems: ""
        },
        checkList: {
            phone: false,
            wechat: false,
            wechatpic: false,
            linkin: false,
            focus: false,
            introduction: false
        },
        skillsDialogVisible: false,
        skillsList: [],
        skillsSelectedList: [],

        editCompanyData: {},
        editCompanyDialog: false,
        accountInfo: {
            phone: "",
            wechat: "",
            linkin: "",
            focus: "",
            introduction: ""
        }
    },
    watch: {
        ['composition.salaryComposition'](newVal, oldVal) {
            console.log(newVal, this.composition.salaryCompositionExtra)
        },
        ['composition.liveWelfare'](newVal, oldVal) {
            console.log('liveWelfare', newVal)
        },
        ['form.ShowPersonal'](newVal, oldVal) {
            if (!newVal) {
                this.checkList = {
                    phone: false,
                    wechat: false,
                    wechatpic: false,
                    linkin: false,
                    focus: false,
                    introduction: false
                }
                this.form.ShowItems = ""
            }
        }

    },
    mounted: function () {
        var ajax1 = this.getBasic();
        var ajax2 = this.getBasicFunc();
        var ajax3 = this.getBasicDist();

        if (ChkUtil.getQueryString('type')) {
            this.pageType = ChkUtil.getQueryString('type')
        }
        if (ChkUtil.getQueryString('id')) {
            this.form.PKID = this.pageType == 'draft' ? 0 : ChkUtil.getQueryString('id')
            Promise.all([ajax1, ajax2, ajax3]).then((result) => {
                this.getJobPushProfile()
            });
        }


        var publishBar = this.$refs['publishBar']
        var publishBarCard = this.$refs['publishBarCard']

        $(window).on('load resize scroll', function () {
            if (publishBar.getBoundingClientRect().top > (window.innerHeight - publishBarCard.clientHeight)) {
                if (publishBarCard.className.indexOf('fixed') == -1) {
                    publishBarCard.className += ' fixed'
                }
            } else {
                if (publishBarCard.className.indexOf('fixed') != -1) {
                    publishBarCard.className = publishBarCard.className.substring(0, publishBarCard.className.indexOf('fixed'))
                }
            }
        })
    },
    methods: {
        getBasic: function () {
            return axiosGet(api.delivery + "GetBasicInfo")
                .then((res) => {
                    this.basicData.agent = res.data.type;
                    this.basicData.basicID = res.data.basicId;
                    this.basicData.accountID = res.data.accountID;
                    this.ruleForm.basicName = res.data.basicCom;
                    this.ruleForm.basicShortName = res.data.basicShortName;
                    this.accountInfo.phone = res.data.phone;
                    this.accountInfo.wechat = res.data.wechat;
                    this.accountInfo.linkin = res.data.linkin;
                    this.accountInfo.focus = res.data.focus;
                    this.accountInfo.introduction = res.data.introduction;
                    var basic = {
                        ID: res.data.basicId,
                        Name: res.data.basicCom,
                        ShortName: res.data.basicShortName,
                        _ID:0,
                        Agent: false
                    };
                    for (let i = 0; i < res.data.agentCom.length; i++) {
                        res.data.agentCom[i].Agent = true
                        res.data.agentCom[i]._ID = res.data.agentCom[i].ID
                    }
                    this.basicData.agentCompany = res.data.agentCom;
                    this.basicData.agentCompany.push(basic);
                })
        },
        getBasicFunc: function () {
            return axiosGet(api.candidate + "GetFunctionForCascader")
                .then((res) => {
                    this.basicData.funcList = res.data;
                })
        },
        getBasicDist: function () {
            return axiosGet(api.candidate + "GetDistrictForCascader")
                .then((res) => {
                    this.basicData.districtList = res.data;
                })
        },
        getJobPushProfile() {
            console.log(this.pageType)
            let url = "GetJobInfo"
            let params = {
                PKID: this.form.PKID,
                accountID: this.basicData.accountID
            }
            if (this.pageType == 'draft') {
                url = "GetJobDraftInfo"
                params = {
                    PKID: ChkUtil.getQueryString('id'),
                    accountID: this.basicData.accountID
                }
            }
            axiosGet(api.delivery + url, params)
                .then((res) => {
                    if (res.code == 1) {

                        let data = res.data ? (this.pageType == 'draft' ? res.data : res.data.jobData) : []

                        this.selChange(data.JobCompanyID)

                        this.ruleForm.JobName = data.JobName

                        var skillAjax = this.funcListDefaultInit(data.FunctionID)

                        this.distListDefaultInit(data.DistrictID)

                        this.ruleForm.Reporter = data.Reporter
                        this.ruleForm.SubordinateCount = data.SubordinateCount == 0 ? '' : data.SubordinateCount
                        this.ruleForm.salaryOff = data.MinAnnualSalary
                        this.ruleForm.salaryOn = data.MaxAnnualSalary
                        this.ruleForm.salaryOpen = data.SalaryOpen

                        this.composition.salaryComposition = data.SalaryComposition.split(',')
                        this.composition.salaryCompositionExtra = data.SalaryCompositionExtra
                        this.composition.socialSecurity = data.SocialSecurity.split(',')
                        this.composition.socialSecurityExtra = data.SocialSecurityExtra
                        this.composition.liveWelfare = data.LiveWelfare.split(',')
                        this.composition.liveWelfareExtra = data.LiveWelfareExtra
                        this.composition.annualLeave = data.AnnualLeave.split(',')
                        this.composition.annualLeaveExtra = data.AnnualLeaveExtra
                        this.composition.subsidy = data.Subsidy
                        this.composition.subsidyExtra = data.SubsidyExtra

                        this.ruleForm.ageOff = data.AgeRequirements.split(',')[0]
                        this.ruleForm.ageOn = data.AgeRequirements.split(',')[1]
                        this.ruleForm.MajorRequirements = data.MajorRequirements
                        this.ruleForm.WorkYears = data.WorkYears
                        this.ruleForm.Degree = data.Degree

                        this.composition.FullTime = data.FullTime

                        this.composition.Language = data.Language.split(',')
                        this.composition.LanguageExtra = data.LanguageExtra
                        this.form.ShowPersonal = data.ShowPersonal
                        this.form.ShowItems = data.ShowItems
                        if (data.ShowPersonal) {
                            var items = data.ShowItems.split(',')
                            if (items.indexOf("phone") != -1) {
                                this.checkList.phone = true
                            }

                            if (items.indexOf("wechat")) {
                                this.checkList.wechat = true
                            }

                            if (items.indexOf("wechatpic")) {
                                this.checkList.wechatpic = true
                            }

                            if (items.indexOf("linkin")) {
                                this.checkList.linkin = true
                            }

                            if (items.indexOf("focus")) {
                                this.checkList.focus = true
                            }

                            if (items.indexOf("introduction")) {
                                this.checkList.introduction = true
                            }
                        }

                            Promise.all([skillAjax]).then((result) => {
                                var skillsArr = data.Skills.length > 0 ? data.Skills.split(',') : []
                                this.skillsSelectedList = []

                                for (let i = 0; i < this.skillsList.length; i++) {
                                    for (let j = 0; j < skillsArr.length; j++) {
                                        if (this.skillsList[i].SkillName == skillsArr[j]) {
                                            this.skillsList[i].Selected = true
                                            this.skillsSelectedList.push(this.skillsList[i])
                                        }
                                    }
                                }
                            });

                        this.composition.SkillsExtra = data.SkillsExtra

                        this.ruleForm.JobDescription = data.JobDescription
                        this.ruleForm.JobRequirements = data.JobRequirements
                        this.composition.ExtraInfo = data.ExtraInfo

                        if (data.ActiveStatus) {
                            this.composition.ActiveStatus = 1
                        } else {
                            this.composition.ActiveStatus = 0
                        }
                        

                    }
                })
        },
        distListDefaultInit(val) {
            for (var i = 0; i < this.basicData.districtList.length; i++) {
                if (this.basicData.districtList[i].Children) {
                    for (var j = 0; j < this.basicData.districtList[i].Children.length; j++) {
                        if (val == this.basicData.districtList[i].Children[j].PKID) {
                            this.ruleForm.distListDefault = [this.basicData.districtList[i].PKID, this.basicData.districtList[i].Children[j].PKID]
                            return;
                        }
                    }
                }
            }
        },
        funcListDefaultInit(val) {
            for (var i = 0; i < this.basicData.funcList.length; i++) {
                for (var j = 0; j < this.basicData.funcList[i].children.length; j++) {
                    if (val == this.basicData.funcList[i].children[j].value) {
                        this.ruleForm.funcListDefault = [this.basicData.funcList[i].value, this.basicData.funcList[i].children[j].value]

                        return this.getSkillsList(this.basicData.funcList[i].children[j].value);
                    }
                    if (this.basicData.funcList[i].children[j].children) {
                        for (var k = 0; k < this.basicData.funcList[i].children[j].children.length; k++) {
                            if (val == this.basicData.funcList[i].children[j].children[k].value) {
                                this.ruleForm.funcListDefault = [this.basicData.funcList[i].value, this.basicData.funcList[i].children[j].value, this.basicData.funcList[i].children[j].children[k].value]

                                return this.getSkillsList(this.basicData.funcList[i].children[j].children[k].value);
                            }
                        }
                    }

                }
            }
        },
        selChange: function (val) {
            console.log(val)
            for (var i = 0; i < this.basicData.agentCompany.length; i++) {
                if (val == this.basicData.agentCompany[i]._ID) {
                    console.log(this.basicData.agentCompany[i])
                    this.ruleForm.agentShortName = this.basicData.agentCompany[i].ShortName;
                    this.ruleForm.JobCompanyID = this.basicData.agentCompany[i]._ID;
                    this.form.AgentJob = this.basicData.agentCompany[i].Agent;
                }
            }
        },
        submitForm(formName, type) {
            this.$refs[formName].validate((valid) => {
                if (valid) {
                    
                    if (this.basicData.agent) {
                        this.form.JobCompanyID = this.ruleForm.JobCompanyID;
                    } else {
                        this.form.JobCompanyID = this.basicData.basicID;
                    }

                    if (this.form.ShowPersonal) {
                        if (this.checkList.phone) {
                            this.form.ShowItems += "phone,"
                        }
                        if (this.checkList.wechat) {
                            this.form.ShowItems += "wechat,"
                        }
                        if (this.checkList.wechatpic) {
                            this.form.ShowItems += "wechatpic,"
                        }
                        if (this.checkList.linkin) {
                            this.form.ShowItems += "linkin,"
                        }
                        if (this.checkList.focus) {
                            this.form.ShowItems += "focus,"
                        }
                        if (this.checkList.introduction) {
                            this.form.ShowItems += "introduction,"
                        }
                    }

                    this.form.JobName = this.ruleForm.JobName;
                    this.form.FunctionID = this.ruleForm.funcListDefault[this.ruleForm.funcListDefault.length - 1];
                    this.form.DistrictID = this.ruleForm.distListDefault[this.ruleForm.distListDefault.length - 1];
                    this.form.Reporter = this.ruleForm.Reporter;
                    this.form.SubordinateCount = this.ruleForm.SubordinateCount;
                    this.form.MinAnnualSalary = this.ruleForm.salaryOff;
                    this.form.MaxAnnualSalary = this.ruleForm.salaryOn;
                    this.form.SalaryOpen = this.ruleForm.salaryOpen;

                    this.form.SalaryComposition = this.composition.salaryComposition.join(',').replace(/^,+/, "").replace(/,+$/, "");
                    this.form.SalaryCompositionExtra = this.composition.salaryCompositionExtra;
                    this.form.SocialSecurity = this.composition.socialSecurity.join(',').replace(/^,+/, "").replace(/,+$/, "");
                    this.form.SocialSecurityExtra = this.composition.socialSecurityExtra;
                    this.form.LiveWelfare = this.composition.liveWelfare.join(',').replace(/^,+/, "").replace(/,+$/, "");
                    this.form.LiveWelfareExtra = this.composition.liveWelfareExtra;
                    this.form.AnnualLeave = this.composition.annualLeave.join(',').replace(/^,+/, "").replace(/,+$/, "");
                    this.form.AnnualLeaveExtra = this.composition.annualLeaveExtra;
                    this.form.Subsidy = this.composition.subsidy;
                    this.form.SubsidyExtra = this.composition.subsidy == '其它' ? this.composition.subsidyExtra : '';

                    this.form.AgeRequirements = (this.ruleForm.ageOff + (this.ruleForm.ageOn ? ("," + this.ruleForm.ageOn) : '')).replace(/^,+/, "").replace(/,+$/, "");
                    this.form.MajorRequirements = this.ruleForm.MajorRequirements;
                    this.form.WorkYears = this.ruleForm.WorkYears;
                    this.form.Degree = this.ruleForm.Degree;
                    this.form.FullTime = this.composition.FullTime;

                    this.form.Language = this.composition.Language.join(',').replace(/^,+/, "").replace(/,+$/, "");
                    this.form.LanguageExtra = this.composition.LanguageExtra;
                    this.form.Skills = this.skillsSelectedList.map(function (e, i) {
                        return e.SkillName
                    }).join(',').replace(/^,+/, "").replace(/,+$/, "");
                    this.form.SkillsExtra = this.composition.SkillsExtra;

                    this.form.JobDescription = this.ruleForm.JobDescription;
                    this.form.JobRequirements = this.ruleForm.JobRequirements;
                    this.form.ExtraInfo = this.composition.ExtraInfo;
                    this.form.ActiveStatus = this.composition.ActiveStatus;


                    //return false;

                    // 发布
                    if (type == 'publish') {
                        //let JobForm = this.form
                        //let JobForm = { ...this.form }
                        let JobForm = JSON.parse(JSON.stringify(this.form));

                        if (this.pageType == 'draft') { JobForm.PKID = 0 }
                        axiosPost(api.delivery + "SaveJob", JobForm)
                            .then((res) => {
                                this.$message(res.msg)
                                if (res.code == 1) {
                                    //location.href = "/jobdelivery/jobmanage?type=activity"
                                }
                            })
                    }
                    // 保存
                    if (type == 'draft') {
                        //let DraftForm = this.form
                        //let DraftForm = { ...this.form }
                        let DraftForm = JSON.parse(JSON.stringify(this.form));

                        if (this.pageType == 'job') { DraftForm.PKID = 0 }
                        if (this.pageType == 'draft') {
                            DraftForm.PKID = ChkUtil.getQueryString('id')
                        }
                        axiosPost(api.delivery + "SaveJobDraft", DraftForm)
                            .then((res) => {
                                this.$message(res.msg)
                                if (res.code == 1) {
                                    //location.href = "/jobdelivery/jobmanage?type=draft"
                                }
                            })
                    }

                } else {
                    // false
                    return false;
                }
            });
        },
        // switchOtherChange
        switchOtherChange(e) {
            console.log(e)
            if (e.type == "radio" && e.label == '其它') {
                this.composition[e.name] = '其它'
            }
            this.composition[e.name + 'Extra'] = e.value
        },
        jobChang() {
            if (this.ruleForm.funcListDefault.length > 0) {
                var data = this.ruleForm.funcListDefault[this.ruleForm.funcListDefault.length - 1];
                this.getSkillsList(data);
            } else {
                this.skillsList = [];
                this.skillsSelectedList = [];
                console.log(this.ruleForm.funcListDefault);
            }
        },
        getSkillsList(id) {
            return axiosGet(api.delivery + "GetRelationSkills?PKID=" + id)
                .then((res) => {
                    if (res.code == 1) {
                        let data = res.data

                        for (var i = 0; i < data.length; i++) {
                            data[i].Selected = false
                        }

                        this.skillsList = data
                    }
                })
        },
        addSkillsListTag() {
            // 验证
            //if () {
            //    this.$message('请先选择职能')
            //    return;
            //}
            this.skillsDialogVisible = true
        },
        skillsListTagSelect(item, index) {
            event.preventDefault();
            let target = event.target

            if (this.skillsSelectedList.length >= 5 && !item.Selected) {
                this.$message('最多选择五个技能')
                return;
            }

            for (var i = 0; i < this.skillsSelectedList.length; i++) {
                if (item.PKID == this.skillsSelectedList[i].PKID) {
                    this.skillsSelectedList.splice(i, 1)
                    this.skillsList[index].Selected = false
                    return false
                }
            }
            this.skillsSelectedList.push(item)
            this.skillsList[index].Selected = true
        },

        // 添加企业回调
        saveJobCompany(e) {

            this.$message(e.res.msg)

            if (e.res.code == 1) {

                this.editCompanyDialog = false
                this.getBasic()
            }
        },

    },
    filters: {

    }
})