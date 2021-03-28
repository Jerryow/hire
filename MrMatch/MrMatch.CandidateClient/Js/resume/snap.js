var appMyresume = new Vue({
    el: "#appSnap",
    data() {
        return {
            profile: null,
            intention: null,
            workEx:null,
            education: null,

            loading: false
        };
    },
    mounted: function () {
        this.getProfileSnap()
    },
    methods: {
        // 获取所有简历信息
        getProfileSnap() {
            this.loading = true
            axiosGet(api.profile + "GetProfileSnap", {
                userID: this.userId
            }).then((res) => {
                this.loading = false
                if (res.code == "1") {
                    this.profile = res.data.profile
                    this.intention = {
                        FunctionIDs: res.data.profile.FunctionIDs,
                        LocationIDs: res.data.profile.LocationIDs,
                        AnnualSalary: res.data.profile.IntentionAnnualSalary
                    }
                    this.workEx = res.data.workEx
                    this.educationList = res.data.education
                }else {
                    this.$alert(res.msg, '提示');
                }
            });
        },
    },
    filters: {
        genderFilter: function (val) {
            if (val == 1) {
                return '男';
            } else {
                return '女';
            }
        },
        jobFilter: function (val) {
            if (val == 1) {
                return '在职';
            } else {
                return '离职';
            }
        },
        formatDate: function (val) {
            return ChkUtil.dateToFormat(val.split('T')[0]);
        },
        educationTypeFilter(val) {

            let educationSelect = ["初中", "高中", "中专", "大专", "本科", "研究生", "MBA/EMBA", "博士"]
            let educationTypeValue = [10, 20, 30, 40, 50, 60, 70, 80]

            return educationSelect[educationTypeValue.indexOf(val)]
        }
    }
});