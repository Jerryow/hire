"use strict";

var key = "";
var ws = null;
var num = RndNum(10);
var data = {
    TypeCode: "",
    UserID: 0,
    Message: ""
};
if ("WebSocket" in window) {
    //alert("您的浏览器支持 WebSocket!");
    console.log("This browser supports WebSocket!");

    // 打开一个 web socket
    ws = new WebSocket("ws://192.168.1.128:8099/api/wechatqr/GetQRConnection");
    //ws = new WebSocket("ws://mm.wss.deetoo.com/WSSHandler.ashx?user=abc");
    //ws = new WebSocket("wss://wss.mrmatch.net/websocket");
    ws.onopen = function () {
        // Web Socket 已连接上，使用 send() 方法发送数据
        console.log("Web Socket 已连接上");
        //$('#ErrorLog').html("Web Socket 已连接上");
        var initData = data;
        initData.TypeCode = "1000";
        initData.UserID = 0;
        initData.Message = "";
        tmpData.dataContent = "New Connected!";
        ws.send(JSON.stringify(tmpData));
    };

    ws.onmessage = function (evt) {
        console.log(evt);
        var msg = JSON.parse(evt.data);
        // alert("数据已接收...");
        console.log(msg + ' - ' + msg.TypeCode);

        switch (msg.TypeCode) {
            case 1000:
                var htm = '<img src="/api/passportapi/loginqrcode/?keycode=' + msg.DataContent + '" />';
                $('#QRCode').html(htm);
                break;
            case 1001:
                $('#ErrorLog').html("扫码成功");
                break;
            case 1002:
                //登录验证及跳转
                console.log(msg.TypeCode);
                //alert(msg.TypeCode + ' - ' + msg.OpenID);
                console.log(msg.TypeCode + ' - ' + msg.UserCode);
                $('#ErrorLog').html("扫码成功");
                if (msg.UserCode != "" && msg.UserCode != null) {
                    //return false;
                    $.ajax({
                        type: "post",
                        //datatype: "jsonp",
                        //contentType: "application/json;charset=utf-8",
                        //url: "/api/passportapi/LoginVerify",
                        //data: {
                        //    "OpenID": msg.OpenID
                        //},
                        url: "/api/passportapi/LoginVerifyMobile",
                        data: {
                            "UniCode": msg.UserCode
                            //"UniCode": "64067d7d-4ace-47c4-8880-d1c1161d86f1"
                        },
                        success: function success(data) {
                            var res = JSON.parse(data);
                            alert(res.Msg);
                            if (res.IsOK) {
                                window.location.href = res.ReturnUrl;
                            }
                            console.log(data + ' - ' + msg.UserCode);
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

function RndNum(n) {
    var rnd = "";
    for (var i = 0; i < n; i++) {
        rnd += Math.floor(Math.random() * 10);
    }return rnd;
}

function toSend(val) {
    var tmpData = data;
    tmpData.typeCode = 1003;
    tmpData.dataContent = val;
    console.log(JSON.stringify(tmpData));
    ws.send(JSON.stringify(tmpData));
}