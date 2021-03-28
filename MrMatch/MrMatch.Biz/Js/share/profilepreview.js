var vm = new Vue({
    el: "#preview",
    data: {
        contract: false,
        contractHelp: "",
        profile: {},
        code: "",
        helpDialogVisible: false
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
            return val.replace("T", " ").split(".")[0];
        },
    }
})