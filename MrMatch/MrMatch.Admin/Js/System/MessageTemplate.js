var vm = new Vue({
    el: "#appMsgTemplate",
    data() {
        return {
            tableData: [],
            dialogVisible: false,
            diaClose: false,
            labelPosition: "top",
            tableRowProp: {
                hiden: true
            },
            getArgs: {
                pageIndex: "1",
                pageSize: "10",
                keyWords: ""
            },
            pagination: {
                index: 0,
                total: 0,
                pagecount: 0
            },
            selectSize: "10",
            sizes: [10, 20, 30, 50, 100],
            addForm: {
                TemplateType: "",
                TemplateCode: "",
                TemplateTitle: "",
                TemplateContent: "",
                IsActivated: false

            },
            rules: {
                TemplateType: [
                    { required: true, message: '请选择模板类型.', trigger: 'change' }
                ],
                TemplateCode: [
                    { required: true, message: '模板编码不能为空.', trigger: 'blur' }
                ],
                TemplateTitle: [
                    { required: true, message: '模板标题不能为空.', trigger: 'blur' }
                ],
                TemplateContent: [
                    { required: true, message: '模板内容不能为空.', trigger: 'blur' }
                ]
            }
        };
    },
    mounted: function () {
        this.getList();
    },
    methods: {
        getList: function () {
            var para = pageFill(this.getArgs.pageIndex, this.selectSize, this.getArgs.keyWords);
            axiosGet(api.system + 'GetMessageTemplatePagenation', para)
                .then((res) => {
                    var data = res.data.data;
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
        refresh: function () {
            window.location.reload();
        },
        handleClick(row) {
            console.log(row.PKID);
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
        tagType: function (val) {
            if (val) {
                return "success";
            } else {
                return "danger";
            }
        },
        closeModal: function () {
            //this.formInit();
            this.dialogVisible = false;
            //this.result = "";
        },
        addTempalte: function (form) {
            this.dialogVisible = true;
            //setTimeout(() => {
            //    this.formInit();
            //}, 500);
        },
        submitForm(form) {
            this.$refs[form].validate((valid) => {
                if (valid) {
                    axiosPost(api.system + "AddMessageTemplate", this.addForm)
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
        edit: function (pkid) {
            window.location.href = "/system/MessageTemplateItem?id=" + pkid;
        },
        setCache: function (PKID) {
            axiosGet(api.system + "SetTemplateCache?PKID=" + PKID)
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
        }
    },
    filters: {
        activeFilter: function (val) {
            if (val) {
                return "启用";
            } else {
                return "不启用";
            }
        },
        tempType: function (val) {
            if (val == 10)
                return "短信发送";
            else
                return "邮件发送";
        }
    }
});
