var vm = new Vue({
    el: "#appNoticeMessage",
    data: {
        form: {
            pageIndex: 1,
            pageSize: 10,
            total: 0
        },
        messageList: []
    },
    mounted: function () {
        this.getMessageList();
    },
    methods: {
        logout: function () {
            axiosGet(api.passport + "Logout")
                .then((res) => {
                    alert(res);
                    window.location.reload();
                })
        },
        getMessageList: function () {
            axiosGet(api.message + "GetNoticeInfo", this.form)
                .then((res) => {
                    this.messageList = res.data.data.DataList
                    this.form.total = res.data.data.Total
                    this.form.pageIndex = res.data.data.PageIndex


                })
        },
        // 点击分页
        currentChange: function (e) {
            this.form.pageIndex = e
        },
        readMark: function (val, read) {

            var folding = this.$refs['folding' + val]

            if ($(folding).hasClass('unfold')) {
                $(folding).removeClass('unfold').children('.tabpanel').height(0)
            } else {

                $(folding).addClass('unfold')
                $(folding).children('.tabpanel').height($(folding).children('.tabpanel').children('div').outerHeight())
            }


            if (!read) {
                axiosGet(api.message + "ReadMessage?PKID=" + val)
                    .then((res) => {
                        this.getMessageList();
                    });

                var name = _ucode;
                var domain = 'wss://message.mrmatch.net';
                //var domain = 'ws://192.168.1.128:8094';
                var url = domain + '/api/MessageApi/GetConnection?clientName=Send&fromUser=' + name;
                var ws = new WebSocket(url);


                ws.onopen = function (evt) {
                    console.log("Connection open ...");
                    ws.send("回写");
                };
                var that = this;
                ws.onmessage = function (evt) {
                    console.log("Received Message: " + evt.data);

                    var msg = JSON.parse(evt.data);

                    if (msg.Code == 10003) {

                    }
                    ws.close();
                };
            }
        }
    },
    filters: {
        dateFormat: function (val) {
            return val.split('.')[0].replace('T', ' ');
        }
    }
})