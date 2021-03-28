var vm = new Vue({
    el: "#appCompanInfo",
    data: {
        checkStatus:"",
        toolTips: {
            short: "简要描述您的公司及雇主闪光点，快速捕获贤才，最多100个汉字.",
            long: "请详尽介绍您的公司，建立雇主品牌，有利于求职者接受您的邀请哦！最多4000个汉字."+
            "公司介绍包括但不仅限于公司概况（股东结构、公司历史、主要产品或服务、雇员数目、年销售额），业务范围（主要市场& 新兴市场）、公司总部位置及分支布局、公司文化（使命、愿景、价值观）、公司主要客户介绍、公司所在行业上下游合作方、公司所获荣誉，及企业所作的社会贡献等."
        },
        form: {
            Website: "",
            ShortName: "",
            Location: "",
            Address: "",
            CompnayType: "",
            RegisteredCapital: "",
            EmployeeNum: "",
            Summary: "",
            Introduce: "",
            ContactName: "",
            CellPhone: "",
            Email: "",
            PKID: "",
            FinancingStatus:""
        },
        validate: {
            Website: {
                isPass: false,
                message: '公司官网不能为空',
            },
            Location: {
                isPass: false,
                message: '所在城市不能为空',
            },
            ShortName: {
                isPass: false,
                message: '对外显示名称不能为空',
            },
            CompnayType: {
                isPass: false,
                message: '请选择企业类型',
            },
            FinancingStatus: {
                isPass: false,
                message: '请选择融资阶段',
            },
            EmployeeNum: {
                isPass: false,
                message: '企业规模不能为空'
            },
            Summary: {
                isPass: false,
                message: '公司简介不能为空'
            },
            ContactName: {
                isPass: false,
                message: '企业联系人姓名不能为空',
            },
            CellPhone: {
                isPass: false,
                rules: [
                    { message: '企业联系人手机号格式有误', rule: ChkUtil.isMobile }
                ],
                message: '企业联系人手机号不能为空',
            },
            Email: {
                isPass: false,
                rules: [
                    { message: '企业联系人手机号格式有误', rule: ChkUtil.isEmail }
                ],
                message: '企业联系人邮箱不能为空'
            }
        },
        confirmModalText: '',
        isAdmin: false,
        contracts: [],
        contractHelp: "",

        headerText: false,
        loading:{
            submitLoading: false
        }
    },
    mounted: function () {
        this.getBasicInfo();
    },
    methods: {
        // 加载info
        getBasicInfo: function () {
            axiosGet(api.account + "GetCompanyInfo")
                .then((res) => {
                    this.isAdmin = res.data.isAdmin;
                    this.contracts = res.data.contractInfoList;
                    this.contractHelp = res.data.contractHelp;
                    this.checkStatus = res.data.companyInfo.CheckedStatus;
                    this.form.CompanyName = res.data.companyInfo.CompanyName;
                    this.form.Website = res.data.companyInfo.Website;
                    this.form.ShortName = res.data.companyInfo.ShortName;
                    this.form.Location = res.data.companyInfo.Location;
                    this.form.Address = res.data.companyInfo.Address;
                    this.form.CompnayType = res.data.companyInfo.CompnayType;
                    this.form.RegisteredCapital = res.data.companyInfo.RegisteredCapital;
                    this.form.EmployeeNum = res.data.companyInfo.EmployeeNum;
                    this.form.Summary = res.data.companyInfo.Summary;
                    this.form.Introduce = res.data.companyInfo.Introduce;
                    this.form.ContactName = res.data.companyInfo.ContactName;
                    this.form.CellPhone = res.data.companyInfo.CellPhone;
                    this.form.Email = res.data.companyInfo.Email;
                    this.form.FinancingStatus = res.data.companyInfo.FinancingStatus;
                    this.form.PKID = res.data.companyInfo.PKID;

                })
        },
        // 提交表单
        submitForm: function (form, sta) {
            if (!this.isAdmin) {
                this.$confirm('您不是管理员', '提示', {
                    confirmButtonText: '确定',
                    showCancelButton: false
                })
                return
            }

            if (!this.validateForm()) {
                this.$confirm(this.confirmModalText, '提示', {
                    confirmButtonText: '确定',
                    showCancelButton: false
                })
                return
            }
            this.loading.submitLoading = true;
            axiosPost(api.account + "UpdateCompany", this.form)
                .then((res) => {
                    this.loading.submitLoading = false;
                    this.$confirm(res.msg, '提示', {
                        confirmButtonText: '确定',
                        showCancelButton: false
                    })
                })
                .catch((ex) => {
                    this.loading.submitLoading = false;
                    this.$confirm('提交失败，请重试', '提示', {
                        confirmButtonText: '确定',
                        showCancelButton: false
                    })
                });
        },
        // 表单验证
        validateForm: function () {
            var validate = this.validate;
            var form = this.form;
            for (var x in validate) {
                if (!form[x]) {
                    this.validate[x].isPass = true;
                    this.confirmModalText = validate[x].message;

                    return false
                } else {
                    if (validate[x].rules) {
                        for (var i = 0; i < validate[x].rules.length; i++) {
                            if (!validate[x].rules[i].rule(form[x])) {
                                this.validate[x].isPass = true
                                this.confirmModalText = validate[x].rules[i].message
                                return false
                            }
                        }
                    }
                    this.validate[x].isPass = false
                }
            }

            return true
        },
        // 计算签约是否过期
        computePutTime: function (time) {
            var last = Date.parse(time);
            var now = Date.now();
            var bo = now - last <= 60 * 60 * 24 * 7 * 1000;
            if (bo) {
                return "有效";
            } else {
                return "过期";
            }
        },

    },
    filters: {
        dateFormat: function (val) {
            return val.split('T')[0];
        }
    }
})