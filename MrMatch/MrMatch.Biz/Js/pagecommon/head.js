var vm = new Vue({
    el: "#appHead",
    data: {
        isShow: false,
        msgCount: "",
        countShow: false
    },
    mounted: function () {
        var socket;
        var count = 0;
        var t;
        var maxCount = 1000;
        var that = this;

        var connection = function () {
            //var name = _ucode;
            //var domain = 'wss://message.mrmatch.net';
            var domain = 'wss://websocket.mrmatch.net';
            var url = domain + '/api/BizClient/GetConnection?clientName=' + 1;
            socket = new WebSocket(url);
            socket.onopen = onopen;
            socket.onmessage = onmessage;
            socket.onclose = onclose;
            socket.onerror = onerror;
        }


        var onopen = function () {
            console.log("Connection Open....");
            start();
        }
        var onmessage = function (evt) {
            console.log("Received Message: " + evt.data);
            var msg = JSON.parse(evt.data);

            if (msg.Code == 10001) {
                that.getMessageCount();
                alert("您有一条新消息");
            }
            else if (msg.Code == 10002) {
                console.log(msg.Message);
                that.getMessageCount();
            }
            else if (msg.Code == 10004) {
                //心跳检测  重置定时时间
                console.log("心跳检测");
                reset();
            }
        }
        var onclose = function (evt) {
            console.log("onclose", evt);
            if (evt.code != 4500) {
                reConnection();//重连
            }
        }
        var onerror = function () {
            console.log("error...");
            reConnection();
        }

        //心跳检测
        //var heartCheck = {
        //    timeout: 5000,//60s
        //    timeoutObj: null,
        //    reset: function () {
        //        clearInterval(this.timeoutObj);
        //        this.start();
        //    },
        //    start: function () {
        //        this.timeoutObj = setInterval(function () {
        //            if (socket.readyState == 1) {
        //                socket.send("HeartCheck");
        //            }
        //        }, this.timeout)
        //    }
        //};

        var heartCheck = setInterval(function () { start() }, 15000);

        function start() {
            if (socket.readyState == 1) {
                socket.send("HeartCheck");
            }
        }

        function reset() {
            clearInterval(heartCheck);
            heartCheck = setInterval(function () { start() }, 15000);
        }

        //重连
        var reConnection = function () {
            count = count + 1;
            console.log("reconnection...【" + count + "】");
            //1与服务器已经建立连接
            if (socket.readyState == 1) {
                clearTimeout(t);
            } else {
                //2已经关闭了与服务器的连接
                if (socket.readyState == 3) {
                    connection();
                }
                //0正尝试与服务器建立连接,2正在关闭与服务器的连接
                t = setTimeout(function () { reConnection(); }, 3000);
            }
        }
        connection();

        this.getMessageCount();

    },
    methods: {
        logout: function () {
            axiosGet(api.passport + "Logout")
                .then((res) => {
                    alert(res);
                    window.location.reload();
                })
        },
        getMessageCount: function () {
            axiosGet(api.message + "GetDiDNotRead")
                .then((res) => {
                    if (res.data.count > 0) {
                        this.countShow = true;
                        this.msgCount = res.data.count;
                    } else {
                        this.msgCount = 0;
                        this.countShow = false;
                    }
                })
        }
    }
})