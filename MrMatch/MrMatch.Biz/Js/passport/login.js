var vm = new Vue({
    el: "#appLogin",
    data: {
        username: '',
        verifycode: '',
        timer: 60,
        isSending: false,
        hint: '',
        checkboxAgreement: false,
        automaticLogin: false,
        isQRCodeLogin: false,
        loading: {
            sendLoading: false,
            loginLoading: false,
        },
        imgVerOption: {
            InitImage: '',
            OffsetX: '',
            Sign: '',
            VOffset: ''
        }
    },
    mounted: function () {
        if (getUrlKey("cellphone")) {
            this.username = getUrlKey("cellphone");
        }
    },
    methods: {
        loadImgVer() {
            let _this = this
            // 验证手机号
            let isPhone = this.usernameVerify(this.username)


            if (isPhone && !this.loading.sendLoading) {

                $('body').append(` <div class="verBox">
                    <div id="imgVer" style="display:inline-block;"></div>
                    <div class="verBox-ghost"></div>
                </div>`)

                window.imgVer({
                    el: $("#imgVer"),
                    width: '260',
                    height: '160',
                    offsetx: this.imgVerOption.OffsetX,
                    voffset: this.imgVerOption.VOffset,
                    img: [
                        this.imgVerOption.InitImage
                    ],
                    success(res) {
                        _this.getVerifyCode(res.moveEnd_X);
                    },
                    error() {
                        //alert('错误执行')
                        console.log('error')
                    }
                });
            }
        },
        getImage: function () {
            let _this = this
            // 验证手机号
            let isPhone = this.usernameVerify(this.username)


            if (isPhone && !this.loading.sendLoading) {
                axios.get(api.passport + "GetPicInfo", { params: { phone: this.username } })
                    .then((res) => {
                        if (res.data.code == 1) {
                            this.imgVerOption.InitImage = res.data.data.InitImage
                            this.imgVerOption.OffsetX = res.data.data.OffsetX
                            this.imgVerOption.Sign = res.data.data.Sign
                            this.imgVerOption.VOffset = res.data.data.VOffset

                            this.loadImgVer()
                        }
                    })
            }
        },
        // 输入手机号
        usernameChange(e) {
            let value = e.target.value
            this.username = value
        },
        // 获取验证码
        getVerifyCode(moveEnd_X) {
            let _this = this
            // 验证手机号
            let isPhone = this.usernameVerify(this.username)

            if (isPhone && !this.loading.sendLoading) {

                axios.post(api.passport + "SendVerifyCode", {
                    PhoneNumber: this.username,
                    UserOffsetX: moveEnd_X,
                    UserSign: this.imgVerOption.Sign
                })
                    .then((res) => {
                        if (res.data.code == 1) {
                            this.loading.sendLoading = true;
                            // 验证码计时
                            if (isPhone) {
                                _this.isSending = true
                                let currentTime = _this.timer
                                let interval = setInterval(function () {
                                    currentTime--;
                                    _this.timer = currentTime
                                    if (currentTime <= 0) {
                                        clearInterval(interval)
                                        _this.isSending = false
                                        _this.loading.sendLoading = false
                                        _this.timer = 60
                                    }
                                }, 1000)
                            }
                        } else {
                            this.$alert('验证码发送失败', '提示');
                        }
                    })
            }
        },
        // 输入验证码
        verifycodeChange(e) {
            let value = e.target.value
            this.verifycode = value
        },
        // 点击登录
        loginSubmit() {
            let _this = this

            let isPhone = this.usernameVerify(this.username)
            let isCode = isPhone ? this.verifycodeVerify(this.verifycode) : false


            if (isCode && !this.loading.loginLoading) {

                if (!this.checkboxAgreement) {
                    this.$alert('请勾选同意 猫奇使用协议、 隐私政策', '提示');
                    return;
                }

                this.loading.loginLoading = true
                // 登录操作
                axios.post(api.passport + 'LoginMobile', {
                    CellPhone: this.username,
                    VerifyCode: this.verifycode,
                    AutoLogin: this.automaticLogin
                }).then((res) => {
                    var json = JSON.parse(res.data);
                    if (json.IsOK) {
                        var retUrl = getUrlKey("fromurl");
                        if (retUrl) {
                            location.href = retUrl;
                        } else {
                            location.href = json.ReturnUrl
                        }
                    } else {
                        this.loading.loginLoading = false
                        this.$alert(json.Msg, '提示');
                    }
                });
            }
        }, 
        // 勾选用户协议
        checkboxClickAgreement(e) {
            this.checkboxAgreement = e.target.checked
        },
        //  选择自动登录
        checkboxClick(e) {
            this.automaticLogin = e.target.checked
        },
        // 手机验证
        usernameVerify(value) {
            if (value == '') {
                this.hint = '请输入手机号'
                return false
            } else if (!ChkUtil.isMobile(value)) {
                this.hint = '请输入正确的手机号'
                return false
            } else {
                this.hint = ''
                return true
            }
        },
        // code验证
        verifycodeVerify(value) {
            if (value == '') {
                this.hint = '请输入验证码'
                return false
            } else if (!ChkUtil.isDigit(value) || value.length !== 4) {
                this.hint = '请输入正确的验证码'
                return false
            } else {
                this.hint = ''
                return true
            }
        },
        // 收不到验证码
        unReceiveCode: function () {
            this.$alert('<div style="text-align:left"><p>1）检查手机是否启用了短信拦截；</p><p>2）检查短信手机安心是否需要清理；</p ><p>3）部分手机由于所在网络问题，可能需要尝试重启以及重装SIM卡；</p><p>4）所在地区信号可能不稳定，需要等待一段时间，或联系当地运营商查询。</p></div>', '收不到验证码？', {
                confirmButtonText: '确定',
                dangerouslyUseHTMLString: true,
                confirmButtonClass:'unReceiveBtn'
            });
        }
    }
})