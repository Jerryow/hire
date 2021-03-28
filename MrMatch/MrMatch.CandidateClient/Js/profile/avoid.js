var appAvoid = new Vue({
    el: "#appAvoid",
    data() {
        return {
            isRedact: false,
            isRedactType: 'appAvoid',
            activeRedactType: '',
            nickName: "",
            avatarUrl: "",
            profileShow: false,
            isLocked: false,
            labelPosition: "top",
            avoidList: [],
            form: {
                UserID: 0,
                AvoidDomain: ""
            },
            rules: {
                AvoidDomain: [
                    { required: true, message: '企业邮箱后缀不能为空.', trigger: 'blur' }
                ]
            },
            loading: {
                submitLoading: false
            }
        };
    },
    mounted: function () {
        bus.getBasicInfo(this, this.getAvoidsInfo)
        this.pageBus()

    },
    methods: {
        getAvoidsInfo: function () {
            axiosGet(api.profile + "GetAvoidList",
                {
                    userID: this.form.UserID
                })
                .then((res) => {
                    if (res.code == "1") {
                        this.avoidList = res.data;
                    } else {
                        this.$alert(res.msg, '提示');
                    }
                });
        },
        submitForm: function (form) {
            this.$refs[form].validate((valid) => {
                if (valid) {
                    this.loading.submitLoading = true
                    axiosPost(api.profile + "AddAvoid", this.form)
                        .then((res) => {
                            this.loading.submitLoading = false
                            this.$alert(res.msg, '提示');
                            if (res.code == "1") {
                                this.form.AvoidDomain = "";

                                this.unRedact()
                                this.getAvoidsInfo();
                                appMyresume.getProfile();
                                appMyresume.snapUpdateStatus();
                            }
                        })
                        .catch((ex) => {
                            this.loading.submitLoading = false
                            this.$alert(ex.response.data.Message, '提示');
                        });
                } else {
                    return
                }
            })
        },
        redirectProfile: function () {
            var id = getUrlKey("id");
            window.location.href = "/profile/profile?id=" + id;
        },
        removeAvoid: function (val) {
            if (bus.lockMessage(this, bus._data.isLocked)) return
            this.$confirm('确认要删除此条域名信息嘛？', '提示').then(active => {
                axiosGet(api.profile + "DestroyAvoid",
                    {
                        PKID: val
                    })
                    .then((res) => {
                        if (res.code == "1") {
                            this.$message.success(res.msg, '提示');
                            this.getAvoidsInfo();
                            appMyresume.getProfile();
                            appMyresume.snapUpdateStatus();
                        } else {
                            this.$message.error(res.msg, '提示');
                        }
                    })
            })
        },
        redact: function () {
            if (bus.lockMessage(this, bus._data.isLocked)) return

            if (this.activeRedactType == '' || this.activeRedactType == this.isRedactType) {
                bus.$emit('on-redact', { isRedact: true, isRedactType: this.isRedactType });
            } else {
                this.$confirm(bus._data.switchRedactText, '提示', {
                    confirmButtonText: '退出',
                    cancelButtonText: '取消',
                }).then(active => {
                    bus.$emit('on-redact', { isRedact: true, isRedactType: this.isRedactType });
                }).catch(() => {
                });
            }
        },
        unRedact: function () {
            this.isRedact = false
            bus.$emit('on-redact', { isRedact: false, isRedactType: '' });
        },
        pageBus: function () {
            bus.$on('on-redact', (option)=> {
                this.activeRedactType = option.isRedactType;

                if (option.isRedact && (this.activeRedactType == this.isRedactType || this.activeRedactType == '')) {
                    this.isRedact = true
                } else {
                    this.isRedact = false
                }
            })
        }
    }
});