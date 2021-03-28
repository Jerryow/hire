var vm = new Vue({
    el: "#appDistrict",
    data() {
        return {
            btnDisable: {
                delDialog: false
            },
            btnShow: {
                addShow: false,
                updateShow: false,
                title:""
            },
            diaClose: false,
            getArgs: {
                pageIndex: "1",
                pageSize: "10",
                keyWords: ""
            },
            searchSelect: {
                provinceSel: [],
                citySel: [],
                selectProvince: "",
                selectCity: ""
            },
            selectSize: "10",
            sizes: [10, 20, 30, 50, 100],
            districtList: [],
            parentList: [],
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
                CountryID: "",
                DistrictName: "",
                ParentID: "",
                OrderNum: "",
                OnShow: false,
                HotCity: false
            },
            labelPosition: "top",
            tableRowProp: {
                hiden: true
            },
            countries: [],
            rules: {
                CountryID: [
                    { required: true, message: '请选择国家.', trigger: 'change' }
                ],
                DistrictName: [
                    { required: true, message: '城市名不能为空.', trigger: 'blur' }
                ],
                ParentID: [
                    { required: true, message: '请选择上级城市.', trigger: 'change' }
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
        getCountry: function () {
            axiosGet(api.config + "GetAllCountry")
                .then((res) => {
                    this.countries = res.data.data;
                    this.getDistricts();
                });
            
            //axiosGet(api.config + "GetProvinceDistrict")
            //    .then((res) => {
            //        this.searchSelect.provinceSel = res.data.data;
            //    });
        },
        getDistricts: function () {
            axiosGet(api.config + "GetAllDistrict")
                .then((res) => {
                    this.parentList = res.data.data;
                    var nullV = {
                        DistrictName: "无",
                        PKID:0
                    };
                    this.parentList.unshift(nullV);
                });
        },
        getList: function () {
            var para = pageFill(this.getArgs.pageIndex, this.selectSize, this.getArgs.keyWords);
            return axiosGet(api.config + 'GetDistrictPagenation', para)
                .then((datas) => {
                    var data = datas.data.data;
                    if (data.DataList.length > 0) {
                        this.districtList = data.DataList;
                        this.fillPagination(data);
                        this.getCountry();
                    } else {
                        this.districtList = [];
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
        add: function (form, sta) {
            this.btnShow.addShow = false;
            this.btnShow.updateShow = false;
            this.btnShow.title = "修改";
            if (sta == "add") {
                this.btnShow.addShow = true;
            }
            this.form.PKID = "";
            this.form.CountryID = "";
            this.form.DistrictName = "";
            this.form.ParentID = "";
            this.form.OrderNum = "";
            this.form.OnShow = false;
            this.form.HotCity = false;
            this.dialogVisible = true;
        },
        submitForm(form, sta) {
            this.$refs[form].validate((valid) => {
                if (valid) {
                    axiosPost(api.config + "AddDistrict", this.form)
                        .then((res) => {
                            if (res.data.code == "1") {
                                alert(res.data.msg);
                                this.getList();
                                this.closeModal();
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
                    axiosPost(api.config + "UpdateDistrict", this.form)
                        .then((res) => {
                            if (res.data.code == "1") {
                                alert(res.data.msg);
                                this.getList();
                                this.closeModal();
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
            this.btnShow.title = "修改";
            if (sta == "update") {
                this.btnShow.updateShow = true;
            }
            axiosGet(api.config + "GetDistrictById",
                {
                    PKID: pkid
                })
                .then((res) => {
                    this.form.PKID = res.data.data.PKID;
                    this.form.CountryID = res.data.data.CountryID;
                    this.form.DistrictName = res.data.data.DistrictName;
                    this.form.ParentID = res.data.data.ParentID;
                    this.form.OrderNum = res.data.data.OrderNum;
                    this.form.OnShow = res.data.data.OnShow;
                    this.form.HotCity = res.data.data.HotCity;
                    this.dialogVisible = true;
                })
                .catch((ex) => {
                    alert(ex.Message);
                });
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
                return "sucess";
            } else {
                return "danger";
            }
        },
        setCache: function () {
            axiosGet(api.config + "SetBasicCityCache")
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
        onShow: function (val) {
            if (val) {
                return "显示";
            } else {
                return "不显示";
            }
        }
    }
});