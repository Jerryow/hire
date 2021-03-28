var vm = new Vue({
    el: "#appMsgConfig",
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
                ConfigType: "",
                ProviderName: "",
                ApiName: "",
                ApiCode: "",
                ApiUrl: "",
                Port: "",
                SenderName: "",
                LoginName: "",
                Password: "",
                VerifyKey: "",
                SignMark: "",
                TimesLimit: "",
                EnableSsl: false,
                IsActivated: false

            },
            rules: {
                ConfigType: [
                    { required: true, message: '请选择配置类型.', trigger: 'change' }
                ],
                ProviderName: [
                    { required: true, message: '供应商名字不能为空.', trigger: 'blur' }
                ],
                ApiName: [
                    { required: true, message: 'Api名不能为空.', trigger: 'blur' }
                ],
                ApiCode: [
                    { required: true, message: 'Api编码不能为空.', trigger: 'blur' }
                ],
                ApiUrl: [
                    { required: true, message: 'Api地址不能为空.', trigger: 'blur' }
                ],
                Port: [
                    { required: true, message: '端口不能为空.', trigger: 'blur' }
                ],
                SenderName: [
                    { required: true, message: '发送者名称不能为空.', trigger: 'blur' }
                ],
                LoginName: [
                    { required: true, message: '登录名不能为空.', trigger: 'blur' }
                ],
                Password: [
                    { required: true, message: '密码不能为空.', trigger: 'blur' }
                ],
                VerifyKey: [
                    { required: true, message: '验证密钥不能为空.', trigger: 'blur' }
                ],
                SignMark: [
                    { required: true, message: '签名不能为空.', trigger: 'blur' }
                ],
                TimesLimit: [
                    { required: true, message: '发送限制次数不能为空.', trigger: 'blur' }
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
            axiosGet(api.system + 'GetMessageConfigByPagenation', para)
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
        addConfig: function (form) {
            this.dialogVisible = true;
            //setTimeout(() => {
            //    this.formInit();
            //}, 500);
        },
        submitForm(form) {
            this.$refs[form].validate((valid) => {
                if (valid) {
                    axiosPost(api.system + "AddMessageConfig", this.addForm)
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
            window.location.href = "/system/MessageConfigItem?id=" + pkid;
        },
        reload: function () {
            window.location.reload();
        }
    },
    filters: {
        formatNull: function (val) {
            if (val === "null" || val === "Null") {
                return "";
            } else {
                return val;
            }
        },
        sslFilter: function (val) {
            if (val) {
                return "加密";
            } else {
                return "不加密";
            }
        },
        activeFilter: function (val) {
            if (val) {
                return "启用";
            } else {
                return "不启用";
            }
        },
        cfgType: function (val) {
            if (val == 10)
                return "短信发送";
            else
                return "邮件发送";
        }
    }
});
