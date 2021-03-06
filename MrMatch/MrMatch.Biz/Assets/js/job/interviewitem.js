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
            submitLoading: false
        }
    },
    mounted: function () {
        this.pkid = getUrlKey("id");
        this.getBasicInfo();
    },
    methods: {
        getBasicInfo: function () {
            axiosGet(api.interview + "GetInterviewItem", {
                id: this.pkid
            }).then(res => {
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
            });
        },
        computePutTime(time) {
            var last = Date.parse(time);
            var now = Date.now();
            return now - last <= 60 * 60 * 24 * 7 * 1000;
        },
        operations: function (val) {
            axiosGet(api.interview + "InterviewOperations", {
                id: this.interview.PKID,
                status: val
            }).then(res => {
                this.getBasicInfo();
            });
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
            axiosPost(api.interview + "SubmitComment", {
                InterviewID: this.interview.PKID,
                IsOpen: open,
                Comment: com
            }).then(res => {
                if (res.code == "1") {
                    this.getBasicInfo();
                    this.commentOpen = "";
                    this.commentUnOpen = "";
                }
            });
        },
        resetCom: function (val) {
            if (val == 1) {
                this.commentOpen = "";
            } else {
                this.commentUnOpen = "";
            }
        },
        exportProfile: function () {
            var _this = this;
            var url = api.share + "ExportProfile";

            this.$message('????????????????????????????????????????????????????????????~');
            this.loading.submitLoading = true;
            $.fileDownload(url, {
                data: {
                    "uuid": this.code,
                    "cid": this.cid
                },
                successCallback: function (url) {
                    _this.$message({
                        message: '????????????',
                        type: 'success'
                    });
                    _this.loading.submitLoading = false;
                },
                failCallback: function (html, url) {
                    _this.$message({
                        message: '????????????',
                        type: 'error'
                    });
                    _this.loading.submitLoading = false;
                }
            });
        }
    },
    filters: {
        status: function (val) {
            var sta = [{ id: 100, value: "?????????" }, { id: 110, value: "?????????" }, { id: 200, value: "?????????" }, { id: 210, value: "?????????" }, { id: 220, value: "?????????" }, { id: 230, value: "?????????" }];
            for (var i = 0; i < sta.length; i++) {
                if (val == sta[i].id) {
                    return sta[i].value;
                }
            }
        },
        dateFormat: function (val) {
            return val.replace("T", " ").split(".")[0];
        },
        monthFormat: function (val) {
            if (val == "??????") {
                return "??????";
            }
            return val.split('-')[0] + "." + val.split("-")[1];
        },
        dateFormat1: function (val) {
            var year = val.split("T")[0].split('-')[0];
            var month = val.split("T")[0].split('-')[1].replace(/^0/, "");
            return year + "." + month;
        }
    }
});