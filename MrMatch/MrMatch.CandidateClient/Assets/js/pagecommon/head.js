"use strict";

var appHeader = new Vue({
    el: "#header",
    data: function data() {
        return {
            nickName: "",
            avatarUrl: ""
        };
    },

    mounted: function mounted() {
        bus.getBasicInfo(this, function () {});
    },
    methods: {
        logout: function logout() {
            this.$confirm('确定退出登录？', '提示').then(function (active) {
                axiosGet(api.passport + "Logout").then(function (res) {
                    window.bus.userinfo = null;
                    local.clear('userinfo');
                    window.location.reload();
                });
            });
        }
    }
});