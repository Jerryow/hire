var vm = new Vue({
    el: "#appCountry",
    data() {
        return {
            btnDisable: {
                delDialog: false
            },
            diaClose: false,
            getArgs: {
                pageIndex: "1",
                pageSize: "10",
                keyWords: ""
            },
            btnShow: {
                addShow: false,
                updateShow: false,
                title:""
            },
            selectSize: "10",
            sizes: [10, 20, 30, 50, 100],
            countryList: [],
            pagination: {
                index: 0,
                total: 0,
                pagecount: 0
            },
            result: "",
            dialogVisible: false,
            loading: false,
            form: {
                PKID: "",
                CountryName: "",
                EngName: "",
                Initial: "",
                Initials: "",
                Extra: "",
                OrderNum: ""
            },
            labelPosition: "left",
            deleteId: "",
            rules: {
                CountryName: [
                    { required: true, message: '国家名称不能为空.', trigger: 'blur' }
                ],
                EngName: [
                    { required: true, message: '英文名不能为空.', trigger: 'blur' }
                ],
                Initial: [
                    { required: true, message: '首字母不能为空.', trigger: 'blur' }
                ],
                Initials: [
                    { required: true, message: '缩写不能为空.', trigger: 'blur' }
                ],
                OrderNum: [
                    { required: true, message: '排序不能为空.', trigger: 'blur' }
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
            axiosGet(api.config + 'GetCountryPagenation', para)
                .then((datas) => {
                    var data = datas.data.data;
                    if (data.DataList.length > 0) {
                        this.countryList = data.DataList;
                        this.fillPagination(data);
                    } else {
                        this.countryList = [];
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
            this.dialogVisible = false;
            this.result = "";
        },
        addCountry: function () {
            this.form.PKID = "";
            this.form.CountryName = "";
            this.form.EngName = "";
            this.form.Initial = "";
            this.form.Initials = "";
            this.form.Extra = "";
            this.form.OrderNum = "";
            this.btnShow.addShow = true;
            this.btnShow.updateShow = false;
            this.dialogVisible = true;
        },
        submitForm(form, sta) {
            this.$refs[form].validate((valid) => {
                if (valid) {
                    axiosPost(api.config + "AddCountry", this.form)
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
        updateForm(form) {
            this.$refs[form].validate((valid) => {
                if (valid) {
                    axiosPost(api.config + "UpdateCountry", this.form)
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
        updateUserFill: function (pkid) {
            this.btnShow.addShow = false;
            this.btnShow.updateShow = true;
            
            axiosGet(api.config + "GetCountryByID",
                {
                    PKID: pkid
                })
                .then((res) => {
                    this.form.PKID = res.data.data.PKID;
                    this.form.CountryName = res.data.data.CountryName;
                    this.form.EngName = res.data.data.EngName;
                    this.form.Initial = res.data.data.Initial;
                    this.form.Initials = res.data.data.Initials;
                    this.form.Extra = res.data.data.Extra;
                    this.form.OrderNum = res.data.data.OrderNum;
                    this.dialogVisible = true;
                })
                .catch((ex) => {
                    alert(ex.Message);
                });
        },
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