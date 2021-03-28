var vm = new Vue({
    el: "#left-panel",
    data: {
        isCheck: false,
        agent: false
    },
    mounted:function() {
        this.check();
    },
    methods: {
        check: function () {
            axiosGet(api.account + "GetApproveStatus")
                .then((res) => {
                    this.isCheck = res.data.check;
                    this.agent = res.data.agent;
                })
        }
    }
})