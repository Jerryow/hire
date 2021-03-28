var appHeader = new Vue({
    el: "#header",
    data() {
        return {
            nickName: "",
            avatarUrl: ""
        };
    },
    mounted: function () {
        bus.getBasicInfo(this, function () { })
    },
    methods: {
        logout: function () {
            this.$confirm('确定退出登录？', '提示').then(active => {
                axiosGet(api.passport + "Logout")
                    .then((res) => {
                        window.bus.userinfo = null
                        local.clear('userinfo')
                        window.location.reload();
                    })
            })
        }
    }
});