'use strict';

var vm = new Vue({
    el: "#appLogin",
    data: function data() {
        return {
            username: '',
            verifycode: '',
            timer: 60,
            isSending: false,
            sendLoading: false,
            loginLoading: false,
            verifyImgLoading: false,
            hint: '',
            checkboxAgreement: false,
            automaticLogin: false,
            isQRCodeLogin: false,
            imgVerOption: {
                InitImage: '',
                OffsetX: '',
                Sign: '',
                VOffset: ''
            }
        };
    },

    mounted: function mounted() {

        if (getUrlKey("cellphone")) {
            this.username = getUrlKey("cellphone");
        }
    },
    methods: {
        loadImgVer: function loadImgVer() {
            var _this = this;
            // 验证手机号
            var isPhone = this.usernameVerify(this.username);

            if (isPhone && !this.sendLoading) {

                $('body').append(' <div class="verBox">\n                    <div id="imgVer" style="display:inline-block;"></div>\n                    <div class="verBox-ghost"></div>\n                </div>');

                window.imgVer({
                    el: $("#imgVer"),
                    width: '260',
                    height: '160',
                    offsetx: this.imgVerOption.OffsetX,
                    voffset: this.imgVerOption.VOffset,
                    img: [this.imgVerOption.InitImage],
                    success: function success(res) {
                        _this.getVerifyCode(res.moveEnd_X);
                    },
                    error: function error() {
                        //alert('错误执行')
                        console.log('error');
                    }
                });
            }
        },

        getImage: function getImage() {
            var _this2 = this;

            // 验证手机号
            var isPhone = this.usernameVerify(this.username);

            if (isPhone && !this.sendLoading && !this.verifyImgLoading) {
                this.verifyImgLoading = true;
                axios.get(api.passport + "GetPicInfo", {
                    params: {
                        phone: this.username
                    }
                }).then(function (res) {
                    _this2.verifyImgLoading = false;
                    if (res.data.code == 1) {
                        _this2.imgVerOption.InitImage = res.data.data.InitImage;
                        _this2.imgVerOption.OffsetX = res.data.data.OffsetX;
                        _this2.imgVerOption.Sign = res.data.data.Sign;
                        _this2.imgVerOption.VOffset = res.data.data.VOffset;

                        var img = new Image();
                        img.onload = function () {
                            _this2.loadImgVer();
                        };
                        img.onerror = function () {
                            _this2.$message.success('校验图片加载失败');
                        };
                        img.src = _this2.imgVerOption.InitImage;
                    }
                });
            }
        },
        // 输入手机号
        usernameChange: function usernameChange(e) {
            var value = e.target.value;
            this.username = value;
        },

        // 获取验证码
        getVerifyCode: function getVerifyCode(moveEnd_X) {
            var _this3 = this;

            var _this = this;
            // 验证手机号
            var isPhone = this.usernameVerify(this.username);

            if (isPhone && !this.sendLoading) {

                axios.post(api.passport + "SendVerifyCode", {
                    PhoneNumber: this.username,
                    UserOffsetX: moveEnd_X,
                    UserSign: this.imgVerOption.Sign
                }).then(function (res) {
                    _this3.sendLoading = true;

                    // 验证码计时
                    if (res.data.code === '1') {
                        _this3.isSending = true;
                        _this3.$message.success(res.data.msg);

                        var currentTime = _this.timer;
                        var interval = setInterval(function () {
                            currentTime--;
                            _this.timer = currentTime;
                            if (currentTime <= 0) {
                                clearInterval(interval);
                                _this.isSending = false;
                                _this.sendLoading = false;
                                _this.timer = 60;
                            }
                        }, 1000);
                    } else {
                        _this3.$message.error(res.data.msg);
                    }
                });
            }
        },

        // 输入验证码
        verifycodeChange: function verifycodeChange(e) {
            var value = e.target.value;
            this.verifycode = value;
        },

        // 点击登录
        loginSubmit: function loginSubmit() {
            var _this4 = this;

            var _this = this;

            var isPhone = this.usernameVerify(this.username);
            var isCode = isPhone ? this.verifycodeVerify(this.verifycode) : false;

            if (isCode && !this.loginLoading) {

                if (!this.checkboxAgreement) {
                    this.$alert('请勾选同意 猫奇使用协议、 隐私政策', '提示');
                    return;
                }

                this.loginLoading = true;

                // 登录操作

                axiosPost(api.passport + "LoginMobile", {
                    CellPhone: this.username,
                    VerifyCode: this.verifycode,
                    AutoLogin: this.automaticLogin
                }).then(function (res) {
                    var data = JSON.parse(res);
                    local.set('userinfo', {
                        NickName: data.NickName,
                        PKID: data.PKID,
                        AvatarUrl: data.AvatarUrl
                    });
                    //return
                    if (data.IsOK) {

                        var retUrl = getUrlKey("fromurl");
                        if (retUrl) {
                            location.href = retUrl;
                        } else {
                            location.href = "/resume/index";
                        }
                    } else {
                        _this4.$message.error(data.Msg);
                        _this4.loginLoading = false;
                    }
                });
            }
        },

        // 勾选用户协议
        checkboxClickAgreement: function checkboxClickAgreement(e) {
            this.checkboxAgreement = e.target.checked;
        },

        //  选择自动登录
        checkboxClick: function checkboxClick(e) {
            this.automaticLogin = e.target.checked;
        },

        // 手机验证
        usernameVerify: function usernameVerify(value) {
            if (value == '') {
                this.hint = '请输入手机号';
                return false;
            } else if (!ChkUtil.isMobile(value)) {
                this.hint = '请输入正确的手机号';
                return false;
            } else {
                this.hint = '';
                return true;
            }
        },

        // code验证
        verifycodeVerify: function verifycodeVerify(value) {
            if (value == '') {
                this.hint = '请输入验证码';
                return false;
            } else if (!ChkUtil.isDigit(value) || value.length !== 4) {
                this.hint = '请输入正确的验证码';
                return false;
            } else {
                this.hint = '';
                return true;
            }
        },

        QRCodeLogin: function QRCodeLogin() {
            this.isQRCodeLogin = !this.isQRCodeLogin;
            this.WebSocket();
        },
        // 收不到验证码
        unReceiveCode: function unReceiveCode() {
            this.$alert('<div style="text-align:left"><p>1）检查手机是否启用了短信拦截；</p><p>2）检查短信手机安心是否需要清理；</p ><p>3）部分手机由于所在网络问题，可能需要尝试重启以及重装SIM卡；</p><p>4）所在地区信号可能不稳定，需要等待一段时间，或联系当地运营商查询。</p></div>', '收不到验证码？', {
                confirmButtonText: '确定',
                dangerouslyUseHTMLString: true,
                confirmButtonClass: 'btn-theme'
            });
        },
        WebSocket: function (_WebSocket) {
            function WebSocket() {
                return _WebSocket.apply(this, arguments);
            }

            WebSocket.toString = function () {
                return _WebSocket.toString();
            };

            return WebSocket;
        }(function () {

            var key = "";
            var ws = null;
            var data = {
                TypeCode: "",
                UserID: 0,
                Message: ""
            };
            if ("WebSocket" in window) {
                //alert("您的浏览器支持 WebSocket!");
                console.log("This browser supports WebSocket!");

                // 打开一个 web socket
                //ws = new WebSocket("ws://mm.wss.deetoo.com/websocket");
                //ws = new WebSocket("ws://mm.wss.deetoo.com/WSSHandler.ashx?user=abc");
                ws = new WebSocket("wss://websocket.mrmatch.net/api/wechatqr/GetQRConnection");
                ws.onopen = function () {
                    // Web Socket 已连接上，使用 send() 方法发送数据
                    console.log("Web Socket 已连接上");
                    //$('#ErrorLog').html("Web Socket 已连接上");
                    var initData = data;
                    initData.TypeCode = "1000";
                    initData.UserID = 0;
                    initData.Message = "";
                    ws.send(JSON.stringify(initData));
                };

                ws.onmessage = function (evt) {
                    console.log(evt);
                    var msg = JSON.parse(evt.data);
                    // alert("数据已接收...");


                    switch (msg.TypeCode) {
                        case "1000":
                            var htm = '<img src="/api/passportapi/loginqrcode/?keycode=' + msg.Message + '" />';
                            $('#QRCode').html(htm);
                            break;
                        case "1001":
                            $('#ErrorLog').html("扫码成功");
                            break;
                        case "1002":
                            //登录验证及跳转
                            console.log(msg.TypeCode);
                            //alert(msg.TypeCode + ' - ' + msg.OpenID);
                            console.log(msg.TypeCode + ' - ' + msg.UserID);
                            $('#ErrorLog').html("扫码成功");
                            if (msg.UserID != "" && msg.UserID != null) {
                                //return false;
                                $.ajax({
                                    type: "post",
                                    //datatype: "jsonp",
                                    //contentType: "application/json;charset=utf-8",
                                    //url: "/api/passportapi/LoginVerify",
                                    //data: {
                                    //    "OpenID": msg.OpenID
                                    //},
                                    url: "/api/passportapi/LoginQR",
                                    data: {
                                        "PKID": msg.UserID,
                                        "ReturnUrl": ""
                                        //"UniCode": "64067d7d-4ace-47c4-8880-d1c1161d86f1"
                                    },
                                    success: function success(data) {
                                        console.log(data);
                                        var res = JSON.parse(data);

                                        local.set('userinfo', {
                                            NickName: res.NickName,
                                            PKID: res.PKID,
                                            AvatarUrl: res.AvatarUrl
                                        });
                                        $('#ErrorLog').html(res.Msg);
                                        if (res.IsOK) {
                                            window.location.href = res.ReturnUrl;
                                        }
                                    },
                                    error: function error(XMLHttpRequest, textStatus, errorThrown) {
                                        $('#ErrorLog').html(textStatus + ' - ' + XMLHttpRequest.status + ' - ' + errorThrown + '。');
                                    }
                                });
                            }
                            break;
                        default:
                            $('#ErrorLog').html(msg.DataContent);
                            break;
                    }
                };

                ws.onerror = function () {
                    var htm = '<i class="fa fa-qrcode" style="font-size:80px; line-height:150px; color:#ddd;"></i>';
                    $('#QRCode').html(htm);
                    $('#ErrorLog').html("二维码已失效<br />请刷新后重新获取。");
                };
                ws.onclose = function () {
                    // 关闭 websocket
                    //alert("连接已关闭...");
                    var htm = '<i class="fa fa-qrcode" style="font-size:80px; line-height:150px; color:#ddd;"></i>';
                    $('#QRCode').html(htm);
                    $('#ErrorLog').html("二维码已失效<br />请刷新后重新获取。");
                };
            } else {
                console.log("This browser does not support WebSocket.");
            }
        })

    }
});