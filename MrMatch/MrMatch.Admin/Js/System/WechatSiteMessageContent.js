var vm = new Vue({
    el: "#appMsgContent",
    data() {
        return {
            form: {
                PKID: "",
                MessageID: "",
                MessageContent: ""
            },
            rules: {
                MessageContent: [
                    { required: true, message: '消息内容不能为空.', trigger: 'blur' }
                ]
            },
            btnShow: {
                addShow: false,
                updateShow: false
            },
            labelPos: "top"
        };
    },
    mounted: function () {
        this.checkExist();
    },
    methods: {
        checkExist: function () {
            var id = getUlrKey("id");
            this.form.MessageID = id;
            axiosGet(api.message + "CheckIfExistContentInfo",
                {
                    "PKID": id
                })
                .then((res) => {
                    if (res.data.code == "1") {
                        this.btnShow.addShow = false;
                        this.btnShow.updateShow = true;
                        this.form.PKID = res.data.data.PKID;
                        this.form.MessageID = res.data.data.MessageID;
                        this.form.MessageContent = res.data.data.MessageContent;
                    } else {
                        this.btnShow.addShow = true;
                        this.btnShow.updateShow = false;
                    }
                });
        },
        submitForm: function (form) {
            this.$refs[form].validate((valid) => {
                if (valid) {
                    axiosPost(api.message + "AddMsgContent", this.form)
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
                    axiosPost(api.message + "UpdateMsgContent", this.form)
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
        redirectIndex: function () {
            window.location.href = "/message/index";
        }
    }
});