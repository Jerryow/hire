"use strict";

var appMyresume = new Vue({
    el: "#appSnap",
    data: function data() {
        return {
            profile: null,
            intention: null,
            workEx: null,
            education: null,

            loading: false
        };
    },

    mounted: function mounted() {
        this.getProfileSnap();
    },
    methods: {
        // 获取所有简历信息
        getProfileSnap: function getProfileSnap() {
            var _this = this;

            this.loading = true;
            axiosGet(api.profile + "GetProfileSnap", {
                userID: this.userId
            }).then(function (res) {
                _this.loading = false;
                if (res.code == "1") {
                    _this.profile = res.data.profile;
                    _this.intention = {
                        FunctionIDs: res.data.profile.FunctionIDs,
                        LocationIDs: res.data.profile.LocationIDs,
                        AnnualSalary: res.data.profile.IntentionAnnualSalary
                    };
                    _this.workEx = res.data.workEx;
                    _this.educationList = res.data.education;
                } else {
                    _this.$alert(res.msg, '提示');
                }
            });
        }
    },
    filters: {
        genderFilter: function genderFilter(val) {
            if (val == 1) {
                return '男';
            } else {
                return '女';
            }
        },
        jobFilter: function jobFilter(val) {
            if (val == 1) {
                return '在职';
            } else {
                return '离职';
            }
        },
        formatDate: function formatDate(val) {
            return ChkUtil.dateToFormat(val.split('T')[0]);
        },
        educationTypeFilter: function educationTypeFilter(val) {

            var educationSelect = ["初中", "高中", "中专", "大专", "本科", "研究生", "MBA/EMBA", "博士"];
            var educationTypeValue = [10, 20, 30, 40, 50, 60, 70, 80];

            return educationSelect[educationTypeValue.indexOf(val)];
        }
    }
});