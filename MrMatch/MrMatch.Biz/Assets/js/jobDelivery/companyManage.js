
var vm = new Vue({
    el: "#appCompanyManage",
    components: {},
    data: {
        pagenation: {
            pageIndex: 1,
            pageSize: 10,
            total: 0
        },
        rules: {
            CompanyName: [{ required: true, message: '请输入公司全称', trigger: 'change' }]
        },
        form: {
            CompanyName: ''
        },
        tableData: [],
        editCompanyData: {},
        editCompanyDialog: false
    },
    watch: {
        editCompanyDialog(newVal, oldVal) {
            console.log(1, newVal);
        }
    },
    mounted: function () {
        this.getAgentCompanyList();
    },
    methods: {
        getAgentCompanyList(type) {
            let params = {
                pageIndex: this.pagenation.pageIndex,
                pageSize: this.pagenation.pageSize,
                companyName: this.form.CompanyName
            };
            axiosGet(api.delivery + "GetAgentCompanyList", params).then(res => {
                if (res.code == 1 && res.data) {
                    this.tableData = res.data.DataList;
                    this.pagenation.total = res.data.Total;
                } else {
                    this.tableData = [];
                    this.pagenation.total = 0;
                }
            });
        },
        companyHandleClick(option) {
            console.log(option.PKID);
            if (option.PKID) {
                this.editCompanyData = option;
            } else {
                this.editCompanyData = {};
            }
            this.editCompanyDialog = true;
        },
        saveJobCompany(e) {

            this.$message(e.res.msg);

            if (e.res.code == 1) {

                this.editCompanyDialog = false;
                this.getAgentCompanyList();
            }
        },
        currentChange(e) {
            console.log(e);
            this.pagenation.pageIndex = e;
            this.getAgentCompanyList();
        }
    },
    filters: {
        DateFilter(date) {
            return new Date(new Date(date)).format("yyyy-MM-dd hh:mm:ss");
        }
    }
});