var vm = new Vue({
    el: "#appInterviewItem",
    data: {
        contract: false,
        contractHelp: "",
        interview: {},
        profile: {},
        pkid: "",
        accountName: "",
        phone: "",
        response: [],
        comment: [],
        commentRemark: [],
        invite: {},
        commentOpen: "",
        commentUnOpen: "",
        code: "",
        cid: 0,
        loading: {
            submitLoading: false,
        }
    },
    mounted: function () {
        this.pkid = getUrlKey("id");
        this.getBasicInfo();
    },
    methods: {
        getBasicInfo: function () {
            axiosGet(api.interview + "GetInterviewItem",
                {
                    id: this.pkid
                })
                .then((res) => {
                    this.profile = res.data.data.ProfileInfo;
                    this.code = this.profile.UniCode;
                    this.cid = res.data.cid;
                    this.contract = res.data.contract;
                    this.accountName = res.data.accountName;
                    this.phone = res.data.phone;
                    this.interview = res.data.data;
                    this.response = res.data.response;
                    this.comment = res.data.comment;
                    this.commentRemark = res.data.commentRemark;
                    this.invite = res.data.data.InviteInfo;
                })
        },
        computePutTime(time) {
            var last = Date.parse(time)
            var now = Date.now()
            return now - last <= 60 * 60 * 24 * 7 * 1000
        },
        operations: function (val) {
            axiosGet(api.interview + "InterviewOperations",
                {
                    id: this.interview.PKID,
                    status: val
                })
                .then((res) => {
                    this.getBasicInfo();
                })
        },
        submitComment: function (val) {
            var open = false;
            var com = "";
            if (val == 1) {
                open = true;
                com = this.commentOpen;
            } else {
                open = false;
                com = this.commentUnOpen;
            }
            axiosPost(api.interview + "SubmitComment",
                {
                    InterviewID: this.interview.PKID,
                    IsOpen: open,
                    Comment: com
                })
                .then((res) => {
                    if (res.code=="1") {
                        this.getBasicInfo();
                        this.commentOpen = "";
                        this.commentUnOpen = "";
                    }
                })
        },
        resetCom: function (val) {
            if (val == 1) {
                this.commentOpen = "";
            } else {
                this.commentUnOpen = "";
            }
        },
        exportProfile: function () {
            var _this = this
            var url = api.share + "ExportProfile";

            this.$message('文件正在下载中，请不要重复点击，耐心等待~');
            this.loading.submitLoading = true;
            $.fileDownload(
                url,
                {
                    data: {
                        "uuid": this.code,
                        "cid": this.cid
                    },
                    successCallback: function (url) {
                        _this.$message({
                            message: '导出成功',
                            type: 'success'
                        });
                        _this.loading.submitLoading = false;
                    },
                    failCallback: function (html, url) {
                        _this.$message({
                            message: '导出失败',
                            type: 'error'
                        });
                        _this.loading.submitLoading = false;
                    }
                });
        },
    },
    filters: {
        status: function (val) {
            var sta = [
                { id: 100, value: "已拒绝" },
                { id: 110, value: "已回绝" },
                { id: 200, value: "待回应" },
                { id: 210, value: "已沟通" },
                { id: 220, value: "已面试" },
                { id: 230, value: "已录用" }
            ];
            for (var i = 0; i < sta.length; i++) {
                if (val == sta[i].id) {
                    return sta[i].value;
                }
            }
        },
        dateFormat: function (val) {
            return val.replace("T"," ").split(".")[0];
        },
        monthFormat: function (val) {
            if (val == "至今") {
                return "至今"
            }
            return val.split('-')[0] + "." + val.split("-")[1];
        },
        dateFormat1: function (val) {
            var year = val.split("T")[0].split('-')[0];
            var month = val.split("T")[0].split('-')[1].replace(/^0/, "");
            return year + "." + month;
        }
    }
})