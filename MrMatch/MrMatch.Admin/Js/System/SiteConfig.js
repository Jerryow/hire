var vm = new Vue({
    el: "#appSysConfig",
    data() {
        return {
            table: [],
            form: {
                ConfigName: "",
                ConfigCode: "",
                ConfigValue:""
            },
            btnDisable: {
                delDialog: false
            },
            btnShow: {
                addShow: false,
                updateShow: false
            },
            diaClose: false,
            dialogVisible: false,
            labelPosition: "left",
            dialogDelete: false,
            rules: {
                ConfigName: [
                    { required: true, message: '名称不能为空.', trigger: 'blur' }
                ],
                ConfigCode: [
                    { required: true, message: '编号不能为空.', trigger: 'blur' }
                ],
                ConfigValue: [
                    { required: true, message: '内容不能为空.', trigger: 'blur' }
                ]
            }
        };
    },
    mounted: function () {
        this.getList();
    },
    methods: {
        getList: function () {
            axiosGet(api.system + 'GetSiteConfigList')
                .then((res) => {
                    var data = res.data.data;
                    if (data.length > 0) {
                        this.table = data;
                    } else {
                        this.table = [];
                    }
                })
                .catch((ex) => {
                    console.log(ex);
                });

        },
        add: function () {
            this.dialogVisible = true;
            this.btnShow.addShow = true;
            this.btnShow.updateShow = false;
            this.form.ConfigName = "";
            this.form.ConfigCode = "";
            this.form.ConfigValue = "";
        },
        update: function (code) {
            axiosGet(api.system + "GetSiteConfigByCode",
                {
                    configCode: code
                })
                .then((res) => {
                    this.form.PKID = res.data.data.PKID;
                    this.form.ConfigName = res.data.data.ConfigName;
                    this.form.ConfigCode = res.data.data.ConfigCode;
                    this.form.ConfigValue = res.data.data.ConfigValue;
                    this.btnShow.addShow = false;
                    this.btnShow.updateShow = true;
                    this.dialogVisible = true;
                })
                .catch((ex) => {
                    alert(ex.Message);
                });
        },
        updateConfig: function (form) {
            this.$refs[form].validate((valid) => {
                if (valid) {
                    axiosPost(api.system + "UpdateSiteConfig", this.form)
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
        addConfig: function (form) {
            this.$refs[form].validate((valid) => {
                if (valid) {
                    axiosPost(api.system + "AddSiteConfig", this.form)
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
        setCache: function (PKID) {
            axiosGet(api.system + "SetSiteConfigCache?PKID=" + PKID)
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
        },
        closeModal: function () {
            this.dialogVisible = false;
            this.result = "";
        },
        refresh: function () {
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
        }
    }
});
