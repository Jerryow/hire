var vm = new Vue({
    el: "#appMsgConfigItem",
    data() {
        return {
            labelPosition: "top",
            options: [
                { value: 10, label: "短信" },
                { value: 20, label: "邮件" }
            ],
            aupdateForm: {
                PKID: "",
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
        this.getInfo();
    },
    methods: {
        getInfo: function () {
            axiosGet(api.system + 'GetMessageConfigById',
                {
                    PKID: getUlrKey("id")
                })
                .then((res) => {
                    if (res.data.code == "1") {
                        this.aupdateForm = res.data.data;
                    }

                })
                .catch((ex) => {
                    console.log(ex);
                });

        },
        submitForm(form) {
            this.$refs[form].validate((valid) => {
                if (valid) {
                    axiosPost(api.system + "UpdateMessageConfig", this.aupdateForm)
                        .then((res) => {
                            if (res.data.code == "1") {
                                alert(res.data.msg);
                                this.reload1();
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
        reload1: function () {
            window.location.href = getFullUlr();
        },
        redirect: function () {
            window.location.href = "/system/MessageConfig";
        },
        changes: function () {
            console.log(this.aupdateForm.ConfigType);
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
        }
    }
});