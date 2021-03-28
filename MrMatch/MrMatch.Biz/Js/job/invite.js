var vm = new Vue({
    el: "#appInvite",
    data: {
        inviteInfoList: [],
        accountName:'',
        contract: null,
        form: {
            pageIndex: 1,
            pageSize: 10,
            total: 0,
            cities: '',
            salary: '',
            functions: '',
            position: '',
            years: 0,
            status:0
        },
        helpDialogVisible: false
    },
    mounted: function () {
        var sta = getUrlKey("status");
        if (sta !== null && sta !== "") {
            this.form.status = parseInt(sta);
            history.pushState("c", "das", "invite");
        }
        this.getProfileList()
    },
    methods: {
        // 获取搜索结果
        getProfileList: function () {
            axiosGet(api.invite + "GetJobInviteList", this.form)
                .then((res) => {
                    if (res.code == 1) {

                        if (res.data) {
                            this.contract = res.data.contract
                            this.accountName = res.data.accountName
                            this.inviteInfoList = res.data.data.DataList
                            this.form.total = res.data.data.Total
                            this.form.pageIndex = res.data.data.PageIndex
                        }
                    } else {
                        this.$alert(res.msg, '提示')
                    }
                })
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
        // 点击分页
        currentChange: function (e) {
            this.form.pageIndex = e
            this.getProfileList()
        },
        // 计算是否本周发布
        computePutTime: function (time) {
            var last = Date.parse(time)
            var now = Date.now()
            return now - last <= 60 * 60 * 24 * 7 * 1000
        },
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
        dateFormat1: function (val) {
            var year = val.split("T")[0].split('-')[0];
            var month = val.split("T")[0].split('-')[1].replace(/^0/, "");
            return year + "." + month;
        }
    }
})