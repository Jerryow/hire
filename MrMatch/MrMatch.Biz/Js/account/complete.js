var vm = new Vue({
    el: "#appComplete",
    data: {
        form: {
            AccountName: "",
            Position: "",
            CompanyName: "",
            CompanyID: 0,
            AgentOrNot: false
        },
        company: null,
        otherCompanyName:'',
        invitationList: [],
        options: [{
            value: true,
            label: '猎头企业'
        },
            {
                value: false,
            label: '非猎头企业'
        }]
    },
    watch: {
        otherCompanyName(newVal, oldVal) {
            if (this.form.CompanyID == 0) {
                this.form.CompanyName = newVal
            }
        }
    },
    mounted: function () {
        this.getInvatationInfo();
    },
    methods: {
        getInvatationInfo: function () {
            axiosGet(api.account + "GetInvitation")
                .then((res) => {
                    if (res.data != null) {
                        this.invitationList = res.data;
                        this.company = res.data[0]
                        this.form.CompanyID = res.data[0].PKID;
                        this.form.CompanyName = res.data[0].CompanyName;
                    } else {
                        console.log("3213");
                    }
                })
        },
        registCom: function () {

            if (!this.formValidate()) {

                this.$alert('请填完所有必填项', '提示');
                return false;
            }
            //alert(this.form.CompanyID + "-" + this.form.AgentOrNot)

            axiosPost(api.account + "RegistCompany", this.form)
                .then((res) => {
                    if (res.code == "1") {
                        this.$alert(res.msg,'提示');
                        window.location.href = "/account/CompanyInfo";
                    } else {
                        this.$alert(res.msg,'提示');
                    }
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

            if (this.form.CompanyID == 0) {
                if (this.form.CompanyName == "" || this.form.CompanyName == null) {
                    validate = false;
                } else {
                    var validate = true;
                }
            }

           
            return validate;
        },
        change: function (e) {
            this.form.CompanyID = e;
            if (e == 0) {
                this.form.CompanyName = '';
            } else {
                this.form.CompanyName = this.company.CompanyName;
                this.otherCompanyName = ''
            }
        }
    }
})