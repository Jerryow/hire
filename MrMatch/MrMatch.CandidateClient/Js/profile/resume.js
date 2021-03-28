var appMyresume = new Vue({
    el: "#appMyresume",
    data() {
        return {
            userId: 0,
            nickName: "",
            avatarUrl: "",
            isRedact: false,
            progress: 0,
            profileShow: false,
            educationShow: false,
            workExShow: false,
            tagsShow: false,
            jobIntentionShow: false,
            avoidShow: false,
            profile: {},
            snapUpdate: true,
            loading: {
                submitLoading: false
            }
        };
    },
    mounted: function () {
        bus.getBasicInfo(this, () => {
            this.getProfile()
        })

        bus.$on('on-redact', (option) => {
            this.isRedact = option.isRedact
        })

        this.snapUpdateStatus()

        window.addEventListener('scroll', this.handleScroll)
    },
    methods: {
        // 获取所有简历信息
        getProfile () {
            axiosGet(api.profile + "GetAllProfile",{
                    userID: this.userId
                }).then((res) => {
                    if (res.code == "1") {
                        bus._data.isLocked = res.data.ApproveStatus == 1 ? true : false

                        this.progress = 0

                        // 基本信息
                        if (res.data.ProfileInfo !== null) {
                            this.profileShow = true;
                            this.progress += 25
                        } else {
                            this.profileShow = false;
                        }
                        // 求职意向
                        if (res.data.JobIntention !== null) {
                            this.jobIntentionShow = true;
                            this.progress += 25
                        } else {
                            this.jobIntentionShow = false;
                        }
                        // 工作
                        if (res.data.WorkExList !== null) {
                            this.workExShow = true;
                            this.progress += 25
                        } else {
                            this.workExShow = false;
                        }
                        // 学习
                        if (res.data.EducationList !== null) {
                            this.educationShow = true;
                            this.progress += 25
                        } else {
                            this.educationShow = false;
                        }
                        // 标签
                        if (res.data.UserTagsList !== null) {
                            this.tagsShow = true;
                        } else {
                            this.tagsShow = false;
                        }
                        // 屏蔽
                        if (res.data.AvoidList !== null) {
                            this.avoidShow = true;
                        } else {
                            this.avoidShow = false;
                        }

                        this.profile = res.data

                    }
                    else {
                        this.$alert(res.msg, '提示');
                    }
                });
        },
        // 获取简历快照修改状态
        snapUpdateStatus() {
            axiosGet(api.profile + "SnapUpdateStatus")
                .then((res) => {
                    if (res.code == 1) {
                        if (res.data.Profile || res.data.Education || res.data.WorkEx || res.data.JobIntention) {
                            this.snapUpdate = true
                        } else {
                            this.snapUpdate = false
                        }
                    }
                })
        },
        // 提交审核
        bindToApprove: function () {
            this.loading.submitLoading = true
            axiosGet(api.profile + "CommitApprove")
                .then((res) => {
                    this.loading.submitLoading = false
                    if (res.code == 1) {
                        this.getProfile();
                        this.$message({
                            message: res.msg,
                            type: 'success'
                        });
                    } else {
                        this.$alert(res.msg, '提示')
                    }
                })
        },
        // 上下架
        bindToActive: function (type) {
            this.loading.submitLoading = true
            axiosGet(api.profile + "UpdateUserActivity",{
                status:type
            }).then((res) => {
                this.loading.submitLoading = false
                if (res.code == 1) {
                    this.getProfile();
                    this.$message({
                        message: res.msg,
                        type: 'success'
                    });
                } else {
                    this.$alert(res.msg, '提示')
                }
            })
        }

    }
});