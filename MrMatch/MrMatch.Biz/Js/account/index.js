var vm = new Vue({
    el: "#accountIndex",
    data: {
        form: {
            AccountName: "",
            Position: "",
            CompanyName: "",
            CompanyID:0
        },
        contracts: [],
        contractHelp: "",
        check: false,
        companyName: "",
        interviewCount: {
            waitInvite: 0,
            all: 0,
            waitCommunicate: 0,
            waitInterview: 0,
            isInterview: 0,
            getOffer: 0,
            refuse: 0,
            isRefuse: 0
        },
        interviewitems: [
            {
                val:5,
                label: '待接受邀约',
                name: 'waitInvite',
                class:'text-light-blue'
            },
            {
                val: 0,
                label: '已接受邀约',
                name: 'all',
                class: ''
            },
            {
                val: 200,
                label: '待电话沟通',
                name: 'waitCommunicate',
                class: 'text-200'
            },
            {
                val: 210,
                label: '待面试',
                name: 'waitInterview',
                class: 'text-210'
            },
            {
                val: 220,
                label: '已面试',
                name: 'isInterview',
                class: 'text-220'
            },
            {
                val: 230,
                label: '已录用',
                name: 'getOffer',
                class: 'text-230'
            },
            {
                val: 110,
                label: '我拒绝的候选人',
                name: 'refuse',
                class: 'text-110'
            },
            {
                val: 100,
                label: '候选人谢绝邀约',
                name: 'isRefuse',
                class: 'text-100'
            }
        ]
    },
    mounted: function () {
        this.getBasicInfo();
        this.getInterviewCount();
    },
    methods: {
        //获取不同状态的面试数量
        getInterviewCount: function () {
            axiosGet(api.interview + "GetInterviewCount")
                .then((res) => {
                    this.interviewCount.waitInvite = res.data.waitInvite;
                    this.interviewCount.all = res.data.all;
                    this.interviewCount.waitCommunicate = res.data.waitCommunicate;
                    this.interviewCount.waitInterview = res.data.waitInterview;
                    this.interviewCount.isInterview = res.data.isInterview;
                    this.interviewCount.getOffer = res.data.getOffer;
                    this.interviewCount.refuse = res.data.refuse;
                    this.interviewCount.isRefuse = res.data.isRefuse;
                })

        },
        getBasicInfo: function () {
            axiosGet(api.account + "GetMaintainInfo")
                .then((res) => {
                    console.log(res)
                    if (res.code == "2") {
                        this.$alert(res.msg,'提示');
                        window.location.href = res.data.url;
                    }
                    this.companyName = res.data.companyInfo.CompanyName;
                    this.check = res.data.companyInfo.CheckedStatus;
                    this.contractHelp = res.data.contractHelp;
                    this.contracts = res.data.contractInfoList;
                })
        },
        getInvatationInfo: function () {
            axiosGet(api.account + "GetInvitation")
                .then((res) => {
                    if (res.data != null) {
                        this.invitationList = res.data;
                    } else {
                        console.log("3213");
                    }
                })
        },
        submitForm: function () {

        },
        registCom: function () {
            if (!this.formValidate()) {
                this.$alert("请填完所有必填项",'提示');
                return false;
            }

            axiosPost(api.account + "RegistCompany", this.form)
                .then((res) => {
                    console.log(res);
                })
        },
        formValidate: function () {
            var validate = true;
            if (this.form.AccountName == "" || this.form.AccountName == null) {
                validate = false;
            } else {
                var validate = true;
            }

            if (this.form.Position == "" || this.form.Position == null) {
                validate = false;
            } else {
                var validate = true;
            }

            if (this.form.CompanyName == "" || this.form.CompanyName == null) {
                validate = false;
            } else {
                var validate = true;
            }
            return validate;
        },
        //状态搜索条件
        setStatus: function (val) {
            if (val == 5) {
                window.location.href = "/job/invite?status=" + val;
                return
            }
            window.location.href = "/job/interview?status=" + val;
        },
    },
    filters: {
        dateFormat: function (val) {
            return val.split('T')[0];
        }
    }
})