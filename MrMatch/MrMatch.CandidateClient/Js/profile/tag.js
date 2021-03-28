var appTag = new Vue({
    el: "#appTag",
    data() {
        return {
            nickName: "",
            avatarUrl: "",
            isRedact: false,
            isRedactType: 'appTag',
            activeRedactType: '',
            profileShow: false,
            isLocked: false,
            labelPosition: "top",
            tagList: [], // 所有tag
            tagListData: [],
            optTagList: [], // 选中tag
            form: {
                UserID: 0,
                Tags: ''
            },
            loading: {
                submitLoading: false
            }
        };
    },
    mounted () {
        bus.getBasicInfo(this, function () { })
        this.getTagsList();
        this.pageBus()
    },
    methods: {
        getTagsList () {
            axiosGet(api.profile + "GetTagsList")
                .then((data) => {
                    this.tagList = data.data

                    // 初始化选中状态
                    for (let i = 0; i < this.tagList.length; i++) {
                        for (let j = 0; j < this.tagList[i].ChildrenTag.length; j++) {
                            this.tagList[i].ChildrenTag[j]['isSelect'] = false
                        }
                    }

                    this.getUserTagsList();
                });
        },
        getUserTagsList () {
            axiosGet(api.profile + "GetUserTagsList", { UserID: this.form.UserID })
                .then((res) => {
                    if (res.code == 1 && res.data != null) {
                        this.tagListData = res.data
                        this.optTagList = res.data

                        // 添加选中状态
                        for (let i = 0; i < this.tagList.length; i++) {
                            for (let j = 0; j < this.tagList[i].ChildrenTag.length; j++) {
                                for (let k = 0; k < this.optTagList.length; k++) {
                                    if (this.tagList[i].ChildrenTag[j].Tags === this.optTagList[k].Tags) {
                                        this.tagList[i].ChildrenTag[j]['isSelect'] = true

                                        this.optTagList[k].PKID = this.tagList[i].ChildrenTag[j].PKID

                                    }
                                }
                            }
                        }
                    }
                });
        },
        saveUserTags () {
            this.form.Tags = this.optTagList.map( (e)=> {
                return e.PKID
            }).join(',')

            this.loading.submitLoading = true
            axiosPost(api.profile + "SaveUserTags", this.form)
                .then((data) => {
                    if (data.code == 1) {
                        this.unRedact()
                        appMyresume.snapUpdateStatus();
                    }
                    this.loading.submitLoading = false
                    this.$alert(data.msg, '提示')
                });
        },
        tagClose (item) {
            this.updateTags(item.PKID, true)
        },
        tagClick(item) {
            this.updateTags(item.PKID, item.isSelect)
        },
        updateTags(id, type) {
            if (type) {
                for (var i = 0; i < this.tagList.length; i++) {
                    for (var j = 0; j < this.tagList[i].ChildrenTag.length; j++) {
                        if (id == this.tagList[i].ChildrenTag[j].PKID) {
                            this.tagList[i].ChildrenTag[j]['isSelect'] = false
                        }
                    }
                }
                for (let k = 0; k < this.optTagList.length; k++) {
                    if (id === this.optTagList[k].PKID) {
                        this.optTagList.splice(k,1)
                    }
                        
                }
            } else {
                if (this.optTagList.length >= 10) {
                    this.$alert('最多只能选择10个标签', '提示')
                    return;
                }
                for (var i = 0; i < this.tagList.length; i++) {
                    for (var j = 0; j < this.tagList[i].ChildrenTag.length; j++) {
                        if (id == this.tagList[i].ChildrenTag[j].PKID) {
                            this.tagList[i].ChildrenTag[j]['isSelect'] = true


                            this.optTagList.push({
                                Tags: this.tagList[i].ChildrenTag[j].Tags,
                                PKID: this.tagList[i].ChildrenTag[j].PKID
                            })
                        }
                    }
                }

            }

        },
        redact () {

            if (bus.lockMessage(this, bus._data.isLocked)) return

            if (this.activeRedactType == '' || this.activeRedactType == this.isRedactType) {
                bus.$emit('on-redact', { isRedact: true, isRedactType: this.isRedactType });
            } else {
                this.$confirm(bus._data.switchRedactText, '提示', {
                    confirmButtonText: '退出',
                    cancelButtonText: '取消',
                }).then((active)=> {
                    bus.$emit('on-redact', { isRedact: true, isRedactType: this.isRedactType });
                }).catch(() => {
                });
            }
        },
        unRedact() {
            bus.$emit('on-redact', { isRedact: false, isRedactType: '' });
        },
        pageBus() {
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