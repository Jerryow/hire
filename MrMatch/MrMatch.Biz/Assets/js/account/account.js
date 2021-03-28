var vm = new Vue({
    el: "#appAccount",
    data: {
        account: {},
        accountList: [],
        form: {
            Email: ""
        },
        loading: {
            submitLoading: false
        }
    },
    mounted: function () {
        this.getAccountInfo();
    },
    methods: {
        getAccountInfo: function () {
            axiosGet(api.account + "GetAccountsInfo").then(res => {
                this.account = res.data.account;
                this.accountList = res.data.accountList;
            });
        },
        sendInvite: function () {
            if (this.form.Email == "") {
                this.$alert("邀请邮箱不能为空", '提示');
                return false;
            }
            axiosGet(api.account + "SendInvite", this.form).then(res => {
                this.$alert(res.msg, '提示');
                this.getAccountInfo();
            });
        },
        updateActivity: function (val, id) {
            var status = false;
            if (val == "停用") {
                status = false;
            } else {
                status = true;
            }
            this.loading.submitLoading = true;
            axiosGet(api.account + "AccountActivity?PKID=" + id + "&onActive=" + status).then(res => {
                this.loading.submitLoading = false;
                this.$alert(res.msg, '提示');
                if (res.code == "1") {
                    this.getAccountInfo();
                } else {}
            });
        }
    }
});