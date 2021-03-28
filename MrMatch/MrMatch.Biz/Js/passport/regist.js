var vm = new Vue({
    el: "#appRegist",
    data: {
        registArgs: {
            Email: '',
            CellPhone: '',
            EmailVerifyCode: '',
            PhoneVerifyCode: ''
        },
        timer: 60,
        EmailTimer: 60,
        hint: '',
        automaticLogin: false,
        isQRCodeLogin: false,
        isSendCode: false,
        isSendEmailCode: false,
        checkboxAgreement: false,
        loading: {
            sendLoading: false,
            sendEmailLoading: false,
            registLoading: false,
        },
        imgVerOption: {
            InitImage: '',
            OffsetX: '',
            Sign: '',
            VOffset: ''
        }
    },
    methods: {
        loadImgVer() {
            let _this = this
            // 验证手机号
            let isPhone = this.usernameVerify(this.registArgs.CellPhone)


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
            let isPhone = this.usernameVerify(this.registArgs.CellPhone)


            if (isPhone && !this.loading.sendLoading) {
                axios.get(api.passport + "GetPicInfo", { params: { phone: this.registArgs.CellPhone } })
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
        // 获取验证码
        getVerifyCode(moveEnd_X) {
            let _this = this
            // 验证手机号
            let isPhone = this.usernameVerify(this.registArgs.CellPhone)

            if (isPhone && !this.loading.sendLoading) {

                axios.post(api.passport + "SendVerifyCode", { PhoneNumber: this.registArgs.CellPhone, UserOffsetX: moveEnd_X, UserSign: this.imgVerOption.Sign })
                    .then((res) => {
                        res = res.data
                        this.loading.sendLoading = true;
                        // 验证码计时
                        if (isPhone) {
                            _this.isSendCode = true
                            let currentTime = _this.timer
                            let interval = setInterval(function () {
                                currentTime--;
                                _this.timer = currentTime
                                if (currentTime <= 0) {
                                    clearInterval(interval)
                                    _this.isSendCode = false
                                    _this.loading.sendLoading = false
                                    _this.timer = 60
                                }
                            }, 1000)
                        }
                    })
            }
        },
        // 获取邮箱验证码
        getEmailVerifyCode() {
            let _this = this
            // 验证手机号
            let isEmail = this.emailVerify(this.registArgs.Email)

            if (isEmail && !this.loading.sendEmailLoading) {

                axios.get(api.passport + "SendEmailVerifyCode", { params: { accountEmail: this.registArgs.Email } })
                    .then((res) => {
                        this.loading.sendEmailLoading = true;

                        // 验证码计时
                        if (isEmail) {
                            _this.isSendEmailCode = true
                            let currentTime = _this.EmailTimer
                            let interval = setInterval(function () {
                                currentTime--;
                                _this.EmailTimer = currentTime
                                if (currentTime <= 0) {
                                    clearInterval(interval)
                                    _this.isSendEmailCode = false
                                    _this.loading.sendEmailLoading = false
                                    _this.EmailTimer = 60
                                }
                            }, 1000)
                        }
                    })
            }
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
        // 邮箱验证
        emailVerify(value) {
            if (value == '') {
                this.hint = '请输入邮箱'
                return false
            } else if (!ChkUtil.isEmail(value)) {
                this.hint = '请输入正确的邮箱'
                return false
            } else {
                this.hint = ''
                return true
            }
        },
        // code验证
        verifycodeVerify(value) {
            if (value == '') {
                this.hint = '请输入手机验证码'
                return false
            } else if (!ChkUtil.isDigit(value) || value.length !== 4) {
                this.hint = '请输入正确的手机验证码'
                return false
            } else {
                this.hint = ''
                return true
            }
        },
        // emailcode验证
        verifyEmailCodeVerify(value) {
            if (value == '') {
                this.hint = '请输入邮箱验证码'
                return false
            } else if (!ChkUtil.isDigit(value) || value.length !== 4) {
                this.hint = '请输入正确的邮箱验证码'
                return false
            } else {
                this.hint = ''
                return true
            }
        },
        // 点击注册
        registSubmit() {
            let _this = this

            let isPhone = this.usernameVerify(this.registArgs.CellPhone)
            console.log(isPhone)
            let isPhoneCode = isPhone ? this.verifycodeVerify(this.registArgs.PhoneVerifyCode) : false
            let isEmail = isPhoneCode ? this.emailVerify(this.registArgs.Email) : false
            let isEmailCode = isEmail ? this.verifyEmailCodeVerify(this.registArgs.EmailVerifyCode) : false

            if (isEmailCode) {

                if (!this.checkboxAgreement) {
                    this.$alert('请勾选同意 猫奇使用协议、 隐私政策', '提示');
                    return;
                }

                this.loading.registLoading = true
                // 注册操作
                axios.post(api.passport + 'Regist', this.registArgs).then((res) => {
                    this.loading.registLoading = false
                    var json = JSON.parse(res.data);
                    if (json.IsOK) {
                        location.href = json.ReturnUrl
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

        // 收不到验证码
        unReceiveCode: function () {
            this.$alert('<div style="text-align:left"><p>1）检查手机是否启用了短信拦截；</p><p>2）检查短信手机安心是否需要清理；</p ><p>3）部分手机由于所在网络问题，可能需要尝试重启以及重装SIM卡；</p><p>4）所在地区信号可能不稳定，需要等待一段时间，或联系当地运营商查询。</p></div>', '收不到验证码？', {
                confirmButtonText: '确定',
                dangerouslyUseHTMLString: true,
                confirmButtonClass: 'unReceiveBtn'
            });
        }
    }
})