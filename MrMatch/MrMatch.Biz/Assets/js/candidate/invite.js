var vm = new Vue({
    el: "#appInvite",
    data: {
        contract: false,
        contractHelp: "",
        profile: {},
        code: "",
        inviteList: [],
        exist: false,
        accountName: "",
        letterTemplateList: [],
        letterID: "",
        letterContent: "",
        loading: {
            submitLoading: false
        }
    },
    mounted: function () {
        this.code = getUrlKey("code");
        this.getProfile();
        this.getInvite();
        this.getLetterList();
    },
    methods: {
        getProfile: function () {
            axiosGet(api.candidate + "GetProfileInfo", {
                guid: this.code
            }).then(res => {
                this.contract = res.data.contract;
                this.contractHelp = res.data.contractHelp;
                this.profile = res.data.data;
            });
        },
        // 获取模板列表
        getLetterList: function () {
            axiosGet(api.account + "GetLetterList").then(res => {
                if (res.code == 1) {
                    this.letterTemplateList = res.data.letterList;
                }
            });
        },
        getInvite: function () {
            axiosGet(api.invite + "GetInviteList", {
                code: this.code
            }).then(res => {
                this.inviteList = res.data.data;
                this.exist = res.data.isExist;
                this.accountName = res.data.name;
            });
        },
        computePutTime(time) {
            var last = Date.parse(time);
            var now = Date.now();
            return now - last <= 60 * 60 * 24 * 7 * 1000;
        },
        sendInviteLetter: function () {
            if (this.letterContent == "") {
                this.$alert("请输入邀请的打招呼内容.");
                return false;
            }
            this.loading.submitLoading = true;
            axiosPost(api.invite + "SendInviteLetter", {
                Code: this.code,
                Letter: this.letterContent
            }).then(res => {
                this.loading.submitLoading = false;
                if (res.code == 1) {
                    this.$alert(res.msg, '提示');
                    this.getInvite();
                } else {
                    this.$alert(res.msg, '提示');
                }
            });
        },
        selectChnage: function () {
            for (var i = 0; i < this.letterTemplateList.length; i++) {
                if (this.letterID == this.letterTemplateList[i].PKID) {
                    this.letterContent = this.letterTemplateList[i].TemplateContent;
                }
            }
        },
        templateRedirect: function () {
            var msg = " 即将打开模板管理页面,您可以创建新的模板或修改现有的模板.当操作完成后请回到此页面点击刷新模板按钮同步您的最新模板信息即可.";
            this.$alert(msg, '提示', {
                confirmButtonText: '确定',
                callback: action => {
                    window.open("/account/lettertemplate");
                }
            });
        }
    },
    filters: {
        monthFormat: function (val) {
            if (val == "至今") {
                return "至今";
            }
            return val.split('-')[0] + "." + val.split("-")[1];
        },
        dateFormat: function (val) {
            return val.replace("T", " ").split(".")[0];
        }
    }
});