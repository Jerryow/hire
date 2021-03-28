var vm = new Vue({
    el: "#appCompanyIndex",
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
                CompanyName: "",
                CompnayType: "",
                Website: "",
                EmployeeNum: "",
                Address: "",
                IsChecked: false,
                Introduce: ""
            },
            btnShow: {
                addShow: false,
                updateShow: false
            },
            rules: {
                CompanyName: [
                    { required: true, message: '企业名称不能为空.', trigger: 'blur' }
                ],
                Website: [
                    { required: true, message: '官网不能为空.', trigger: 'blur' }
                ],
                Address: [
                    { required: true, message: '企业地址不能为空.', trigger: 'blur' }
                ],
                Introduce: [
                    { required: true, message: '企业介绍不能为空.', trigger: 'blur' }
                ]
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
            },
            companyTypeList: [
                { ID: "11", Name: "国有企业" },
                { ID: "12", Name: "集体企业" },
                { ID: "13", Name: "股份合作企业" },
                { ID: "14", Name: "联营企业" },
                { ID: "15", Name: "有限责任公司" },
                { ID: "16", Name: "股份有限公司" },
                { ID: "17", Name: "私营企业" },
                { ID: "20", Name: "港澳台商投资企业" },
                { ID: "30", Name: "外商投资企业" }
            ],
            employeeList: [
                { ID: "少于15人", Name: "少于15人" },                { ID: "15-50人", Name: "15-50人" },                { ID: "50-150人", Name: "50-150人" },                { ID: "150-500人", Name: "150-500人" },                { ID: "500-1000人", Name: "500-1000人" },                { ID: "1000-5000人", Name: "1000-5000人" },                { ID: "5000-10000人", Name: "5000-10000人" },                { ID: "10000人以上", Name: "10000人以上" }
            ],
            daysSelect: [
                { label: "1天", value: 1 },
                { label: "3天", value: 3 },
                { label: "7天", value: 7 },
                { label: "15天", value: 15 },
                { label: "30天", value: 30 }
            ],
            selectDay: 1
        };
    },
    mounted: function () {
        this.getList();
    },
    methods: {
        getList: function () {
            var para = pageFill(this.getArgs.pageIndex, this.selectSize, this.getArgs.keyWords);
            axiosGet(api.company + 'GetCompanyInfoPagenation', para)
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
                    axiosPost(api.company + "AddCompany", this.form)
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
            axiosGet(api.company + "GetCompanyById",
                {
                    PKID: pkid
                })
                .then((res) => {
                    this.form.PKID = res.data.data.PKID;
                    this.form.CompanyName = res.data.data.CompanyName;
                    this.form.CompnayType = res.data.data.CompnayType;
                    this.form.Website = res.data.data.Website;
                    this.form.EmployeeNum = res.data.data.EmployeeNum;
                    this.form.Address = res.data.data.Address;
                    this.form.IsChecked = res.data.data.IsChecked;
                    this.form.Introduce = res.data.data.Introduce;
                    this.dialogArgs.dialogVisible = true;
                })
                .catch((ex) => {
                    alert(ex.Message);
                });
        },
        updateForm(form, sta) {
            this.$refs[form].validate((valid) => {
                if (valid) {
                    axiosPost(api.company + "UpdateCompany", this.form)
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
            window.location.href = "/company/index";
        },
        handlerDrop: function (val, url) {
            var page = url + "?id=" + val;
            window.location.href = page;
        },
        waitForCheck: function () {
            this.getArgs.keyWords = "check";
            this.getList();
        },
        newUser: function () {
            this.getArgs.keyWords = "new-" + this.selectDay;
            this.getList();
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
        isChecked: function (val) {
            if (val) {
                return "通过";
            } else {
                return "未通过";
            }
        },
        typeFormat: function (val) {
            var list = [
                { ID: "11", Name: "国有企业" },
                { ID: "12", Name: "集体企业" },
                { ID: "13", Name: "股份合作企业" },
                { ID: "14", Name: "联营企业" },
                { ID: "15", Name: "有限责任公司" },
                { ID: "16", Name: "股份有限公司" },
                { ID: "17", Name: "私营企业" },
                { ID: "20", Name: "港澳台商投资企业" },
                { ID: "30", Name: "外商投资企业" }
            ];
            for (var i = 0; i < list.length; i++) {
                if (val == list[i].ID) {
                    return list[i].Name;
                }
            }
        }
    }
});