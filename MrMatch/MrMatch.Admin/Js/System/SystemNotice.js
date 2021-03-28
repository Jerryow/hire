var vm = new Vue({
    el: "#appSystemNotice",
    data() {
        return {
            btnShow: {
                addShow: false,
                updateShow: false
            },
            btnDisable: {
                delDialog: false
            },
            inputDisable: false,
            tableRowProp: {
                hiden: true
            },
            diaClose: false,
            getArgs: {
                pageIndex: "1",
                pageSize: "10",
                keyWords: ""
            },
            selectSize: "10",
            sizes: [10, 20, 30, 50, 100],
            tableData: [],
            pagination: {
                index: 0,
                total: 0,
                pagecount: 0
            },
            result: "",
            dialogVisible: false,
            dialogDelete: false,
            loading: false,
            users: [],
            usersID:[],
            form: {
                PKID: "",
                MessageSubject: "",
                MessageContent: "",
                MessageType: "",
                UserIDs: "",
                SendClient: "",
                IsFinish: false,
                Remark: ""
            },
            labelPosition: "left",
            deleteId: "",
            rules: {
                MessageSubject: [
                    { required: true, message: '消息主题不能为空.', trigger: 'blur' }
                ],
                MessageContent: [
                    { required: true, message: '消息内容不能为空.', trigger: 'blur' }
                ],
                SendClient: [
                    { required: true, message: '请选择接收端.', trigger: 'change' }
                ],
                MessageType: [
                    { required: true, message: '请选择接收类型.', trigger: 'change' }
                ]
            },
            sendToList: [
                {
                    val: 1,
                    display: "B端"
                },
                {
                    val: 2,
                    display: "C端"
                }
            ],
            msgTypeList: [
                {
                    val: 1,
                    display: "所有用户"
                },
                {
                    val: 2,
                    display: "指定用户"
                }
            ]
        };
    },
    mounted: function () {
        this.getList();
        this.getUserList();
    },
    methods: {
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
        getList: function () {
            var para = pageFill(this.getArgs.pageIndex, this.selectSize, this.getArgs.keyWords);
            axiosGet(api.system + 'GetSystemNoticePagenation', para)
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
        closeModal: function () {
            this.formInit();
            this.dialogVisible = false;
            this.usersID = [];
            this.result = "";
        },
        addUser: function (form, sta) {
            this.btnShow.addShow = false;
            this.btnShow.updateShow = false;
            if (sta == "add") {
                this.btnShow.addShow = true;
            }
            this.dialogVisible = true;
            setTimeout(() => {
                this.formInit();
            }, 500);
        },
        submitForm(form, sta) {
            this.$refs[form].validate((valid) => {
                if (valid) {

                    if (this.form.MessageType == 1) {
                        this.inputDisable = true;
                        this.usersID = [];
                        this.form.UserIDs = "";
                    } else {
                        this.inputDisable = false;
                        for (var i = 0; i < this.usersID.length; i++) {
                            this.form.UserIDs += this.usersID[i]
                            this.form.UserIDs += ","
                        }
                        this.form.UserIDs = this.form.UserIDs.substring(0, (this.form.UserIDs.length - 1))
                    }

                    axiosPost(api.system + "AddSystemNotice", this.form)
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
        updateForm(form, sta) {
            this.$refs[form].validate((valid) => {
                if (valid) {

                    this.form.UserIDs = "";
                    if (this.form.MessageType == 1) {
                        this.inputDisable = true;
                        this.usersID = [];
                    } else {
                        this.inputDisable = false;
                        for (var i = 0; i < this.usersID.length; i++) {
                            this.form.UserIDs += this.usersID[i]
                            this.form.UserIDs += ","
                        }
                        this.form.UserIDs = this.form.UserIDs.substring(0, (this.form.UserIDs.length - 1))
                    }

                    axiosPost(api.system + "UpdateSystemNotice", this.form)
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
        formInit: function () {
            this.form.PKID = "";
            this.form.MessageSubject = "";
            this.form.MessageContent = "";
            this.form.MessageType = "";
            this.form.SendClient = "";
            this.form.UserIDs = "";
            this.form.Remark = "";
        },
        updateUserFill: function (pkid, sta) {
            this.btnShow.addShow = false;
            this.btnShow.updateShow = false;
            if (sta == "update") {
                this.btnShow.updateShow = true;
            }
            axiosGet(api.system + "GetSystemNoticeById",
                {
                    PKID: pkid
                })
                .then((res) => {
                    this.form.PKID = res.data.data.PKID;
                    this.form.MessageSubject = res.data.data.MessageSubject;
                    this.form.MessageContent = res.data.data.MessageContent;
                    this.form.MessageType = res.data.data.MessageType;
                    this.form.UserIDs = res.data.data.UserIDs;
                    this.form.SendClient = res.data.data.SendClient;
                    this.form.Remark = res.data.data.Remark;
                    if (this.form.MessageType == 2) {
                        if (this.form.UserIDs.length >= 1) {
                            this.usersID = this.form.UserIDs.split(',').map((val, index) => {
                                return parseInt(val)
                            })
                        }
                        this.inputDisable = false;
                    } else {
                        this.inputDisable = true;
                        this.usersID = [];
                    }
                    this.dialogVisible = true;
                })
                .catch((ex) => {
                    alert(ex.Message);
                });
        },
        sendWebsocket: function (val) {
            axiosGet(api.system + "ClientNotice?PKID=" + val)
                .then((res) => {
                    alert(res.data.msg);
                    this.getList();
                })
        },
        getUserList: function () {
            axiosGet(api.user + "GetUserIDPhone")
                .then((res) => {
                    this.users = res.data.data;  
                })
        },
        selectChange: function () {
            if (this.form.MessageType == 1) {
                this.inputDisable = true;
                this.usersID = [];
            } else {
                this.inputDisable = false;
            }
        }
    },
    filters: {
        formatDate: function (val) {
            return val.replace('T', ' ');
        },
        formatNull: function (val) {
            if (val === "null" || val === "Null") {
                return "";
            } else {
                return val;
            }
        },
        isFinish: function (val) {
            if (val) {
                return "是";
            } else {
                return "否";
            }
        },
        sendTo: function (val) {
            if (val == 1) {
                return "B端";
            } else {
                return "C端";
            }
        },
        msgType: function (val) {
            if (val == 1) {
                return "所有用户";
            } else {
                return "指定用户";
            }
        }
    }
});