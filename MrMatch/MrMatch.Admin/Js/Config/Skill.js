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
            form: {
                PKID: "",
                SkillName: "",
                OnEnabled: false
            },
            labelPosition: "left",
            deleteId: "",
            rules: {
                SkillName: [
                    { required: true, message: '消息主题不能为空.', trigger: 'blur' }
                ]
            }
        };
    },
    mounted: function () {
        this.getList();
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
            axiosGet(api.config + 'GetSkillPagenation', para)
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
                    axiosPost(api.config + "AddSkill", this.form)
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
                    axiosPost(api.config + "UpdateSkill", this.form)
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
            this.form.SkillName = "";
            this.form.OnEnabled = false;
        },
        updateUserFill: function (pkid, sta) {
            this.btnShow.addShow = false;
            this.btnShow.updateShow = false;
            if (sta == "update") {
                this.btnShow.updateShow = true;
            }
            axiosGet(api.config + "GetSkillById",
                {
                    PKID: pkid
                })
                .then((res) => {
                    this.form.PKID = res.data.data.PKID;
                    this.form.SkillName = res.data.data.SkillName;
                    this.form.OnEnabled = res.data.data.OnEnabled;
                    this.form.Remark = res.data.data.Remark;
                    this.dialogVisible = true;
                })
                .catch((ex) => {
                    alert(ex.Message);
                });
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
        }
    }
});