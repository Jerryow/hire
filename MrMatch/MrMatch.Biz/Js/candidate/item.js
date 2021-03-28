var vm = new Vue({
    el: "#appCandidateItem",
    data: {
        contract: false,
        contractHelp: "",
        cid:0,
        profile: {},
        code: "",
        helpDialogVisible: false,
        shareDialogVisible: false,
        domain:"",
        shareForm: {
            UniCode: "",
            Email: "",
            Intruduce:""
        }
    },
    mounted: function () {
        this.code = getUrlKey("code");
        this.getProfile();
    },
    methods: {
        getProfile: function () {
            axiosGet(api.candidate + "GetProfileInfo",
                {
                    guid: this.code
                })
                .then((res) => {
                    this.contract = res.data.contract;
                    this.contractHelp = res.data.contractHelp;
                    this.cid = res.data.cid;
                    this.domain = res.data.domain;
                    this.profile = res.data.data;
                })
        },
        computePutTime(time) {
            var last = Date.parse(time)
            var now = Date.now()
            return now - last <= 60 * 60 * 24 * 7 * 1000
        },
        sendInviteLetter: function (id) {
            axiosGet(api.invite + "SendInviteLetter",
                {
                    userID: id
                })
                .then((res) => {
                    if (res.code == 1) {
                        this.$alert(res.msg, '提示')
                    } else {
                        this.$alert(res.msg, '提示')
                    }
                })
        },
        exportProfile: function () {
            var _this = this
            var code = getUrlKey("code");
            var url = api.share + "ExportProfile";

            this.$message( '文件正在下载中，请不要重复点击，耐心等待~');

            $.fileDownload(
                url,
                {
                    data: {
                        "uuid": code,
                        "cid":this.cid
                    },
                    successCallback: function (url) {

                        _this.$message({
                            message: '导出成功',
                            type:'success'
                        });
                    },
                    failCallback: function (html, url) {
                        _this.$message({
                            message: '导出失败',
                            type: 'error'
                        });
                    }
                });
        },
        shareProfile: function () {
            var code = getUrlKey("code");
            this.shareForm.UniCode = code;
            axiosPost(api.share + "ShareProfile", this.shareForm)
                .then((res) => {
                    if (res.code == "1") {
                        this.$alert(res.msg, '提示');
                        this.shareForm.Intruduce = "";
                        this.shareForm.Email = "";
                        this.shareDialogVisible = false;
                    } else {
                        this.$alert(res.msg, '提示');
                    }
                })
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