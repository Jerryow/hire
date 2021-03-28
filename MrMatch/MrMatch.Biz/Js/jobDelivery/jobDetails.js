
var vm = new Vue({
    el: "#appJobDetails",
    data() {
        return {
            pagenation: {
                pageIndex: 1,
                pageSize: 10,
                total: 0
            },
            basicData: {
                agent: false,
                basicID: 0,
                agentCompany: [],
                basicName: '',
                basicShortName: '',
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
            skillsList: [],
            profileForm: {
            },
            detailsForm: {
                PKID:0,
                skillsSelected:''
            },
            activeName:'profile',
            list: [],
            loading: false
        }
    },
    watch: {
    },
    mounted: function () {
        // profile 简历
        // details 职位信息

        var ajax1 = this.getBasic();
        var ajax2 = this.getBasicFunc();
        var ajax3 = this.getBasicDist();

        if (ChkUtil.getQueryString('id')) {
            this.detailsForm.PKID = ChkUtil.getQueryString('id')
            Promise.all([ajax1, ajax2, ajax3]).then((result) => {
                this.getProfileList()
                this.getJobDetails()
            });
        }

    },
    methods: {
        getBasic: function () {
            return axiosGet(api.delivery + "GetBasicInfo")
                .then((res) => {
                    this.basicData.agent = res.data.type;
                    this.basicData.basicID = res.data.basicId;
                    this.basicData.basicName = res.data.basicCom;
                    this.basicData.basicShortName = res.data.basicShortName;
                    this.basicData.agentCompany = res.data.agentCom;
                    this.basicData.accountID = res.data.accountID;
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
        getProfileList() {
            let params = {
                pageIndex: this.pagenation.pageIndex,
                pageSize: this.pagenation.pageSize,
                PKID: ChkUtil.getQueryString('id')
            }
            axiosGet(api.delivery + "GetJobProfilesPagenation", params)
                .then((res) => {
                    if (res.code == 1) {
                        this.profileForm = res.data
                        this.profileForm.PKID = ChkUtil.getQueryString('id');
                        this.list = res.data.profiles.DataList ? res.data.profiles.DataList : []
                        this.pagenation.total = res.data.profiles.Total
                    }
                })
        },
        getJobDetails() {
            let params = {
                PKID: ChkUtil.getQueryString('id'),
                accountID: this.basicData.accountID
            }
            axiosGet(api.delivery + "GetJobInfo", params)
                .then((res) => {
                    if (res.code == 1) {
                        //this.detailsForm = res.data.jobData
                        var data = res.data.jobData

                        //this.selNameChange(data.JobCompanyID)
                        if (data.AgentJob) {
                            this.detailsForm.JobCompanyName = data.CompanyName;
                            this.detailsForm.agentShortName = data.CompanyShortName;
                        } else {
                            this.detailsForm.JobCompanyName = this.basicData.basicName;
                            this.detailsForm.agentShortName = this.basicData.basicShortName
                        }

                        this.detailsForm.JobName = data.JobName

                        var skillAjax = this.funcListDefaultInit(data.FunctionID)
                        this.distListDefaultInit(data.DistrictID)

                        this.detailsForm.Reporter = data.Reporter
                        this.detailsForm.SubordinateCount = data.SubordinateCount

                        this.detailsForm.salaryOff = data.MinAnnualSalary
                        this.detailsForm.salaryOn = data.MaxAnnualSalary
                        this.detailsForm.salaryOpen = data.SalaryOpen

                        this.detailsForm.salaryComposition = data.SalaryComposition
                        this.detailsForm.salaryCompositionExtra = data.SalaryCompositionExtra
                        this.detailsForm.socialSecurity = data.SocialSecurity
                        this.detailsForm.socialSecurityExtra = data.SocialSecurityExtra
                        this.detailsForm.liveWelfare = data.LiveWelfare
                        this.detailsForm.liveWelfareExtra = data.LiveWelfareExtra
                        this.detailsForm.annualLeave = data.AnnualLeave
                        this.detailsForm.annualLeaveExtra = data.AnnualLeaveExtra
                        this.detailsForm.subsidy = data.Subsidy
                        this.detailsForm.subsidyExtra = data.SubsidyExtra

                        this.detailsForm.ageOff = data.AgeRequirements ? data.AgeRequirements.split(',')[0] : ''
                        this.detailsForm.ageOn = data.AgeRequirements ? data.AgeRequirements.split(',')[1] : ''
                        this.detailsForm.MajorRequirements = data.MajorRequirements
                        this.detailsForm.WorkYears = data.WorkYears
                        this.detailsForm.Degree = data.Degree
                        this.detailsForm.FullTime = data.FullTime
                        this.detailsForm.Language = data.Language
                        this.detailsForm.LanguageExtra = data.LanguageExtra

                        this.detailsForm.skillsSelected = ''

                        Promise.all([skillAjax]).then((result) => {
                            var skillsArr = data.Skills.length > 0 ? data.Skills.split(',') : []
                            var skillsSelected = []

                            for (let i = 0; i < this.skillsList.length; i++) {
                                for (let j = 0; j < skillsArr.length; j++) {
                                    if (this.skillsList[i].SkillName == skillsArr[j]) {
                                       skillsSelected.push(skillsArr[j])
                                    }
                                }
                            }

                            this.detailsForm.skillsSelected = skillsSelected.join(',')
                            console.log(this.detailsForm.skillsSelected)
                        });

                        this.detailsForm.SkillsExtra = data.SkillsExtra

                        this.detailsForm.JobDescription = data.JobDescription
                        this.detailsForm.JobRequirements = data.JobRequirements
                        this.detailsForm.ExtraInfo = data.ExtraInfo

                        this.detailsForm.ActiveStatus = data.ActiveStatus
                        //this.detailsForm.salaryOff 

                    }
                })
        },
        currentChange(e) {
            this.pagenation.pageIndex = e
            this.getProfileList()
        },
        handleClick(tab, event) {
        },

        distListDefaultInit(val) {
            for (var i = 0; i < this.basicData.districtList.length; i++) {
                if (this.basicData.districtList[i].children) {
                    for (var j = 0; j < this.basicData.districtList[i].children.length; j++) {
                        if (val == this.basicData.districtList[i].children[j].value) {
                            this.detailsForm.distListName = this.basicData.districtList[i].label + '-' + this.basicData.districtList[i].children[j].label
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
                        this.detailsForm.funcListName = this.basicData.funcList[i].label + ' / ' + this.basicData.funcList[i].children[j].label

                        return this.getSkillsList(this.basicData.funcList[i].children[j].value);
                    }
                    if (this.basicData.funcList[i].children[j].children) {
                        for (var k = 0; k < this.basicData.funcList[i].children[j].children.length; k++) {
                            if (val == this.basicData.funcList[i].children[j].children[k].value) {
                                this.detailsForm.funcListName = this.basicData.funcList[i].label + ' / ' + this.basicData.funcList[i].children[j].label + ' / ' + this.basicData.funcList[i].children[j].children[k].label

                                return this.getSkillsList(this.basicData.funcList[i].children[j].children[k].value);
                            }
                        }
                    }

                }
            }
        },
        getSkillsList(id) {
            return axiosGet(api.delivery + "GetRelationSkills?PKID=" + id)
                .then((res) => {
                    if (res.code == 1) {
                        let data = res.data
                        this.skillsList = data
                    }
                })
        },
    },
    filters: {
        DateFilter(date) {
            return new Date(date).format("yyyy-MM-dd hh:mm:ss")
        },
        ValueValidFilter(val) {
            return (val && val.length > 0) ? val : '--'
        },
        ValidateGender: function (val) {
            if (val == 1) {
                return "男";
            } else {
                return "女";
            }
        }
    }
})

