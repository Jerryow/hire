var vm = new Vue({
    el: "#appSysUser",
    data() {
        var checkEmail = (rule, value, callback) => {
            if (!value) {
                return callback(new Error('邮箱不能为空'));
            }
            var reg = /^[A-Za-z\d]+([-_.][A-Za-z\d]+)*@([A-Za-z\d]+[-.])+[A-Za-z\d]{2,4}$/;
            if (!reg.test(value)) {
                callback(new Error('请输入正确的邮箱'));
            } else {
                callback();
            }
        };
        return {
            aotuCom: "Off",
            btnShow: {
                addShow: false,
                updateShow: false
            },
            btnDisable: {
                delDialog: false
            },
            diaClose: false,
            getArgs: {
                pageIndex: "1",
                pageSize: "10",
                keyWords: ""
            },
            selectSize: "10",
            sizes: [10, 20, 30, 50, 100],
            userList: [],
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
                LoginName: "",
                UserName: "",
                Email: "",
                Password: "",
                OnInternal: false,
                OnActive: false,
                Remark: ""
            },
            labelPosition: "left",
            deleteId: "",
            rules: {
                LoginName: [
                    { required: true, message: '登录名不能为空.', trigger: 'blur' }
                ],
                UserName: [
                    { required: true, message: '真实姓名不能为空.', trigger: 'blur' }
                ],
                Email: [
                    { required: true, message: '请输入正确的邮箱地址.', trigger: 'blur' },
                    { validator: checkEmail, trigger: 'blur' }
                ],
                Password: [
                    { required: true, message: '密码不能为空.', trigger: 'blur' }
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
            axiosGet(api.system + 'GetSysUserByPagenation', para)
                .then((datas) => {
                    var data = datas.data.data;
                    if (data.DataList.length > 0) {
                        this.userList = data.DataList;
                        for (var i = 0; i < this.userList.length; i++) {
                            if (this.userList[i].OnActive) {
                                this.userList[i].OnActive = '<i class="fas fa-check text-blue"></i>';
                            } else {
                                this.userList[i].OnActive = '<i class="fas fa-close text-gray"></i>';
                            }
                            if (this.userList[i].OnInternal) {
                                this.userList[i].OnInternal = '<i class="fas fa-check text-blue"></i>';
                            } else {
                                this.userList[i].OnInternal = '<i class="fas fa-close text-gray"></i>';
                            }
                        }
                        this.fillPagination(data);
                    } else {
                        this.userList = [];
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
                    axiosPost(api.system + "AddUser", this.form)
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
                    axiosPost(api.system + "UpdateUser", this.form)
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
        formInit: function () {
            this.form.LoginName = "";
            this.form.UserName = "";
            this.form.Email = "";
            this.form.Password = "";
            this.form.OnInternal = false;
            this.form.OnActive = false;
            this.form.Remark = "";
        },
        deleteUserDia: function (pkid) {
            this.btnDisable.delDialog = false;
            this.dialogDelete = true;
            this.result = "确定要注销此用户吗？";
            this.deleteId = pkid;
        },
        deleteUser: function () {
            this.result = "";
            this.loading = true;
            this.btnDisable.delDialog = true;
            axiosGet(api.system + "DestroyUser",
                {
                    PKID: this.deleteId
                })
                .then((res) => {
                    this.loading = false;
                    this.result = res.data.msg;
                    setTimeout(() => {
                        window.location.reload();
                    }, 700);

                })
                .catch((ex) => {
                    alert(ex.Message);
                });
        },
        updateUserFill: function (pkid, sta) {
            this.btnShow.addShow = false;
            this.btnShow.updateShow = false;
            if (sta == "update") {
                this.btnShow.updateShow = true;
            }
            axiosGet(api.system + "GetUserById",
                {
                    PKID: pkid
                })
                .then((res) => {
                    this.form.PKID = res.data.data.PKID;
                    this.form.LoginName = res.data.data.LoginName;
                    this.form.Password = res.data.data.Password;
                    this.form.Email = res.data.data.Email;
                    this.form.UserName = res.data.data.UserName;
                    this.form.OnInternal = res.data.data.OnInternal;
                    this.form.OnActive = res.data.data.OnActive;
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