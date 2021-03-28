var vm = new Vue({
    el: "#appMsgConfigItem",
    data() {
        return {
            labelPosition: "top",
            aupdateForm: {
                PKID: "",
                TemplateCode: "",
                TemplateTitle: "",
                TemplateContent: "",
                IsActivated: false
            },
            rules: {
                TemplateCode: [
                    { required: true, message: '模板编码不能为空.', trigger: 'blur' }
                ],
                TemplateTitle: [
                    { required: true, message: '模板标题不能为空.', trigger: 'blur' }
                ],
                TemplateContent: [
                    { required: true, message: '模板内容不能为空.', trigger: 'blur' }
                ]
            }
        };
    },
    mounted: function () {
        this.getInfo();
    },
    methods: {
        getInfo: function () {
            axiosGet(api.system + 'GetMessageTemplateById',
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
                    axiosPost(api.system + "UpdateMessageTemplate", this.aupdateForm)
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
            window.location.href = "/system/MessageTemplate";
        },
        changes: function () {
            console.log(this.aupdateForm.ConfigType);
        }
    },
    filters: {
    }
});