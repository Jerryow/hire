
var vm = new Vue({
    el: "#appJobManage",
    data() {
        return {
            pagenation: {
                pageIndex: 1,
                pageSize: 10,
                total: 0
            },
            rules: {
                jobCode: [{ required: false, message: '请输入公司全称', trigger: 'change' }],
                jobName: [{ required: false, message: '请输入公司全称', trigger: 'change' }]
            },
            form: {
                jobCode: '',
                jobName: ''
            },
            activeName: 'activity',
            list: [],
            loading: false,
            accountID: 0
        };
    },
    watch: {
        ['composition.salaryComposition'](newVal, oldVal) {
            console.log(newVal);
        }
    },
    mounted: function () {
        // activity 上架
        // unactivity 下架
        // draft 草稿

        if (ChkUtil.getQueryString('type')) {
            this.activeName = ChkUtil.getQueryString('type');
        } else {
            if (sessionStorage.getItem('activeName')) {
                this.activeName = sessionStorage.getItem('activeName');
            }
        }

        this.pageInit();
        this.getPageType();
    },
    methods: {
        pageInit() {
            this.pagenation = {
                pageIndex: 1,
                pageSize: 10,
                total: 0
            };
            this.form = {
                jobCode: '',
                jobName: ''
            };
            this.list = [];
        },
        getPageType() {
            if (this.activeName == "draft") {
                this.getDraftJobList();
            } else {
                this.getActivityJobList();
            }
        },
        getActivityJobList() {
            let params = {
                pageIndex: this.pagenation.pageIndex,
                pageSize: this.pagenation.pageSize,
                type: this.activeName == 'activity' ? true : false,
                jobID: this.form.jobCode,
                jobName: this.form.jobName
            };
            this.loading = true;
            axiosGet(api.delivery + "GetActivityJobList", params).then(res => {
                this.loading = false;
                if (res.code == 1) {
                    this.list = res.data ? res.data.DataList : [];
                    this.pagenation.total = res.data ? res.data.Total : 0;
                } else {
                    this.$message(res.msg);
                }
            });
        },
        getDraftJobList() {
            let params = {
                pageIndex: this.pagenation.pageIndex,
                pageSize: this.pagenation.pageSize,
                jobName: this.form.jobName
            };
            this.loading = true;
            axiosGet(api.delivery + "GetJobDraftList", params).then(res => {
                this.loading = false;
                if (res.code == 1) {
                    this.list = res.data ? res.data.DataList : [];
                    this.pagenation.total = res.data ? res.data.Total : 0;
                } else {
                    this.$message(res.msg);
                }
            });
        },
        currentChange(e) {
            this.pagenation.pageIndex = e;
            this.getPageType();
        },
        handleClick(tab, event) {
            sessionStorage.setItem('activeName', this.activeName);
            this.pageInit();
            this.getPageType();
            //console.log(tab,event)

            //location.href = "/jobdelivery/jobmanage?type=" + tab.paneName
        },

        editJob(item, type) {
            window.open('/jobdelivery/publish?id=' + item.PKID + (type ? '&type=' + type : ''));
        },
        delDraftJob(item) {
            axiosGet(api.delivery + "DestroyJobDraft?PKID=" + item.PKID + "&accountID=" + item.AccountID).then(res => {
                this.$message(res.msg);
                if (res.code == 1) {
                    this.getDraftJobList();
                }
            });
        },
        updateJobActivityStatus(item, type) {
            let params = {
                jobID: item.PKID,
                type: type == 0 ? false : true
            };
            axiosGet(api.delivery + "UpdateActivityJobStatus", params).then(res => {
                this.$message(res.msg);
                if (res.code == 1) {
                    this.getActivityJobList();
                }
            });
        }
    },
    filters: {
        DateFilter(date) {
            return new Date(date).format("yyyy-MM-dd hh:mm:ss");
        },
        NameFilter(text) {
            return text;
        }

    }
});