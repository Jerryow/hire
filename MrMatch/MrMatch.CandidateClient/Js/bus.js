window.bus = new Vue({
    data() {
        return {
            //switchRedactText:'您正在编辑别的内容，退出编辑后，已填写的内容不会被保存'
            switchRedactText: '您正在编辑别的内容，确定退出吗？',
            //isLocked: false,
            userInfo: null,
        };
    },
    methods: {
        // 获取基础信息
        getBasicInfo (that, fun) {
            var userinfo = local.get('userinfo')
            console.log(userinfo)
            if (userinfo !== '') {
                that.nickName = userinfo.NickName;
                that.avatarUrl = userinfo.AvatarUrl;
                that.userId = userinfo.PKID;

                if (that.form) that.form.UserID = userinfo.PKID;
                if (that.editForm) that.editForm.UserID = userinfo.PKID;

                if(fun)fun()
            } else {
                axiosGet(api.profile + "GetUserInfo")
                    .then(function (res) {
                        console.log(res)
                        window.local.set('userinfo', res.data)

                        that.nickName = res.data.NickName;
                        that.avatarUrl = res.data.AvatarUrl;
                        that.userId = res.data.PKID;

                        if (that.form) that.form.UserID = res.data.PKID;
                        if (that.editForm) that.editForm.UserID = res.data.PKID;

                        if (fun) fun()
                    });
            }
        },
        // 获取所有简历信息
        getAllProfile(that, fun) {
            axiosGet(api.profile + "GetAllProfile",{
                userID: this.userId
            }).then((res) => {
                that.profile = res.data

                fun()
            });
        },
        lockMessage: function (that, type) {
            if (type) {
                that.$alert('简历审核中，暂时无法编辑', '提示')
            }

            return type
        }
    }
})

//;(function ($, window) {
//    $.ajax({
//        type: "GET",
//        url: api.profile + "GetUserInfo",
//        headers: {
//            "Authorization": "Bear " + _ticket
//        },
//        async: false,
//        success: function (res) {
//            local.set('userinfo', res.data)
//        },
//        error: function (err) {
//        }
//    });
//})($,window)