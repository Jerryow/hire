var appEducation = new Vue({
    el: "#appEducation",
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
            isRedactType: 'appEducation',
            isRedactItemType: 'appEducationItem',
            activeRedactType: '',
            nickName: "",
            avatarUrl: "",
            profileShow: false,
            isLocked: false,
            labelPosition: "top",
            newExpirationDate: false,
            editExpirationDate: false,
            educationList: [],
            form: {
                UserID: 0,
                SchoolName: "",
                MajorSubject: "",
                StartDate: "",
                ExpirationDate: "",
                Degree: "",
                EducationType: ""
            },
            editForm: {
                UserID: 0,
                SchoolName: "",
                MajorSubject: "",
                StartDate: "",
                ExpirationDate: "",
                Degree: "",
                EducationType: ""
            },
            rules: {
                SchoolName: [
                    { required: true, message: '学校名称不能为空.', trigger: 'blur' },
                    { validator: checkNull, trigger: 'blur' }
                ],
                MajorSubject: [
                    { required: true, message: '专业不能为空.', trigger: 'blur' },
                    { validator: checkNull, trigger: 'blur' }
                ],
                StartDate: [
                    { required: true, message: '开始时间不能为空.', trigger: 'blur' }
                ],
                ExpirationDate: [
                    { required: true, message: '结束时间不能为空.', trigger: 'blur' }
                ],
                EducationType: [
                    { required: true, message: '学历不能为空.', trigger: 'blur' }
                ]
            },
            educationType: [
                { label: "初中", value: 10 },
                { label: "高中", value: 20 },
                { label: "中专", value: 30 },
                { label: "大专", value: 40 },
                { label: "本科", value: 50 },
                { label: "研究生", value: 60 },
                { label: "MBA/EMBA", value: 70 },
                { label: "博士", value: 80 }
            ],
            dialogEditEducation: {
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
        bus.getBasicInfo(this, this.getEducationsInfo)
        this.pageBus()
    },
    methods: {
        degreeChange: function (e,type) {
            for (var i = 0; i < this.educationType.length; i++) {
                if (this.educationType[i].value == e) {
                    if (type === 'editForm') {
                        this.editForm.Degree = this.educationType[i].value
                    } else {
                        this.form.Degree = this.educationType[i].value
                    }
                }
            }
        },
        getEducationsInfo: function () {
            axiosGet(api.profile + "GetEducationList",
                {
                    userID: this.form.UserID
                })
                .then((res) => {
                    if (res.code == "1" && res.data) {
                        for (var i = 0; i < res.data.length; i++) {
                            res.data[i].isRedact = false

                            for (var x = 0; x < this.educationType.length; x++) {
                                if (this.educationType[x].value == res.data[i].Degree) {
                                    res.data[i].EducationType = this.educationType[x].label
                                }
                            }
                        }
                        this.educationList = res.data;


                    } else {
                        this.educationList = []
                    }
                });
        },
        submitForm: function (form) {
            if (this.newExpirationDate) this.form.ExpirationDate = '至今'
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
                    axiosPost(api.profile + "AddEducation", this.form)
                        .then((res) => {
                            this.loading.submitLoading = false
                            if (res.code == "1") {
                                this.$alert(res.msg, '提示');
                                this.form.SchoolName = "";
                                this.form.MajorSubject = "";
                                this.form.StartDate = "";
                                this.form.ExpirationDate = "";
                                this.form.Degree = "";
                                this.form.EducationType = "";
                                this.newExpirationDate = false;

                                this.unRedact();
                                this.getEducationsInfo();
                                appMyresume.getProfile();
                                appMyresume.snapUpdateStatus();
                            } else {
                                this.$alert(res.msg, '提示');
                            }
                        })
                        .catch((ex) => {
                            this.loading.submitLoading = false;
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
        handleClose: function () {
            this.dialogEditEducation.dialogVisible = false
            this.editExpirationDate = false
        },
        // 修改教育经历
        updateForm: function (form, i) {
            console.log(this.$refs[form])
            this.$refs[form][i].validate((valid) => {
                if (valid) {
                    this.loading.saveLoading = true
                    if (this.editExpirationDate) {
                        this.editForm.ExpirationDate = '至今'
                    } else {
                        this.editForm.ExpirationDate = this.editForm.ExpirationDate.split('-').map(function (e) {
                            return e.toString().replace(/\b(0+)/gi, "")
                        }).join('-')
                    }
                    axiosPost(api.profile + "UpdateEducation", this.editForm)
                        .then((res) => {
                            this.loading.saveLoading = false
                            this.$alert(res.msg, '提示');
                            if (res.code == "1") {
                                this.editExpirationDate = false

                                this.unRedactItem(i)
                                this.getEducationsInfo()
                            }
                        })
                        .catch((ex) => {
                            this.loading.saveLoading = false
                            this.$alert(ex.response.data.Message, '提示');
                        });
                } else {
                    return false;
                }
            });
        },
        removeEducation: function (val) {
            if (bus.lockMessage(this, bus._data.isLocked)) return
            this.$confirm('确定要删除此条教育经历？', '提示').then(active => {
                axiosGet(api.profile + "DestroyEducation",
                    {
                        PKID: val
                    })
                    .then((res) => {
                        if (res.code == "1") {
                            this.$message.success(res.msg);
                            this.getEducationsInfo();
                            appMyresume.getProfile();
                        } else {
                            this.$message.error(res.msg);
                        }
                    })
            })
        },
        redactItem: function (item, index) {

            if (bus.lockMessage(this, bus._data.isLocked)) return

            this.editForm.PKID = item.PKID
            this.editForm.SchoolName = item.SchoolName
            this.editForm.MajorSubject = item.MajorSubject
            this.editForm.StartDate = item.StartDate
            this.editForm.Degree = item.Degree
            this.editForm.EducationType = item.EducationType

            if (item.ExpirationDate == '至今') {
                this.editExpirationDate = true
            } else {
                this.editForm.ExpirationDate = item.ExpirationDate

            }

            this.isRedactItemType = this.isRedactItemType+index

            if (this.activeRedactType == '' || this.activeRedactType == this.isRedactItemType) {
                bus.$emit('on-redact', { isRedact: true, isRedactType: this.isRedactItemType, itemIndex: index});
            } else {
                this.$confirm(bus._data.switchRedactText, '提示', {
                    confirmButtonText: '退出',
                    cancelButtonText: '取消',
                }).then( (active) => {
                    bus.$emit('on-redact', { isRedact: true, isRedactType: this.isRedactItemType, itemIndex: index });
                }).catch(() => {
                });
            }
        },
        unRedactItem: function (item,index) {
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
                }).then( (active)=> {
                    bus.$emit('on-redact', { isRedact: true, isRedactType: this.isRedactType });
                }).catch(() => {
                });
            }
        },
        unRedact: function () {
            bus.$emit('on-redact', { isRedact: false, isRedactType: '' });
        },
        pageBus: function () {
            bus.$on('on-redact', (option)=> {
                this.activeRedactType = option.isRedactType;

                for (var i = 0; i < this.educationList.length; i++) {
                    this.educationList[i].isRedact = false
                }

                if (option.itemIndex || option.itemIndex === 0) {
                    if (option.isRedact && (this.activeRedactType == this.isRedactItemType || this.activeRedactType == '')) {
                        this.educationList[option.itemIndex].isRedact = true
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
        formatDate: function (val) {
            return ChkUtil.dateToFormat(val.split('T')[0]);
        }
    }
});