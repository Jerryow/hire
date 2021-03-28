var appWork = new Vue({
    el: "#appWork",
    data() {
        var checkNull = (rule, value, callback) => {
            if (value.trim() == "") {
                callback(new Error('请输入正确格式的值'));
            } else {
                callback();
            }
        };
        return {
            isRedact: false,
            isRedactType: 'appWork',
            isRedactItemType: 'appWorkItem', 
            activeRedactType: '', 
            nickName: "",
            avatarUrl: "",
            profileShow: false,
            isLocked: false,
            labelPosition: "top",
            workList: [], 
            newExpirationDate: false,
            editExpirationDate: false,
            form: {
                UserID: 0,
                CompanyName: "",
                Position: "",
                StartDate: "",
                ExpirationDate: "",
                Introduction: ""
            },
            editForm: {
                UserID: 0,
                PKID: 0,
                CompanyName: "",
                Position: "",
                StartDate: "",
                ExpirationDate: "",
                Introduction: ""
            },
            rules: {
                CompanyName: [
                    { required: true, message: '公司名称不能为空.', trigger: 'blur' },
                    { validator: checkNull, trigger: 'blur' }
                ],
                Position: [
                    { required: true, message: '头衔不能为空.', trigger: 'blur' },
                    { validator: checkNull, trigger: 'blur' }
                ],
                StartDate: [
                    { required: true, message: '开始时间不能为空.', trigger: 'blur' }
                ],
                ExpirationDate: [
                    { required: true, message: '结束时间不能为空.', trigger: 'blur' }
                ]
            },
            dialogEditWork: {
                index: 0,
                title: "修改简历",
                dialogVisible: false,
                diaClose: false,
                labelPosition: "top"
            },
            loading: {
                submitLoading1: false,
                saveLoading2: false
            }

        };
    },
    watch: {
        newExpirationDate: function (news, old) {
            this.form.ExpirationDate = news ? '至今' : ''
        },
        editExpirationDate: function (news, old) {
            this.editForm.ExpirationDate = news ? '至今' : ''
        }
    },
    mounted: function () {
        bus.getBasicInfo(this, this.getWorksInfo)
        this.pageBus()

    },
    methods: {
        getWorksInfo: function () {
            axiosGet(api.profile + "GetWorkExperienceList",
                {
                    userID: this.form.UserID
                })
                .then((res) => {
                    if (res.code == "1" && res.data) {
                        for (var i = 0; i < res.data.length; i++) {
                            res.data[i]._Introduction = fixText(res.data[i].Introduction) 
                            res.data[i].isRedact = false
                        }
                        this.workList = res.data;
                    } else {
                        this.workList = []
                    }
                });
        },
        submitForm: function (form) {
            this.$refs[form].validate((valid) => {
                if (valid) {
                    this.loading.submitLoading = true
                    if (this.newExpirationDate) {
                        this.form.ExpirationDate = '至今'
                    } else if (this.form.ExpirationDate) {
                        this.form.ExpirationDate = this.form.ExpirationDate.split('-').map(function (e) {
                            return e.toString().replace(/\b(0+)/gi, "")
                        }).join('-')
                    }
                    axiosPost(api.profile + "AddWorkExperience", this.form)
                        .then((res) => {
                            this.loading.submitLoading = false
                            if (res.code == "1") {
                                this.$alert(res.msg, '提示');
                                this.form.CompanyName = "";
                                this.form.Position = "";
                                this.form.StartDate = "";
                                this.form.ExpirationDate = "";
                                this.form.Introduction = "";
                                this.newExpirationDate = false

                                this.unRedact()
                                this.getWorksInfo();
                                appMyresume.getProfile();
                                appMyresume.snapUpdateStatus();
                            } else {
                                this.$alert(res.msg, '提示');
                            }
                        })
                        .catch((ex) => {
                            this.loading.submitLoading = false
                            this.$alert(ex.response.data.Message, '提示');
                        });
                } else {
                    return false;
                }
            });
        },
        redirectProfile: function () {
            var id = getUrlKey("id");
            window.location.href = "/profile/profile?id=" + id;
        },
        // 修改工作经历
        updateForm: function (form, i) {
            this.$refs[form][i].validate((valid) => {
                if (valid) {
                    this.loading.submitLoading = true
                    if (this.editExpirationDate) {
                        this.editForm.ExpirationDate = '至今'
                    } else {
                        this.editForm.ExpirationDate = this.editForm.ExpirationDate.split('-').map(function (e) {
                            return e.toString().replace(/\b(0+)/gi, "")
                        }).join('-')
                    }
                    axiosPost(api.profile + "UpdateWorkExperience", this.editForm)
                        .then((res) => {
                            this.loading.submitLoading = false
                            this.$alert(res.msg, '提示'); 
                            if (res.code == "1") {
                                this.editExpirationDate = false

                                this.unRedactItem(i)
                                this.getWorksInfo()
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
            });
        },
        removeWork: function (val) {
            if (bus.lockMessage(this, bus._data.isLocked)) return
            this.$confirm('确定要删除此条工作经历？', '提示').then(active => {
                axiosGet(api.profile + "DestroyWorkExperience",
                    {
                        PKID: val
                    })
                    .then((res) => {

                        if (res.code == "1") {
                            this.$message.success(res.msg, '提示');
                            this.getWorksInfo();
                            appMyresume.getProfile();
                            appMyresume.snapUpdateStatus();
                        } else {
                            this.$message.error(res.msg, '提示');
                        }
                    })
            }).catch(() => {
            });
        },
        redactItem: function (item, index) {
            console.log(item)
            if (bus.lockMessage(this, bus._data.isLocked)) return

            this.editForm.PKID = item.PKID
            this.editForm.CompanyName = item.CompanyName
            this.editForm.Position = item.Position
            this.editForm.StartDate = item.StartDate
            this.editForm.Introduction = item.Introduction

            if (item.ExpirationDate == '至今') {
                this.editExpirationDate = true
            } else {
                this.editForm.ExpirationDate = item.ExpirationDate

            }

            this.isRedactItemType = this.isRedactItemType + index

            if (this.activeRedactType == '' || this.activeRedactType == this.isRedactItemType) {
                bus.$emit('on-redact', { isRedact: true, isRedactType: this.isRedactItemType, itemIndex: index });
            } else {
                this.$confirm(bus._data.switchRedactText, '提示', {
                    confirmButtonText: '退出',
                    cancelButtonText: '取消',
                }).then( (active)=> {
                    bus.$emit('on-redact', { isRedact: true, isRedactType: this.isRedactItemType, itemIndex: index });
                }).catch(() => {
                });
            }
        },
        unRedactItem: function (index) {
            bus.$emit('on-redact', { isRedact: false, isRedactType: '', itemIndex: index });
        },
        redact: function () {
            if (bus.lockMessage(this, bus._data.isLocked)) return

            if (this.activeRedactType == '' || this.activeRedactType == this.isRedactType) {
                bus.$emit('on-redact', { isRedact: true, isRedactType: this.isRedactType });
            } else {
                this.$confirm(bus._data.switchRedactText, '提示', {
                    confirmButtonText: '退出',
                    cancelButtonText: '取消',
                }).then((active)=>{
                    bus.$emit('on-redact', { isRedact: true, isRedactType: this.isRedactType });
                }).catch(() => {
                });
            }
        },
        unRedact: function () {
            bus.$emit('on-redact', { isRedact: false, isRedactType: '' });
        },
        pageBus: function () {
            bus.$on('on-redact',  (option)=>{
                this.activeRedactType = option.isRedactType;

                for (var i = 0; i < this.workList.length; i++) {
                    this.workList[i].isRedact = false
                }

                if (option.itemIndex || option.itemIndex === 0) {
                    if (option.isRedact && (this.activeRedactType == this.isRedactItemType || this.activeRedactType == '')) {
                        this.workList[option.itemIndex].isRedact = true
                    }
                } else {
                    if (option.isRedact && (this.activeRedactType == this.isRedactType || this.activeRedactType == '')) {
                        this.isRedact = true
                    } else {
                        this.isRedact = false
                    }

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
            return ChkUtil.dateToFormat(val.split('T')[0]);
        }
    }
});


function fixText(text) {
    var replaceRegex = /(\n\r|\r\n|\r|\n)/g;

    text = text || '';

    return text.replace(replaceRegex, "<br/>");
}