var appUserinfo = new Vue({
    el: "#appUserinfo",
    data() {
        return {
            nickName: "",
            avatarUrl: "",
            formData: null,
            form: {
                PKID: 0,
                UserID: 0,
                RealName: "",
                Gender: 0,
                Birthday: "",
                Education: "",
                BirthPlace: "",
                Residence: "",
                CensusRegister: "",
                CurrentCompany: "",
                CurrentPosition: "",
                AnnualSalary: 0,
                JobStatus: 0,
                Introduction: "",
                IsLocked: false,
            },
            rules: {
                RealName: [
                    { required: true, message: '请输入姓名.', trigger: 'blur' },
                ],
                Gender: [
                    { required: true, message: '请选择性别.', trigger: 'blur' },
                ],
                Birthday: [
                    { required: true, message: '请输入生日.', trigger: 'blur' }
                ],
                Education: [
                    { required: true, message: '请选择学历.', trigger: 'blur' }
                ],
                Residence: [
                    { required: true, message: '请输入当前居住地.', trigger: 'blur' }
                ],
                CurrentCompany: [
                    { required: true, message: '请输入当前公司名称.', trigger: 'blur' }
                ],
                CurrentPosition: [
                    { required: true, message: '请输入当前职位.', trigger: 'blur' }
                ],
                JobStatus: [
                    { required: true, message: '请选择当前求职状态.', trigger: 'blur' }
                ]
            },
            Gender: [
                { label: "男", value: 1 },
                { label: "女", value: 2 }
            ],
            Education: [
                { label: "初中", value: 10 },
                { label: "高中", value: 20 },
                { label: "中专", value: 30 },
                { label: "大专", value: 40 },
                { label: "本科", value: 50 },
                { label: "研究生", value: 60 },
                { label: "MBA/EMBA", value: 70 },
                { label: "博士", value: 80 }
            ],
            JobStatus: [
                { label: "在职", value: 1 },
                { label: "离职", value: 2 }
            ],
            isRedact: false,
            isRedactType: 'appUserinfo',
            activeRedactType: '',
            loading: {
                submitLoading: false
            }

        };
    },
    mounted: function () {
        bus.getBasicInfo(this, this.getProfileInfo)
        this.pageBus()
    },
    methods: {
        getProfileInfo: function () {
            axiosGet(api.profile + "GetProfileInfo",
                {
                    userID: this.form.UserID
                })
                .then( (res)=> {
                    if (res.code === "1" && res.data) {
                        var data = res.data;
                        this.form.PKID = data.PKID;
                        this.form.UserID = data.UserID;
                        this.form.RealName = data.RealName;
                        this.form.Gender = data.Gender;
                        this.form.Birthday = data.Birthday.split('T')[0];
                        this.form.Education = data.Education;
                        this.form.BirthPlace = data.BirthPlace;
                        this.form.Residence = data.Residence;
                        this.form.CensusRegister = data.CensusRegister;
                        this.form.CurrentCompany = data.CurrentCompany;
                        this.form.CurrentPosition = data.CurrentPosition;
                        this.form.AnnualSalary = data.AnnualSalary ? data.AnnualSalary:'';
                        this.form.JobStatus = data.JobStatus;
                        this.form.Introduction = data.Introduction;
                        this.form.IsLocked = data.IsLocked;

                        this.formData = data;
                        this.formData.Introduction = fixText(data.Introduction)

                    } else {
                        this.form.UserID = getUrlKey("id");
                    }

                });
        },
        denderChange: function (e) {
            for (var i = 0; i < this.Gender.length; i++) {
                if (this.Gender[i].value == e) {
                    this.form.Gender = this.Gender[i].value

                }
            }
        },
        educationChange: function (e) {
            for (var i = 0; i < this.Education.length; i++) {
                if (this.Education[i].value == e) {
                    this.form.Education = this.Education[i].label
                }
            }
        },
        submitProfile: function (form) {
            this.$refs[form].validate((valid) => { 
                if (valid) {
                    this.loading.submitLoading = true
                    axiosPost(api.profile + "UpdateProfile", this.form)
                        .then( (res)=> {
                            this.loading.submitLoading = false

                            this.$alert(res.msg, '提示');
                            if (res.code == "1") {
                                this.unRedact()
                                this.getProfileInfo();
                                appMyresume.snapUpdateStatus();
                            }
                        })
                        .catch((ex) => {
                            this.loading.submitLoading = false
                            this.$alert(ex.response.data.Message, '提示');
                        });
                } else {
                    return false;
                }
            })
            
        },
        // 编辑
        redact: function () {

            if (bus.lockMessage(this, bus._data.isLocked)) return

            if (this.activeRedactType == '' || this.activeRedactType == this.isRedactType) {
                bus.$emit('on-redact', { isRedact: true, isRedactType: this.isRedactType });
            } else {
                this.$confirm(bus._data.switchRedactText, '提示', {
                    confirmButtonText: '退出',
                    cancelButtonText: '取消',
                }).then( (active)=> {
                    bus.$emit('on-redact', { isRedact: true, isRedactType: this.isRedactType });
                }).catch(() => {
                });
            }
        },
        // 取消编辑
        unRedact: function () {
            bus.$emit('on-redact', { isRedact: false, isRedactType: '' });
        },
        // bus通讯
        pageBus: function () {
            bus.$on('on-redact',  (option) =>{
                this.activeRedactType = option.isRedactType;

                if (option.isRedact && (this.activeRedactType == this.isRedactType || this.activeRedactType == '')) {
                    this.isRedact = true
                } else {
                    this.isRedact = false
                }
            })
        }
    },
    filters: {
        genderFilter: function (val) {
            if (val == 1) {
                return '男';
            } else {
                return '女';
            }
        },
        jobFilter: function (val) {
            if (val == 1) {
                return '在职';
            } else {
                return '离职';
            }
        },
        formatDate: function (val) {
            return val.split('T')[0];
        },
        formatDateDot: function (val) {
            if (!val) return
            return val.split('T')[0].split('-').join('.');
        }
    }
});

function fixText(text) {
    var replaceRegex = /(\n\r|\r\n|\r|\n)/g;

    text = text || '';

    return text.replace(replaceRegex, "<br/>");
}