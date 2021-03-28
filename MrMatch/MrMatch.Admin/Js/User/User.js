var vm = new Vue({
    el: "#appUserIndex",
    data() {
        return {
            getArgs: {
                pageIndex: "1",
                pageSize: "10",
                keyWords: ""
            },
            selectSize: "10",
            sizes: [10, 20, 30, 50, 100],
            pagination: {
                index: 0,
                total: 0,
                pagecount: 0
            },
            tableData: [],
            tableRowProp: {
                hiden: true
            },
            dialogArgs: {
                title: "",
                dialogVisible: false,
                diaClose: false,
                labelPosition: "top"
            },
            tagType: function (val) {
                if (val) {
                    return "success";
                } else {
                    return "danger";
                }
            }
        };
    },
    mounted: function () {
        this.getList();
    },
    methods: {
        getList: function () {
            var para = pageFill(this.getArgs.pageIndex, this.selectSize, this.getArgs.keyWords);
            axiosGet(api.user + 'GetCheckUserInfoPagenation', para)
                .then((datas) => {
                    var data = datas.data.data;
                    if (data.DataList.length > 0) {
                        this.tableData = data.DataList;
                        this.fillPagination(data);
                    } else {
                        this.tableData = [];
                        this.pagination.total = parseInt("0");
                        this.pagination.index = parseInt("1");
                        this.pagination.pagecount = parseInt("1");
                    }
                })
                .catch((ex) => {
                    console.log(ex);
                });

        },
        fillPagination: function (data) {
            this.pagination.total = parseInt(data.Total);
            this.pagination.index = parseInt(data.PageIndex);
            this.pagination.pagecount = parseInt(data.PageCount);
        },
        handleCurrentChange(val) {
            this.getArgs.pageIndex = val;
            this.getList();
        },
        rowClass: function (row, rowIndex) {
            return obj = {
                "height": "20px",
                "background-color": "#f4f4f4"
            };
        },
        cellClass: function (row, rowIndex) {
            return obj = {
                "padding": "0px"
            };
        },
        closeModal: function () {
            this.dialogArgs.dialogVisible = false;
        },
        refresh: function () {
            window.location.href = "/user/checkuser";
        },
        agreeOrNot: function (id, status) {
            var approveStatus = "0";
            if (status == "pass") {
                approveStatus = "2";
            } else {
                approveStatus = "3";
            }
            axiosGet(api.user + "UpdateApproveStatus",
                {
                    "PKID": id,
                    "status": approveStatus
                })
                .then((res) => {
                    alert(res.data.msg);
                    this.getList();
                })
        }
    },
    filters: {
        formatDate: function (val) {
            return val.split('T')[0];
        },
        formatNull: function (val) {
            if (val === "null" || val === "Null") {
                return "";
            } else {
                return val;
            }
        }
    }
});