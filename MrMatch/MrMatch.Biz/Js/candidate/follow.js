var vm = new Vue({
    el: "#appFollow",
    data: {
        userId: null,
        contract: false,
        contractHelp: "",
        form: {
            pageIndex: 1,
            pageSize: 10,
            total:0,
            cities: "",
            salary: "",
            functions: "",
            position: "",
            years: 0,
            folderId:0
        },
        active:null,
        profile: [],
        folders: [],

        userFolders:[],
        followUserDialogVisible: false,

        loading: {
            editFollowLoading:false
        }
    },
    mounted: function () {
        this.folderLoad();
        this.getFolder();

    },
    methods: {
        // 获取all人才收藏数据
        getBasicProfile: function () {
            axiosGet(api.candidate + "GetAllFollowList", this.form)
                .then((res) => {
                    if (res.data) {
                        this.contract = res.data.contract;
                        this.contractHelp = res.data.contractHelp;
                        this.profile = res.data.data.DataList;
                    } else {
                        this.contract = false
                        this.contractHelp = ''
                        this.profile = []
                    }
                })
        },
        // 获取用户收藏夹
        getFolder: function () {
            axiosGet(api.candidate + "GetAccountFolders")
                .then((res) => {
                    this.folders = res.data;

                    this.active = -1
                    for (var x in res.data) {
                        if (res.data[x].PKID == this.form.folderId) {
                            this.active = x++
                        }
                    }
                })
        },
        // 确定加载的收藏夹
        folderLoad: function (e) {
            var folderId = ChkUtil.getQueryString('folder')

            if (folderId == null || folderId == 0) {
                this.form.folderId = 0
                this.active = -1
                this.getBasicProfile();
            } else {
                this.form.folderId = folderId
                this.getFolderProfile()
            }
        },
        // 获取收藏夹人才数据
        getFolderProfile: function () {
            axiosGet(api.candidate + "GetUserFollowList", this.form)
                .then((res) => {
                    if (res.data != null) {
                        this.contract = res.data.contract;
                        this.contractHelp = res.data.contractHelp;
                        this.profile = res.data.data.DataList;
                        this.form.total = res.data.data.Total;
                    } else {
                        this.contract = null;
                        this.contractHelp = null;
                        this.profile = [];
                        this.form.total = 0;
                    }
                })
        },
        // 获取建立的所有收藏夹
        getSingleUserFollow: function (id) {
            axiosGet(api.candidate + "GetSingleUserFollow", { userId:id })
                .then((res) => {
                    this.userFolders = res.data;
                })
        },
        // 编辑简历收藏夹
        editProfile: function () {
            console.log(this.userFolders.join(','))
            if (this.userFolders.length == 0) {
                this.$confirm('不选择收藏夹，将取消收藏该简历', '确认', {
                    distinguishCancelAndClose: true,
                    confirmButtonText: '确认',
                    cancelButtonText: '重新选择'
                })
                    .then(() => {
                        this.loading.editFollowLoading = true
                        axiosGet(api.candidate + "UpdateUserFollow", { userId: this.userId, folderIds: this.userFolders.join(',') })
                            .then((res) => {
                                this.loading.editFollowLoading = false
                                this.$alert(res.msg, '提示').then(() => {
                                    this.followUserDialogVisible = false
                                })
                                if (res.code == 1) this.folderLoad()
                            })
                    })

                return;
            }
            this.loading.editFollowLoading = true
            axiosGet(api.candidate + "UpdateUserFollow", { userId: this.userId , folderIds: this.userFolders.join(',')})
                .then((res) => {
                    this.loading.editFollowLoading = false
                    this.$alert(res.msg, '提示').then(() => {
                        this.followUserDialogVisible = false
                    })
                    if (res.code == 1) this.folderLoad()
                })
        },
        // 从收藏夹移除简历
        delProfile: function (id) {
            this.$confirm('确定从所有收藏夹中删除？', '提示').then(active => {
                axiosGet(api.candidate + "UpdateUserFollow", { userId: id, folderIds: '' })
                    .then((res) => {
                        this.$alert(res.msg, '提示')
                        if (res.code == 1) {
                            this.folderLoad()
                        }
                    })
            })
        },
        // 点击简历收藏夹
        clickUserFollow: function (id) {
            this.getSingleUserFollow(id)
            this.followUserDialogVisible = true
            this.userId = id
        },
        // 切换list
        workListToggle: function (id) {
            var lis = this.$refs['workList' + id][0].getElementsByTagName('li')
            var btn = this.$refs['workListBtn' + id][0].getElementsByTagName('i')[0]
            if (lis[lis.length - 1].className.indexOf('hidden') !== -1) {
                for (var i = 0; i < lis.length; i++) {
                    lis[i].className = ""
                }
            } else {
                for (var i = 0; i < lis.length; i++) {
                    if (i > 1) {
                        lis[i].className = "hidden"
                    }
                }

            }
            if (btn.className.indexOf('fa-angle-double-down') !== -1) {
                btn.className = btn.className.replace("fa-angle-double-down", "fa-angle-double-up")
            } else {
                btn.className = btn.className.replace("fa-angle-double-up", "fa-angle-double-down")
            }
        },
        // 计算是否本周发布
        computePutTime: function (time) {
            var last = Date.parse(time)
            var now = Date.now()
            return now - last <= 60 * 60 * 24 * 7 * 1000
        },
        // 点击分页
        currentChange: function (e) {
            this.form.pageIndex = e
            this.folderLoad()
        }
    },
    filters: {
        monthFormat: function (val) {
            if (val == "至今") {
                return "至今"
            }
            return val.split('-')[0] + "." + val.split("-")[1];
        },
        dateFormat: function (val) {
            var year = val.split("T")[0].split('-')[0];
            var month = val.split("T")[0].split('-')[1].replace(/^0/, "");
            return year + "." + month;
        }
    }
})