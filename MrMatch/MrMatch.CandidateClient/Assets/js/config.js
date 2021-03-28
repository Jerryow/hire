"use strict";

var _typeof = typeof Symbol === "function" && typeof Symbol.iterator === "symbol" ? function (obj) { return typeof obj; } : function (obj) { return obj && typeof Symbol === "function" && obj.constructor === Symbol && obj !== Symbol.prototype ? "symbol" : typeof obj; };

window.api = {
    profile: "/api/profileapi/",
    passport: "/api/passportapi/"
};

window.axiosPost = function (url, paras) {
    return new Promise(function (resolve, reject) {
        axios({
            url: url,
            method: 'post',
            headers: {
                "Authorization": "Bear " + _ticket
            },
            data: paras
        }).then(function (res) {
            resolve(res.data);
        }).catch(function (error) {
            //console.log(error);
            reject(error);
        });
    });
};

window.axiosGet = function axiosGet(url, paras) {
    return new Promise(function (resolve, reject) {
        axios({
            url: url,
            method: 'get',
            headers: {
                "Authorization": "Bear " + _ticket
            },
            params: paras
        }).then(function (res) {
            resolve(res.data);
        }).catch(function (error) {
            reject(error);
            // console.log(error);
        });
    });
};

window.axiosGetAsync = function axiosGetAsync(url, paras) {
    return new Promise(function (resolve, reject) {
        axios({
            url: url,
            method: 'get',
            headers: {
                "Authorization": "Bear " + _ticket
            },
            params: paras
        }).then(function (res) {
            resolve(res.data);
        }).catch(function (error) {
            reject(error);
            // console.log(error);
        });
    });
};

window.pageFill = function pageFill(index, size, keywords) {
    return result = {
        "pageIndex": index,
        "pageSize": size,
        "keyWords": keywords
    };
};

window.pageFillWithPKID = function pageFillWithPKID(index, size, keywords, pkid) {
    return result = {
        "pageIndex": index,
        "pageSize": size,
        "keyWords": keywords,
        "PKID": pkid
    };
};

window.getUrlKey = function getUrlKey(name) {
    return decodeURIComponent((new RegExp('[?|&]' + name + '=' + '([^&;]+?)(&|#|;|$)').exec(location.href) || [, ""])[1].replace(/\+/g, '%20')) || null;
};
window.getFullUrl = function getFullUrl() {
    return document.location;
};

window.getDateTimeNow = function getDateTimeNow() {
    var date = new Date();
    var year = date.getFullYear();
    var month = date.getMonth() + 1;
    var day = date.getDate();
    return year + "-" + month + "-" + day;
};

window.local = {
    get: function get(name) {
        var o = localStorage.getItem(name) || "";
        if (o === "" || o.indexOf("{") < 0 && o.indexOf("[") < 0) {
            return o;
        } else {
            return JSON.parse(o);
        }
    },
    set: function set(name, val) {
        val = (typeof val === "undefined" ? "undefined" : _typeof(val)) === "object" ? JSON.stringify(val) : val;localStorage.setItem(name, val);
    },
    clear: function clear(name) {
        localStorage.removeItem(name);
    }
};

//window.bus = new Vue({
//    data() {
//        return {
//            //switchRedactText:'您正在编辑别的内容，退出编辑后，已填写的内容不会被保存'
//            switchRedactText: '您正在编辑别的内容，确定退出吗？',
//            isLocked: false,
//            userInfo: null,
//        };
//    },
//    methods: {
//        getBasicInfo: function (that, fun) {
//            var userinfo = local.get('userinfo')
//            if (userinfo !== '') {
//                that.nickName = userinfo.NickName;
//                that.avatarUrl = userinfo.AvatarUrl;
//                that.userId = userinfo.PKID;
//                if (that.form) that.form.UserID = userinfo.PKID;
//                if (that.editForm) that.editForm.UserID = userinfo.PKID;

//                fun()
//            } else {
//                axiosGet(api.profile + "GetUserInfo")
//                    .then(function (res) {
//                        local.set('userinfo', res.data)

//                        that.nickName = res.data.NickName;
//                        that.avatarUrl = res.data.AvatarUrl;
//                        that.userId = res.data.PKID;
//                        if (that.form) that.form.UserID = res.data.PKID;
//                        if (that.editForm) that.editForm.UserID = res.data.PKID;

//                        fun()
//                    });
//            }
//        },
//        lockMessage: function (that, type) {
//            if (type) {
//                that.$alert('简历已锁定，请解锁后编辑', '提示')
//            }

//            return type
//        }
//    }
//})

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

//axiosGet(api.profile + "GetUserInfo")
//    .then(function (res) {
//        console.log(res)
//        bus.$emit('on-getInfo', { userinfo: res.data });
//    });