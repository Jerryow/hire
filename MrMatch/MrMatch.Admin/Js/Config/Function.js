

var vm = new Vue({
    el: "#appTag",
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
                ParentID: "",
                FunctionName: "",
                OnEnabled: false,
                SortNum: "",
                IsPopular: false
            },
            formTwo: {
                ParentID: "",
                FunctionName: "",
                OnEnabled: true,
                SortNum: ""
            },
            SkillIDs: [],
            formThree: {
                skillIDs: "",
                functionID: 0
            },
            btnShow: {
                addShow: false,
                updateShow: false
            },
            rules: {
                FunctionName: [
                    { required: true, message: '职能名称不能为空.', trigger: 'blur' }
                ],
                SortNum: [
                    { required: true, message: '排序不能为空.', trigger: 'blur' }
                ]
            },
            dialogArgs: {
                title: "",
                dialogVisible: false,
                diaClose: false,
                labelPosition: "top"
            },
            dialogSecondArgs: {
                title: "",
                dialogVisible: false,
                diaClose: false,
                labelPosition: "top"
            },
            dialogThreeArgs: {
                title: "",
                dialogVisible: false,
                diaClose: false,
                labelPosition: "top"
            },
            allFuncList: [],
            skillList: [],
            searchSelect: {
                firstFuncList: [],
                secondFuncList: [],
                firstSel: "",
                secondSel: ""
            }
        };
    },
    mounted: function () {
        this.getList();
        this.getAllSkill();
        this.getAllFunc();
    },
    methods: {
        getAllSkill: function () {
            //axiosGet(api.config + "GetFirstLevelFunction")
            //    .then((res) => {
            //        this.searchSelect.firstFuncList = res.data.data;
            //    });
            
            axiosGet(api.config + "GetAllSkill")
                .then((res) => {
                    this.skillList = res.data.data;
                });
        },
        getAllFunc: function myfunction() {
            axiosGet(api.config + "GetAllFunction")
                .then((res) => {
                    this.allFuncList = res.data.data;
                    var nullV = {
                        FunctionName: "无",
                        PKID: 0
                    };
                    this.allFuncList.unshift(nullV);
                });
        },
        getAllFirstFunc: function () {
            this.getArgs.keyWords = "btnFirst";
            this.getList();
        },
        getSelectFunc: function () {
            this.getArgs.keyWords = "btnSel-" + this.searchSelect.firstSel;
            this.getList();
        },
        getSelectCityFunc: function () {
            this.getArgs.keyWords = "btnSel-" + this.searchSelect.secondSel;
            this.getList();
        },
        getSelectChange: function () {
            axiosGet(api.config + "GetSelectChangeFunction",
                {
                    "PKID": this.searchSelect.firstSel
                })
                .then((res) => {
                    this.searchSelect.secondFuncList = res.data.data;
                })
        },
        getList: function () {
            this.closeModal();
            this.closeModalTwo();
            this.closeModalThree();
            var para = pageFill(this.getArgs.pageIndex, this.selectSize, this.getArgs.keyWords);
            axiosGet(api.config + 'GetFunctionPagenation', para)
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
        closeModalTwo: function () {
            this.dialogSecondArgs.dialogVisible = false;
        },
        closeModalThree: function () {
            this.dialogThreeArgs.dialogVisible = false;
        },
        add: function (form, sta) {
            this.getAllFunc();
            this.btnShow.addShow = false;
            this.btnShow.updateShow = false;
            if (sta == "add") {
                this.btnShow.addShow = true;
            }
            this.dialogArgs.title = "新增";
            this.form.PKID = "";
            this.form.ParentID = "";
            this.form.FunctionName = "";
            this.form.OnEnabled = false;
            this.form.SortNum = "";
            this.form.IsPopular = false;
            this.dialogArgs.dialogVisible = true;

        },
        addTwo: function (pkid) {
            this.formTwo.ParentID = pkid;
            this.dialogSecondArgs.title = "新增";
            this.dialogSecondArgs.dialogVisible = true;
        },
        addThree: function (pkid) {
            axiosGet(api.config + "GetFunctionSkill",
                {
                    functionID: pkid
                })
                .then((res) => {
                    this.getAllSkill();
                    this.formThree.functionID = pkid;
                    this.formThree.skillIDs = "";
                    this.SkillIDs = res.data.data;
                    this.dialogThreeArgs.title = "新增/编辑";
                    this.dialogThreeArgs.dialogVisible = true;
                })
                .catch((ex) => {
                    alert(ex.Message);
                });
        },
        submitForm: function (form, sta) {
            this.$refs[form].validate((valid) => {
                if (valid) {
                    axiosPost(api.config + "AddFunction", this.form)
                        .then((res) => {
                            if (res.data.code == "1") {
                                alert(res.data.msg);
                                this.getList();
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
        submitFormTwo: function (form, sta) {
            this.$refs[form].validate((valid) => {
                if (valid) {
                    axiosPost(api.config + "AddFunction", this.formTwo)
                        .then((res) => {
                            if (res.data.code == "1") {
                                alert(res.data.msg);
                                this.getList();
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
        submitFormThree: function (form, sta) {
            console.log(this.SkillIDs);
            for (var i = 0; i < this.SkillIDs.length; i++) {
                this.formThree.skillIDs += this.SkillIDs[i];
                this.formThree.skillIDs += ",";
            }
            this.formThree.skillIDs = this.formThree.skillIDs.substring(0, (this.formThree.skillIDs.length - 1));
            this.$refs[form].validate((valid) => {
                if (valid) {
                    axiosGet(api.config + "AddFunctionSkill", this.formThree)
                        .then((res) => {
                            if (res.data.code == "1") {
                                alert(res.data.msg);
                                this.getList();
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
            this.getAllFunc();
            this.btnShow.addShow = false;
            this.btnShow.updateShow = false;
            this.dialogArgs.title = "修改";
            if (sta == "update") {
                this.btnShow.updateShow = true;
            }
            axiosGet(api.config + "GetFunctionById",
                {
                    PKID: pkid
                })
                .then((res) => {
                    this.form.PKID = res.data.data.PKID;
                    this.form.ParentID = res.data.data.ParentID;
                    this.form.FunctionName = res.data.data.FunctionName;
                    this.form.OnEnabled = res.data.data.OnEnabled;
                    this.form.SortNum = res.data.data.SortNum;
                    this.dialogArgs.dialogVisible = true;
                })
                .catch((ex) => {
                    alert(ex.Message);
                });
        },
        updateForm(form, sta) {
            this.$refs[form].validate((valid) => {
                if (valid) {
                    axiosPost(api.config + "UpdateFunction", this.form)
                        .then((res) => {
                            if (res.data.code == "1") {
                                alert(res.data.msg);
                                this.getList();
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
        refresh: function () {
            window.location.reload();
        },
        setCache: function () {
            axiosGet(api.config + "SetBasicFunctionCache")
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
        onEnable: function (val) {
            if (val) {
                return "启用";
            } else {
                return "不启用";
            }
        },
        isPopular: function (val) {
            if (val) {
                return "是";
            } else {
                return "否";
            }
        }
    }
});