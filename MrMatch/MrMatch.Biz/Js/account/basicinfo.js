var vm = new Vue({
    el: "#appBasicInfo",
    data: {
        basicInfo: {
            CompanyName:'',
            Email:'',
            CellPhone:'',
            AccountName:'',
            Position:'',
            IsAdmin:false,
            CreatedTime:'',
            PKID: 0,
            AvatarUrl: "",
            WechatContactUrl:""
        },
        form: {
            AccountName: "",
            Position: "",
            AvatarBytes: "",
            WechatAccount: "",
            WechatContactBytes: "",
            LinkinUrl: "",
            FocusArea: "",
            Introduction: ""
        },
        validate: {
            AccountName: {
                isPass: false,
                message: '称呼不能为空'
            },
            Position: {
                isPass: false,
                message: '职位不能为空'
            }
        },
        confirmModalText: '',
        submitLoading: false,
        preUpload: {
            avartar: "",
            avartarShow: false,
            wechat: "",
            wechatShow: false,
        },
        imgT: "data:image/gif;base64,"
    },
    mounted: function () {
        this.getBasicInfo();
    },
    methods: {
        getBasicInfo: function () {
            axiosGet(api.account + "GetBasicInfo")
                .then((res) => {
                    console.log(res);
                    this.basicInfo.CompanyName = res.data.CompanyName;
                    this.basicInfo.Email = res.data.Email;
                    this.basicInfo.CellPhone = res.data.CellPhone;
                    this.basicInfo.AccountName = res.data.AccountName;
                    this.basicInfo.Position = res.data.Position;
                    this.basicInfo.IsAdmin = res.data.IsAdmin;
                    this.basicInfo.CreatedTime = res.data.CreatedTime;
                    this.basicInfo.PKID = res.data.PKID;
                    this.basicInfo.AvatarUrl = res.data.AvatarUrl;
                    this.basicInfo.WechatContactUrl = res.data.WechatContactUrl;

                    this.form.AccountName = res.data.AccountName;
                    this.form.Position = res.data.Position;
                    this.form.FocusArea = res.data.FocusArea;
                    this.form.Introduction = res.data.Introduction;
                    this.form.WechatAccount = res.data.WechatAccount;
                    this.form.LinkinUrl = res.data.LinkinUrl;

                    this.preUpload.avartarShow = false;
                    this.preUpload.wechatShow = false;
                    this.preUpload.avartar = "";
                    this.preUpload.wechat = "";
                })
        },
        // 提交表单
        submitForm: function (form, sta) {
            this.form.AvatarBytes = this.preUpload.avartar;
            this.form.WechatContactBytes = this.preUpload.wechat;
            if (!this.validateForm()) {
                this.$confirm(this.confirmModalText, '提示', {
                    confirmButtonText: '确定',
                    showCancelButton: false
                })
                return
            }
            this.submitLoading = true;
            axiosPost(api.account + "UpdateBasicInfo", this.form)
                .then((res) => {
                    this.submitLoading = false;
                    this.$confirm(res.msg, '提示', {
                        confirmButtonText: '确定',
                        showCancelButton: false
                    })
                })
                .catch((ex) => {
                    this.submitLoading = false;
                    this.$confirm('提交失败，请重试', '提示', {
                        confirmButtonText: '确定',
                        showCancelButton: false
                    })
                });
        },
        // 表单验证
        validateForm: function () {
            var validate = this.validate
            var form = this.form
            for (var x in validate) {
                if (!form[x]) {
                    this.validate[x].isPass = true
                    this.confirmModalText = validate[x].message

                    return false
                } else {
                    this.validate[x].isPass = false
                }
            }

            return true
        },
        beforeAvatarUploadA(file) {
            var img = "";
            var reader = new FileReader();
            reader.readAsDataURL(file);
            reader.onload = (e) => {
                img = e.target.result;
                console.log(img.split(','));
                this.preUpload.avartar = img.split(',')[1];
                this.preUpload.avartarShow = true;

            }

            return false;
        },
        beforeAvatarUploadW(file) {
            var img = "";
            var reader = new FileReader();
            reader.readAsDataURL(file);
            reader.onload = (e) => {
                img = e.target.result;
                console.log(img.split(','));
                this.preUpload.wechat = img.split(',')[1];
                this.preUpload.wechatShow = true;

            }
            return false;
        }
    },
    filters: {
        dateFormat: function (val) {
            console.log(val)
            return val.split('T')[0];
        }
    }
})