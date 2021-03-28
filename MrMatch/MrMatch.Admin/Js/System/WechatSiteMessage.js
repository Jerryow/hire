var vm = new Vue({
    el: "#appMsgIndex",
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
            form: {
                PKID: "",
                UserID: "",
                MessageSubject: "",
                FromID: ""
            },
            btnShow: {
                addShow: false,
                updateShow: false
            },
            rules: {
                MessageSubject: [
                    { required: true, message: '消息主题不能为空.', trigger: 'blur' }
                ],
                UserID: [
                    { required: true, message: '标签名称不能为空.', trigger: 'change' }
                ],
                FromID: [
                    { required: true, message: '标签名称不能为空.', trigger: 'change' }
                ]
            },
            dialogArgs: {
                title: "",
                dialogVisible: false,
                diaClose: false,
                labelPosition: "top"
            },
            parentTagsList: []
        };
    },
    mounted: function () {
        this.getList();
    },
    methods: {
        getList: function () {
            var para = pageFill(this.getArgs.pageIndex, this.selectSize, this.getArgs.keyWords);
            axiosGet(api.message + 'GetMsgInfoPagenation', para)
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
        add: function (form, sta) {
            this.btnShow.addShow = false;
            this.btnShow.updateShow = false;
            if (sta == "add") {
                this.btnShow.addShow = true;
            }
            this.dialogArgs.title = "新增";
            this.dialogArgs.dialogVisible = true;

        },
        submitForm: function (form, sta) {
            this.$refs[form].validate((valid) => {
                if (valid) {
                    axiosPost(api.message + "AddMsgInfo", this.form)
                        .then((res) => {
                            if (res.data.code == "1") {
                                alert(res.data.msg);
                                window.location.reload();
                            } else {
                                alert(res.data.msg);
                            }
                        })
                        .catch((ex) => {
                            alert(ex.Message);
                        });
                } else {
                    return false;
                }
            });
        },
        updateFill: function (pkid, sta) {
            this.btnShow.addShow = false;
            this.btnShow.updateShow = false;
            this.dialogArgs.title = "修改";
            if (sta == "update") {
                this.btnShow.updateShow = true;
            }
            axiosGet(api.message + "GetMsgInfoById",
                {
                    PKID: pkid
                })
                .then((res) => {
                    this.form.PKID = res.data.data.PKID;
                    this.form.FromID = res.data.data.FromID;
                    this.form.UserID = res.data.data.UserID;
                    this.form.MessageSubject = res.data.data.MessageSubject;
                    this.dialogArgs.dialogVisible = true;
                })
                .catch((ex) => {
                    alert(ex.Message);
                });
        },
        updateForm(form, sta) {
            this.$refs[form].validate((valid) => {
                if (valid) {
                    axiosPost(api.message + "UpdateMsgInfo", this.form)
                        .then((res) => {
                            if (res.data.code == "1") {
                                alert(res.data.msg);
                                window.location.reload();
                            } else {
                                alert(res.data.msg);
                            }
                        })
                        .catch((ex) => {
                            alert(ex.Message);
                        });
                } else {
                    return false;
                }
            });
        },
        resetForm(form) {
            this.$refs[form].resetFields();
        },
        refresh: function () {
            window.location.reload();
        },
        redirectContent: function (id) {
            window.location.href = "/message/content?id=" + id;
        },
        sendMsg: function (val) {
            axiosGet(api.message + "SendMsg",
                {
                    PKID: val
                })
                .then((res) => {
                    alert(res.data.msg)
                    window.location.reload();
                })
                .catch((ex) => {
                    alert(ex.Message);
                });
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
        },
        onValid: function (val) {
            if (val) {
                return "已发送";
            } else {
                return "未发送";
            }
        },
        onPublic: function (val) {
            if (val) {
                return "是";
            } else {
                return "否";
            }
        }
    }
});