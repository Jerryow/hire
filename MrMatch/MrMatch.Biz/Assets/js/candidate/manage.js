var vm = new Vue({
    el: "#appManageFollow",
    data: {
        contract: false,
        contractHelp: "",
        form: {
            pageIndex: 1,
            pageSize: 10,
            cities: "",
            salary: "",
            functions: "",
            position: "",
            years: 0,
            folderId: 0
        },
        folders: [],
        active: null,
        newFolderName: null,
        selectFolders: null,
        editFolderName: null,
        editFollowDialogVisible: false
    },
    mounted: function () {
        this.getFolder();
    },
    methods: {
        // 获取用户收藏夹
        getFolder: function () {
            axiosGet(api.candidate + "GetAccountFolders").then(res => {
                this.folders = res.data;

                this.active = -1;
                for (var x in res.data) {
                    if (res.data[x].PKID == this.form.folderId) {
                        this.active = x++;
                    }
                }
            });
        },
        // 新建收藏夹
        saveUserFolder: function () {
            if (!this.newFolderName || this.newFolderName == '') {

                this.$alert('请输入要新增的收藏夹名称', '提示');
                return;
            }
            this.$confirm('确定新增？', '提示').then(() => {
                axiosPost(api.candidate + "SaveUserFolder", {
                    PKID: 0,
                    FolderName: this.newFolderName
                }).then(res => {
                    if (res.code == 1) {
                        this.getFolder();
                    }

                    this.$alert(res.msg, '提示');
                });
            });
        },
        // 修改收藏夹
        clickEditFolder: function () {
            if (this.selectFolders == null) {
                this.$alert('请选择要修改的收藏夹', '提示');
            } else {
                this.editFollowDialogVisible = true;
            }
        },
        editFolder() {
            if (this.editFolderName == null || this.editFolderName == '') {
                this.$alert('新的收藏夹名称不能为空', '提示');

                return;
            }
            axiosPost(api.candidate + "SaveUserFolder", {
                PKID: this.selectFolders,
                FolderName: this.editFolderName
            }).then(res => {
                if (res.code == 1) {
                    this.selectFolders = null;
                    this.editFolderName = null;
                    this.editFollowDialogVisible = false;
                    this.getFolder();
                }

                this.$alert(res.msg, '提示');
            });
        },
        // 删除收藏夹
        delFolder: function () {
            if (this.selectFolders == null) {
                this.$alert('请选择要删除的收藏夹', '提示');
            } else {
                this.$confirm('确定删除？', '提示').then(() => {
                    axiosGet(api.candidate + "DestroyFolder", {
                        PKID: this.selectFolders
                    }).then(res => {
                        if (res.code == 1) {
                            this.selectFolders = null;
                            this.getFolder();
                        }

                        this.$alert(res.msg, '提示');
                    });
                });
            }
        }
    }
});