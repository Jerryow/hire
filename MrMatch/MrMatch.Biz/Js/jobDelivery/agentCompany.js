
var vm = new Vue({
    el: "#appAgentCompany",
    data: {
        rules: {
            CompanyName: [
                { required: true, message: '请输入公司全称', trigger: 'change' }
            ],
            ShortName: [
                { required: true, message: '请输入对外显示名称', trigger: 'change' }
            ],
            City: [
                { required: false, message: '请输入所在城市', trigger: 'change' }
            ],
            CompnayType: [
                { required: false, message: '请选择企业类型', trigger: 'change' }
            ],
            EmployeeNum: [
                { required: false, message: '请选择企业规模', trigger: 'change' }
            ],
            Summary: [
                { required: false, message: '请输入公司简介', trigger: 'change' }
            ],
            Introduce: [
                { required: false, message: '请补充说明', trigger: 'change' }
            ]
        },
        form: {
            PKID: 0,
            CompanyName: '',
            ShortName: '',
            City: '',
            CompnayType: '',
            EmployeeNum: '',
            Summary:'',
            Introduce: ''
        }
    },
    watch: {
    },
    mounted: function () {
        if (ChkUtil.getQueryString('id')) {
            this.form.PKID = ChkUtil.getQueryString('id')

            this.GetJobCompany()
        }
    },
    methods: {
        GetJobCompany() {
            axiosGet(api.delivery + "GetJobCompanyByID?PKID=" + this.form.PKID)
                .then((res) => {
                    if (res.code == 1) {
                        this.form = res.data
                        if (res.data.CompnayType == 0) this.form.CompnayType = ''
                        if (res.data.EmployeeNum == 0) this.form.EmployeeNum = ''
                    }
                })
        },
        submitForm(formName) {
            console.log(this.form)
            this.$refs[formName].validate((valid) => {
                if (valid) {
                    // true
                    axiosPost(api.delivery + "SaveJobCompany", this.form)
                        .then((res) => {
                            this.$message(res.msg)
                            // if (res.code == 1) {}
                        })
                } else {
                    // false
                }
            });
        }
    },
    filters: {
        
    }
})

