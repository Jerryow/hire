'use strict';

var appAvoid = new Vue({
    el: "#appAvoid",
    data: function data() {
        return {
            isRedact: false,
            isRedactType: 'appAvoid',
            activeRedactType: '',
            nickName: "",
            avatarUrl: "",
            profileShow: false,
            isLocked: false,
            labelPosition: "top",
            avoidList: [],
            form: {
                UserID: 0,
                AvoidDomain: ""
            },
            rules: {
                AvoidDomain: [{ required: true, message: '企业邮箱后缀不能为空.', trigger: 'blur' }]
            },
            loading: {
                submitLoading: false
            }
        };
    },

    mounted: function mounted() {
        bus.getBasicInfo(this, this.getAvoidsInfo);
        this.pageBus();
    },
    methods: {
        getAvoidsInfo: function getAvoidsInfo() {
            var _this2 = this;

            axiosGet(api.profile + "GetAvoidList", {
                userID: this.form.UserID
            }).then(function (res) {
                if (res.code == "1") {
                    _this2.avoidList = res.data;
                } else {
                    _this2.$alert(res.msg, '提示');
                }
            });
        },
        submitForm: function submitForm(form) {
            var _this3 = this;

            this.$refs[form].validate(function (valid) {
                if (valid) {
                    _this3.loading.submitLoading = true;
                    axiosPost(api.profile + "AddAvoid", _this3.form).then(function (res) {
                        _this3.loading.submitLoading = false;
                        _this3.$alert(res.msg, '提示');
                        if (res.code == "1") {
                            _this3.form.AvoidDomain = "";

                            _this3.unRedact();
                            _this3.getAvoidsInfo();
                            appMyresume.getProfile();
                            appMyresume.snapUpdateStatus();
                        }
                    }).catch(function (ex) {
                        _this3.loading.submitLoading = false;
                        _this3.$alert(ex.response.data.Message, '提示');
                    });
                } else {
                    return;
                }
            });
        },
        redirectProfile: function redirectProfile() {
            var id = getUrlKey("id");
            window.location.href = "/profile/profile?id=" + id;
        },
        removeAvoid: function removeAvoid(val) {
            var _this4 = this;

            if (bus.lockMessage(this, bus._data.isLocked)) return;
            this.$confirm('确认要删除此条域名信息嘛？', '提示').then(function (active) {
                axiosGet(api.profile + "DestroyAvoid", {
                    PKID: val
                }).then(function (res) {
                    if (res.code == "1") {
                        _this4.$message.success(res.msg, '提示');
                        _this4.getAvoidsInfo();
                        appMyresume.getProfile();
                        appMyresume.snapUpdateStatus();
                    } else {
                        _this4.$message.error(res.msg, '提示');
                    }
                });
            });
        },
        redact: function redact() {
            var _this5 = this;

            if (bus.lockMessage(this, bus._data.isLocked)) return;

            if (this.activeRedactType == '' || this.activeRedactType == this.isRedactType) {
                bus.$emit('on-redact', { isRedact: true, isRedactType: this.isRedactType });
            } else {
                this.$confirm(bus._data.switchRedactText, '提示', {
                    confirmButtonText: '退出',
                    cancelButtonText: '取消'
                }).then(function (active) {
                    bus.$emit('on-redact', { isRedact: true, isRedactType: _this5.isRedactType });
                }).catch(function () {});
            }
        },
        unRedact: function unRedact() {
            this.isRedact = false;
            bus.$emit('on-redact', { isRedact: false, isRedactType: '' });
        },
        pageBus: function pageBus() {
            var _this6 = this;

            bus.$on('on-redact', function (option) {
                _this6.activeRedactType = option.isRedactType;

                if (option.isRedact && (_this6.activeRedactType == _this6.isRedactType || _this6.activeRedactType == '')) {
                    _this6.isRedact = true;
                } else {
                    _this6.isRedact = false;
                }
            });
        }
    }
});
var appEducation = new Vue({
    el: "#appEducation",
    data: function data() {
        var checkNull = function checkNull(rule, value, callback) {
            if (value.trim() == "") {
                callback(new Error('请输入正确格式的值'));
            } else {
                callback();
            }
        };
        return {
            isRedact: false,
            isRedactType: 'appEducation',
            isRedactItemType: 'appEducationItem',
            activeRedactType: '',
            nickName: "",
            avatarUrl: "",
            profileShow: false,
            isLocked: false,
            labelPosition: "top",
            newExpirationDate: false,
            editExpirationDate: false,
            educationList: [],
            form: {
                UserID: 0,
                SchoolName: "",
                MajorSubject: "",
                StartDate: "",
                ExpirationDate: "",
                Degree: "",
                EducationType: ""
            },
            editForm: {
                UserID: 0,
                SchoolName: "",
                MajorSubject: "",
                StartDate: "",
                ExpirationDate: "",
                Degree: "",
                EducationType: ""
            },
            rules: {
                SchoolName: [{ required: true, message: '学校名称不能为空.', trigger: 'blur' }, { validator: checkNull, trigger: 'blur' }],
                MajorSubject: [{ required: true, message: '专业不能为空.', trigger: 'blur' }, { validator: checkNull, trigger: 'blur' }],
                StartDate: [{ required: true, message: '开始时间不能为空.', trigger: 'blur' }],
                ExpirationDate: [{ required: true, message: '结束时间不能为空.', trigger: 'blur' }],
                EducationType: [{ required: true, message: '学历不能为空.', trigger: 'blur' }]
            },
            educationType: [{ label: "初中", value: 10 }, { label: "高中", value: 20 }, { label: "中专", value: 30 }, { label: "大专", value: 40 }, { label: "本科", value: 50 }, { label: "研究生", value: 60 }, { label: "MBA/EMBA", value: 70 }, { label: "博士", value: 80 }],
            dialogEditEducation: {
                index: 0,
                title: "修改简历",
                dialogVisible: false,
                diaClose: false,
                labelPosition: "top"
            },
            loading: {
                submitLoading1: false,
                saveLoading2: false
            }
        };
    },

    watch: {
        newExpirationDate: function newExpirationDate(news, old) {
            this.form.ExpirationDate = news ? '至今' : '';
        },
        editExpirationDate: function editExpirationDate(news, old) {
            this.editForm.ExpirationDate = news ? '至今' : '';
        }
    },
    mounted: function mounted() {
        bus.getBasicInfo(this, this.getEducationsInfo);
        this.pageBus();
    },
    methods: {
        degreeChange: function degreeChange(e, type) {
            for (var i = 0; i < this.educationType.length; i++) {
                if (this.educationType[i].value == e) {
                    if (type === 'editForm') {
                        this.editForm.Degree = this.educationType[i].value;
                    } else {
                        this.form.Degree = this.educationType[i].value;
                    }
                }
            }
        },
        getEducationsInfo: function getEducationsInfo() {
            var _this7 = this;

            axiosGet(api.profile + "GetEducationList", {
                userID: this.form.UserID
            }).then(function (res) {
                if (res.code == "1" && res.data) {
                    for (var i = 0; i < res.data.length; i++) {
                        res.data[i].isRedact = false;

                        for (var x = 0; x < _this7.educationType.length; x++) {
                            if (_this7.educationType[x].value == res.data[i].Degree) {
                                res.data[i].EducationType = _this7.educationType[x].label;
                            }
                        }
                    }
                    _this7.educationList = res.data;
                } else {
                    _this7.educationList = [];
                }
            });
        },
        submitForm: function submitForm(form) {
            var _this8 = this;

            if (this.newExpirationDate) this.form.ExpirationDate = '至今';
            this.$refs[form].validate(function (valid) {
                if (valid) {
                    _this8.loading.submitLoading = true;
                    if (_this8.newExpirationDate) {
                        _this8.form.ExpirationDate = '至今';
                    } else if (_this8.form.ExpirationDate) {
                        _this8.form.ExpirationDate = _this8.form.ExpirationDate.split('-').map(function (e) {
                            return e.toString().replace(/\b(0+)/gi, "");
                        }).join('-');
                    }
                    axiosPost(api.profile + "AddEducation", _this8.form).then(function (res) {
                        _this8.loading.submitLoading = false;
                        if (res.code == "1") {
                            _this8.$alert(res.msg, '提示');
                            _this8.form.SchoolName = "";
                            _this8.form.MajorSubject = "";
                            _this8.form.StartDate = "";
                            _this8.form.ExpirationDate = "";
                            _this8.form.Degree = "";
                            _this8.form.EducationType = "";
                            _this8.newExpirationDate = false;

                            _this8.unRedact();
                            _this8.getEducationsInfo();
                            appMyresume.getProfile();
                            appMyresume.snapUpdateStatus();
                        } else {
                            _this8.$alert(res.msg, '提示');
                        }
                    }).catch(function (ex) {
                        _this8.loading.submitLoading = false;
                        _this8.$alert(ex.response.data.Message, '提示');
                    });
                } else {
                    return false;
                }
            });
        },
        redirectProfile: function redirectProfile() {
            var id = getUrlKey("id");
            window.location.href = "/profile/profile?id=" + id;
        },
        handleClose: function handleClose() {
            this.dialogEditEducation.dialogVisible = false;
            this.editExpirationDate = false;
        },
        // 修改教育经历
        updateForm: function updateForm(form, i) {
            var _this9 = this;

            console.log(this.$refs[form]);
            this.$refs[form][i].validate(function (valid) {
                if (valid) {
                    _this9.loading.saveLoading = true;
                    if (_this9.editExpirationDate) {
                        _this9.editForm.ExpirationDate = '至今';
                    } else {
                        _this9.editForm.ExpirationDate = _this9.editForm.ExpirationDate.split('-').map(function (e) {
                            return e.toString().replace(/\b(0+)/gi, "");
                        }).join('-');
                    }
                    axiosPost(api.profile + "UpdateEducation", _this9.editForm).then(function (res) {
                        _this9.loading.saveLoading = false;
                        _this9.$alert(res.msg, '提示');
                        if (res.code == "1") {
                            _this9.editExpirationDate = false;

                            _this9.unRedactItem(i);
                            _this9.getEducationsInfo();
                        }
                    }).catch(function (ex) {
                        _this9.loading.saveLoading = false;
                        _this9.$alert(ex.response.data.Message, '提示');
                    });
                } else {
                    return false;
                }
            });
        },
        removeEducation: function removeEducation(val) {
            var _this10 = this;

            if (bus.lockMessage(this, bus._data.isLocked)) return;
            this.$confirm('确定要删除此条教育经历？', '提示').then(function (active) {
                axiosGet(api.profile + "DestroyEducation", {
                    PKID: val
                }).then(function (res) {
                    if (res.code == "1") {
                        _this10.$message.success(res.msg);
                        _this10.getEducationsInfo();
                        appMyresume.getProfile();
                    } else {
                        _this10.$message.error(res.msg);
                    }
                });
            });
        },
        redactItem: function redactItem(item, index) {
            var _this11 = this;

            if (bus.lockMessage(this, bus._data.isLocked)) return;

            this.editForm.PKID = item.PKID;
            this.editForm.SchoolName = item.SchoolName;
            this.editForm.MajorSubject = item.MajorSubject;
            this.editForm.StartDate = item.StartDate;
            this.editForm.Degree = item.Degree;
            this.editForm.EducationType = item.EducationType;

            if (item.ExpirationDate == '至今') {
                this.editExpirationDate = true;
            } else {
                this.editForm.ExpirationDate = item.ExpirationDate;
            }

            this.isRedactItemType = this.isRedactItemType + index;

            if (this.activeRedactType == '' || this.activeRedactType == this.isRedactItemType) {
                bus.$emit('on-redact', { isRedact: true, isRedactType: this.isRedactItemType, itemIndex: index });
            } else {
                this.$confirm(bus._data.switchRedactText, '提示', {
                    confirmButtonText: '退出',
                    cancelButtonText: '取消'
                }).then(function (active) {
                    bus.$emit('on-redact', { isRedact: true, isRedactType: _this11.isRedactItemType, itemIndex: index });
                }).catch(function () {});
            }
        },
        unRedactItem: function unRedactItem(item, index) {
            bus.$emit('on-redact', { isRedact: false, isRedactType: '', itemIndex: index });
        },
        redact: function redact() {
            var _this12 = this;

            if (bus.lockMessage(this, bus._data.isLocked)) return;

            if (this.activeRedactType == '' || this.activeRedactType == this.isRedactType) {
                bus.$emit('on-redact', { isRedact: true, isRedactType: this.isRedactType });
            } else {
                this.$confirm(bus._data.switchRedactText, '提示', {
                    confirmButtonText: '退出',
                    cancelButtonText: '取消'
                }).then(function (active) {
                    bus.$emit('on-redact', { isRedact: true, isRedactType: _this12.isRedactType });
                }).catch(function () {});
            }
        },
        unRedact: function unRedact() {
            bus.$emit('on-redact', { isRedact: false, isRedactType: '' });
        },
        pageBus: function pageBus() {
            var _this13 = this;

            bus.$on('on-redact', function (option) {
                _this13.activeRedactType = option.isRedactType;

                for (var i = 0; i < _this13.educationList.length; i++) {
                    _this13.educationList[i].isRedact = false;
                }

                if (option.itemIndex || option.itemIndex === 0) {
                    if (option.isRedact && (_this13.activeRedactType == _this13.isRedactItemType || _this13.activeRedactType == '')) {
                        _this13.educationList[option.itemIndex].isRedact = true;
                    }
                } else {
                    if (option.isRedact && (_this13.activeRedactType == _this13.isRedactType || _this13.activeRedactType == '')) {
                        _this13.isRedact = true;
                    } else {
                        _this13.isRedact = false;
                    }
                }
            });
        }
    },
    filters: {
        formatDate: function formatDate(val) {
            return ChkUtil.dateToFormat(val.split('T')[0]);
        }
    }
});
var appIntention = new Vue({
    el: "#appIntention",
    data: function data() {
        return {
            isAdded: false,
            isRedact: false,
            isRedactType: 'appIntention',
            activeRedactType: '',
            nickName: "",
            avatarUrl: "",
            profileShow: false,
            isLocked: false,
            labelPosition: "top",
            FunctionsList: [],
            FunctionalIntent: [],
            districtList: [],
            allDistrictList: [],
            districtNameList: [],
            checkedCities: [],

            selectType: 'function',

            functionId: [],
            functionIDs: [],
            functionNames: [null, null, null],
            functionIndex: null,

            intentionId: [],
            intentionIDs: [],
            intentionNames: [null, null, null],
            intentionIndex: null,

            Locations: [],
            LocationIDs: [],
            LocationNames: [],
            intentionInfo: {},
            form: {
                UserID: 0,
                AnnualSalary: "",
                FunctionIDs: '',
                Functions: '',
                IntentionFunctionIDs: '',
                IntentionFunctions: '',
                LocationIDs: [],
                Locations: '',
                PKID: null
            },
            drawer: false,
            selectCity: false,
            selectCityIndex: 0,
            secondList: [],
            thirdList: [],
            loading: {
                submitLoading: false
            }
        };
    },

    filters: {
        functionNameFilters: function functionNameFilters(val) {
            console.log(val);
            var str = '';
            for (var i = 0; i < this.functionList.length; i++) {
                if (val[0] == this.functionList[i].value) {
                    str += this.functionList[i].label;
                }
            }

            return 0;
        }
    },
    mounted: function mounted() {
        //this.getBasicInfo()

        bus.getBasicInfo(this, this.getBasicInfo);
        this.pageBus();
    },
    methods: {
        getBasicInfo: function getBasicInfo() {
            this.getFunctionList();
            this.getDistrictList();
        },
        // 获取职能数据
        getFunctionList: function getFunctionList() {
            var _this14 = this;

            axiosGet(api.profile + "GetFunctionForCascader").then(function (data) {
                if (data.code == 1) {
                    _this14.FunctionsList = data.data;
                    _this14.FunctionsList.unshift({
                        label: "暂不选择",
                        value: -1
                    });

                    _this14.getJobIntention();
                }
            });
        },
        // 获取城市数据
        getDistrictList: function getDistrictList() {
            var _this15 = this;

            axiosGet(api.profile + "GetAllDistrictList").then(function (data) {
                if (data.code == 1) {
                    _this15.districtList = data.data.hot;
                    _this15.allDistrictList = data.data.basic;

                    // 
                    _this15.initDistrictList();

                    //for (var i = 0; i < this.districtList.length; i++) {
                    //    this.districtNameList.push(this.districtList[i].DistrictName)
                    //}
                }
            });
        },
        // 更新城市数据状态
        initDistrictList: function initDistrictList() {
            var districtList = this.districtList;
            var allDistrictList = this.allDistrictList;
            var LocationNames = [];
            var _arr = this.LocationIDs;

            console.log(100);
            // 更新热门城市选中状态
            for (var i = 0; i < districtList.length; i++) {
                districtList[i].checked = false;

                for (var j = 0; j < _arr.length; j++) {
                    if (_arr[j] == districtList[i].PKID) {
                        districtList[i].checked = true;
                    }
                }
            }

            // 更新全部城市选中状态
            for (var _i = 0; _i < allDistrictList.length; _i++) {
                for (var k = 0; k < allDistrictList[_i].Children.length; k++) {
                    allDistrictList[_i].Children[k].checked = false;

                    for (var _j = 0; _j < _arr.length; _j++) {
                        if (_arr[_j] == allDistrictList[_i].Children[k].PKID) {
                            allDistrictList[_i].Children[k].checked = true;
                            LocationNames.push(allDistrictList[_i].Children[k].DistrictName);
                        }
                    }
                }
            }

            this.districtList = null;
            this.allDistrictList = null;

            this.districtList = districtList;
            this.allDistrictList = allDistrictList;
            this.LocationNames = LocationNames;
        },

        getJobIntention: function getJobIntention() {
            var _this16 = this;

            var _this = this;
            axiosGet(api.profile + "GetJobIntention", { UserID: this.form.UserID }).then(function (data) {
                if (data.code == 1) {
                    console.log(_this16.form.PKID);
                    if (data.data != null) {
                        _this16.form.AnnualSalary = data.data.AnnualSalary ? data.data.AnnualSalary : '';
                        _this16.form.FunctionIDs = data.data.FunctionIDs;
                        _this16.form.Functions = data.data.Functions;
                        _this16.form.LocationIDs = data.data.LocationIDs;
                        _this16.form.Locations = data.data.Locations;
                        _this16.form.PKID = data.data.PKID;

                        _this16.intentionInfo = data.data;

                        _this16.LocationIDs = data.data.LocationIDs.split(',').map(function (e) {
                            return parseInt(e);
                        });
                        _this16.checkedCities = data.data.Locations.split(',');
                        _this16.functionIDs = data.data.FunctionIDs.split(',').map(function (e, index) {
                            _this.updateData(e.split('-'), index, 'function');
                            return e.split('-');
                        });
                        //this.intentionIDs = data.data.IntentionFunctionIDs.split(',').map(function (e, index) {
                        //    _this.updateData(e.split('-'), index, 'intention')
                        //    return e.split('-')
                        //})

                        _this16.initDistrictList();

                        _this16.isAdded = false;
                    } else {
                        _this16.isAdded = true;
                    }
                }
            });
        },
        submitForm: function submitForm(form) {
            var _this17 = this;

            var _form = {
                UserID: this.form.UserID,
                FunctionIDs: this.form.FunctionIDs,
                LocationIDs: this.LocationIDs.join(','),
                AnnualSalary: this.form.AnnualSalary
            };

            if (_form.FunctionIDs.trim() == "") {
                this.$alert("请选择您过往的职能资历", '提示');
                return false;
            }
            if (_form.LocationIDs.trim() == "") {
                this.$alert("请选择您的意向城市", '提示');
                return false;
            }

            this.$refs[form].validate(function (valid) {
                if (valid) {
                    _this17.loading.submitLoading = true;
                    if (_this17.isAdded) {

                        axiosPost(api.profile + "AddJobIntention", _form).then(function (res) {
                            _this17.loading.submitLoading = false;
                            _this17.$alert(res.msg, '提示');
                            if (res.code == "1") {
                                _this17.unRedact();
                                _this17.intentionNames = [];
                                _this17.functionNames = [];
                                _this17.getJobIntention();
                                appMyresume.getProfile();
                                appMyresume.snapUpdateStatus();
                            }
                        }).catch(function (ex) {
                            _this17.loading.submitLoading = false;
                            _this17.$aler(ex.response.data.Message, '提示');
                        });
                    } else {
                        _form.PKID = _this17.form.PKID;
                        axiosPost(api.profile + "UpdateJobIntention", _form).then(function (res) {
                            _this17.loading.submitLoading = false;
                            _this17.$alert(res.msg, '提示');
                            if (res.code == "1") {
                                _this17.unRedact();
                                _this17.intentionNames = [];
                                _this17.functionNames = [];
                                _this17.getJobIntention();
                                appMyresume.getProfile();
                                appMyresume.snapUpdateStatus();
                            }
                        }).catch(function (ex) {
                            _this17.loading.submitLoading = false;
                            _this17.$aler(ex.response.data.Message, '提示');
                        });
                    }
                } else {
                    return false;
                }
            });
        },
        redirectProfile: function redirectProfile() {
            var id = getUrlKey("id");
            window.location.href = "/profile/profile?id=" + id;
        },
        // 变更城市 //作废
        checkedCitiesChange: function checkedCitiesChange(e) {
            console.log(e);
            var arr = [];
            for (var i = 0; i < e.length; i++) {
                for (var j = 0; j < this.districtList.length; j++) {
                    if (e[i] == this.districtList[j].DistrictName) {
                        arr.push(this.districtList[j].PKID);
                    }
                }
            }

            this.form.LocationIDs = arr.join(',');
        },

        // 选择城市
        cityClick: function cityClick(item, i) {
            var LocationIDs = this.LocationIDs ? this.LocationIDs : [];
            var LocationNames = this.LocationNames;

            for (var k = 0; k < LocationIDs.length; k++) {
                if (item.PKID == LocationIDs[k]) {
                    this.LocationIDs.splice(k, 1);
                    this.initDistrictList();
                    return;
                }
            }

            if (LocationIDs.length >= 3) {
                this.$message("最多选择三个意向城市");
                return;
            }

            this.LocationIDs.push(item.PKID);
            this.initDistrictList();
        },

        // 简历概括
        clickFunctionItem: function clickFunctionItem(i, type) {
            this.selectType = type;

            this.functionIndex = i;
            this.drawer = true;

            this.secondList = [];
            this.thirdList = [];
        },
        firstListClick: function firstListClick(i) {
            if (this.FunctionsList[i].value == -1) {
                this.updateData(-1, this.functionIndex);
            } else {
                this.functionId[0] = this.FunctionsList[i].value;
                this[this.selectType + 'Id'][0] = this.FunctionsList[i].value;
                this.secondList = this.FunctionsList[i].children;
                this.thirdList = [];
            }
        },
        secondListClick: function secondListClick(i) {
            this[this.selectType + 'Id'][1] = this.secondList[i].value;
            if (this.secondList[i].children) {
                this.thirdList = this.secondList[i].children;
            } else {
                if (this[this.selectType + 'Id'][2]) {
                    this[this.selectType + 'Id'].pop();
                }
                this.addFunctionItem();
            }
        },
        thirdListClick: function thirdListClick(i) {
            this[this.selectType + 'Id'][2] = this.thirdList[i].value;
            this.addFunctionItem();
        },
        addFunctionItem: function addFunctionItem(item) {
            console.log(0, this[this.selectType + 'Id']);

            this.drawer = false;
            this.updateData(this[this.selectType + 'Id'], this.functionIndex);

            console.log(1, this[this.selectType + 'IDs']);
        },
        // 更新数据
        updateData: function updateData(val, index, type) {
            if (type) this.selectType = type;
            var str = '';
            var arr = [];
            if (val == -1) {
                this[this.selectType + 'Names'][index] = null;
                this[this.selectType + 'IDs'][index] = [];
                this.drawer = false;
            } else {
                for (var i = 0; i < this.FunctionsList.length; i++) {
                    if (val[0] == this.FunctionsList[i].value) {
                        str += this.FunctionsList[i].label;
                        arr[0] = this.FunctionsList[i].value;

                        for (var j = 0; j < this.FunctionsList[i].children.length; j++) {
                            if (val[1] == this.FunctionsList[i].children[j].value) {

                                str += ' / ' + this.FunctionsList[i].children[j].label;
                                arr[1] = this.FunctionsList[i].children[j].value;

                                if (val.length == 3) {
                                    for (var k = 0; k < this.FunctionsList[i].children[j].children.length; k++) {

                                        if (val[2] == this.FunctionsList[i].children[j].children[k].value) {
                                            str += ' / ' + this.FunctionsList[i].children[j].children[k].label;
                                            arr[2] = this.FunctionsList[i].children[j].children[k].value;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                this[this.selectType + 'Names'][index] = str;
                this[this.selectType + 'IDs'][index] = arr;
            }

            if (this.selectType == 'function') {
                this.form['FunctionIDs'] = this[this.selectType + 'IDs'].map(function (e, i) {
                    //var a = e ? e.join('-'):''
                    return e.join('-');
                }).join(',').replace(/^,+|,+$/g, "").replace(",,", ",");
            }
            if (this.selectType == 'intention') {
                this.form['IntentionFunctionIDs'] = this[this.selectType + 'IDs'].map(function (e, i) {
                    //var a = e ? e.join('-'):''
                    return e.join('-');
                }).join(',').replace(/^,+|,+$/g, "").replace(",,", ",");
            }
        },

        redact: function redact() {
            var _this18 = this;

            if (bus.lockMessage(this, bus._data.isLocked)) return;

            if (this.activeRedactType == '' || this.activeRedactType == this.isRedactType) {
                bus.$emit('on-redact', { isRedact: true, isRedactType: this.isRedactType });
            } else {
                this.$confirm(bus._data.switchRedactText, '提示', {
                    confirmButtonText: '退出',
                    cancelButtonText: '取消'
                }).then(function (active) {
                    bus.$emit('on-redact', { isRedact: true, isRedactType: _this18.isRedactType });
                }).catch(function () {});
            }
        },
        unRedact: function unRedact() {
            this.isRedact = false;
            bus.$emit('on-redact', { isRedact: false, isRedactType: '' });
        },
        pageBus: function pageBus() {
            var _this19 = this;

            bus.$on('on-redact', function (option) {
                _this19.activeRedactType = option.isRedactType;

                if (option.isRedact && (_this19.activeRedactType == _this19.isRedactType || _this19.activeRedactType == '')) {
                    _this19.isRedact = true;
                } else {
                    _this19.isRedact = false;
                }
            });
        }
    }
});
var appUserinfo = new Vue({
    el: "#appUserinfo",
    data: function data() {
        return {
            nickName: "",
            avatarUrl: "",
            formData: null,
            form: {
                PKID: 0,
                UserID: 0,
                RealName: "",
                Gender: 0,
                Birthday: "",
                Education: "",
                BirthPlace: "",
                Residence: "",
                CensusRegister: "",
                CurrentCompany: "",
                CurrentPosition: "",
                AnnualSalary: 0,
                JobStatus: 0,
                Introduction: "",
                IsLocked: false
            },
            rules: {
                RealName: [{ required: true, message: '请输入姓名.', trigger: 'blur' }],
                Gender: [{ required: true, message: '请选择性别.', trigger: 'blur' }],
                Birthday: [{ required: true, message: '请输入生日.', trigger: 'blur' }],
                Education: [{ required: true, message: '请选择学历.', trigger: 'blur' }],
                Residence: [{ required: true, message: '请输入当前居住地.', trigger: 'blur' }],
                CurrentCompany: [{ required: true, message: '请输入当前公司名称.', trigger: 'blur' }],
                CurrentPosition: [{ required: true, message: '请输入当前职位.', trigger: 'blur' }],
                JobStatus: [{ required: true, message: '请选择当前求职状态.', trigger: 'blur' }]
            },
            Gender: [{ label: "男", value: 1 }, { label: "女", value: 2 }],
            Education: [{ label: "初中", value: 10 }, { label: "高中", value: 20 }, { label: "中专", value: 30 }, { label: "大专", value: 40 }, { label: "本科", value: 50 }, { label: "研究生", value: 60 }, { label: "MBA/EMBA", value: 70 }, { label: "博士", value: 80 }],
            JobStatus: [{ label: "在职", value: 1 }, { label: "离职", value: 2 }],
            isRedact: false,
            isRedactType: 'appUserinfo',
            activeRedactType: '',
            loading: {
                submitLoading: false
            }

        };
    },

    mounted: function mounted() {
        bus.getBasicInfo(this, this.getProfileInfo);
        this.pageBus();
    },
    methods: {
        getProfileInfo: function getProfileInfo() {
            var _this20 = this;

            axiosGet(api.profile + "GetProfileInfo", {
                userID: this.form.UserID
            }).then(function (res) {
                if (res.code === "1" && res.data) {
                    var data = res.data;
                    _this20.form.PKID = data.PKID;
                    _this20.form.UserID = data.UserID;
                    _this20.form.RealName = data.RealName;
                    _this20.form.Gender = data.Gender;
                    _this20.form.Birthday = data.Birthday.split('T')[0];
                    _this20.form.Education = data.Education;
                    _this20.form.BirthPlace = data.BirthPlace;
                    _this20.form.Residence = data.Residence;
                    _this20.form.CensusRegister = data.CensusRegister;
                    _this20.form.CurrentCompany = data.CurrentCompany;
                    _this20.form.CurrentPosition = data.CurrentPosition;
                    _this20.form.AnnualSalary = data.AnnualSalary ? data.AnnualSalary : '';
                    _this20.form.JobStatus = data.JobStatus;
                    _this20.form.Introduction = data.Introduction;
                    _this20.form.IsLocked = data.IsLocked;

                    _this20.formData = data;
                    _this20.formData.Introduction = fixText(data.Introduction);
                } else {
                    _this20.form.UserID = getUrlKey("id");
                }
            });
        },
        denderChange: function denderChange(e) {
            for (var i = 0; i < this.Gender.length; i++) {
                if (this.Gender[i].value == e) {
                    this.form.Gender = this.Gender[i].value;
                }
            }
        },
        educationChange: function educationChange(e) {
            for (var i = 0; i < this.Education.length; i++) {
                if (this.Education[i].value == e) {
                    this.form.Education = this.Education[i].label;
                }
            }
        },
        submitProfile: function submitProfile(form) {
            var _this21 = this;

            this.$refs[form].validate(function (valid) {
                if (valid) {
                    _this21.loading.submitLoading = true;
                    axiosPost(api.profile + "UpdateProfile", _this21.form).then(function (res) {
                        _this21.loading.submitLoading = false;

                        _this21.$alert(res.msg, '提示');
                        if (res.code == "1") {
                            _this21.unRedact();
                            _this21.getProfileInfo();
                            appMyresume.snapUpdateStatus();
                        }
                    }).catch(function (ex) {
                        _this21.loading.submitLoading = false;
                        _this21.$alert(ex.response.data.Message, '提示');
                    });
                } else {
                    return false;
                }
            });
        },
        // 编辑
        redact: function redact() {
            var _this22 = this;

            if (bus.lockMessage(this, bus._data.isLocked)) return;

            if (this.activeRedactType == '' || this.activeRedactType == this.isRedactType) {
                bus.$emit('on-redact', { isRedact: true, isRedactType: this.isRedactType });
            } else {
                this.$confirm(bus._data.switchRedactText, '提示', {
                    confirmButtonText: '退出',
                    cancelButtonText: '取消'
                }).then(function (active) {
                    bus.$emit('on-redact', { isRedact: true, isRedactType: _this22.isRedactType });
                }).catch(function () {});
            }
        },
        // 取消编辑
        unRedact: function unRedact() {
            bus.$emit('on-redact', { isRedact: false, isRedactType: '' });
        },
        // bus通讯
        pageBus: function pageBus() {
            var _this23 = this;

            bus.$on('on-redact', function (option) {
                _this23.activeRedactType = option.isRedactType;

                if (option.isRedact && (_this23.activeRedactType == _this23.isRedactType || _this23.activeRedactType == '')) {
                    _this23.isRedact = true;
                } else {
                    _this23.isRedact = false;
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
            return val.split('T')[0];
        },
        formatDateDot: function formatDateDot(val) {
            if (!val) return;
            return val.split('T')[0].split('-').join('.');
        }
    }
});

function fixText(text) {
    var replaceRegex = /(\n\r|\r\n|\r|\n)/g;

    text = text || '';

    return text.replace(replaceRegex, "<br/>");
}
var appMyresume = new Vue({
    el: "#appMyresume",
    data: function data() {
        return {
            userId: 0,
            nickName: "",
            avatarUrl: "",
            isRedact: false,
            progress: 0,
            profileShow: false,
            educationShow: false,
            workExShow: false,
            tagsShow: false,
            jobIntentionShow: false,
            avoidShow: false,
            profile: {},
            snapUpdate: true,
            loading: {
                submitLoading: false
            }
        };
    },

    mounted: function mounted() {
        var _this24 = this;

        bus.getBasicInfo(this, function () {
            _this24.getProfile();
        });

        bus.$on('on-redact', function (option) {
            _this24.isRedact = option.isRedact;
        });

        this.snapUpdateStatus();

        window.addEventListener('scroll', this.handleScroll);
    },
    methods: {
        // 获取所有简历信息
        getProfile: function getProfile() {
            var _this25 = this;

            axiosGet(api.profile + "GetAllProfile", {
                userID: this.userId
            }).then(function (res) {
                if (res.code == "1") {
                    bus._data.isLocked = res.data.ApproveStatus == 1 ? true : false;

                    _this25.progress = 0;

                    // 基本信息
                    if (res.data.ProfileInfo !== null) {
                        _this25.profileShow = true;
                        _this25.progress += 25;
                    } else {
                        _this25.profileShow = false;
                    }
                    // 求职意向
                    if (res.data.JobIntention !== null) {
                        _this25.jobIntentionShow = true;
                        _this25.progress += 25;
                    } else {
                        _this25.jobIntentionShow = false;
                    }
                    // 工作
                    if (res.data.WorkExList !== null) {
                        _this25.workExShow = true;
                        _this25.progress += 25;
                    } else {
                        _this25.workExShow = false;
                    }
                    // 学习
                    if (res.data.EducationList !== null) {
                        _this25.educationShow = true;
                        _this25.progress += 25;
                    } else {
                        _this25.educationShow = false;
                    }
                    // 标签
                    if (res.data.UserTagsList !== null) {
                        _this25.tagsShow = true;
                    } else {
                        _this25.tagsShow = false;
                    }
                    // 屏蔽
                    if (res.data.AvoidList !== null) {
                        _this25.avoidShow = true;
                    } else {
                        _this25.avoidShow = false;
                    }

                    _this25.profile = res.data;
                } else {
                    _this25.$alert(res.msg, '提示');
                }
            });
        },

        // 获取简历快照修改状态
        snapUpdateStatus: function snapUpdateStatus() {
            var _this26 = this;

            axiosGet(api.profile + "SnapUpdateStatus").then(function (res) {
                if (res.code == 1) {
                    if (res.data.Profile || res.data.Education || res.data.WorkEx || res.data.JobIntention) {
                        _this26.snapUpdate = true;
                    } else {
                        _this26.snapUpdate = false;
                    }
                }
            });
        },

        // 提交审核
        bindToApprove: function bindToApprove() {
            var _this27 = this;

            this.loading.submitLoading = true;
            axiosGet(api.profile + "CommitApprove").then(function (res) {
                _this27.loading.submitLoading = false;
                if (res.code == 1) {
                    _this27.getProfile();
                    _this27.$message({
                        message: res.msg,
                        type: 'success'
                    });
                } else {
                    _this27.$alert(res.msg, '提示');
                }
            });
        },
        // 上下架
        bindToActive: function bindToActive(type) {
            var _this28 = this;

            this.loading.submitLoading = true;
            axiosGet(api.profile + "UpdateUserActivity", {
                status: type
            }).then(function (res) {
                _this28.loading.submitLoading = false;
                if (res.code == 1) {
                    _this28.getProfile();
                    _this28.$message({
                        message: res.msg,
                        type: 'success'
                    });
                } else {
                    _this28.$alert(res.msg, '提示');
                }
            });
        }

    }
});
var appTag = new Vue({
    el: "#appTag",
    data: function data() {
        return {
            nickName: "",
            avatarUrl: "",
            isRedact: false,
            isRedactType: 'appTag',
            activeRedactType: '',
            profileShow: false,
            isLocked: false,
            labelPosition: "top",
            tagList: [], // 所有tag
            tagListData: [],
            optTagList: [], // 选中tag
            form: {
                UserID: 0,
                Tags: ''
            },
            loading: {
                submitLoading: false
            }
        };
    },
    mounted: function mounted() {
        bus.getBasicInfo(this, function () {});
        this.getTagsList();
        this.pageBus();
    },

    methods: {
        getTagsList: function getTagsList() {
            var _this29 = this;

            axiosGet(api.profile + "GetTagsList").then(function (data) {
                _this29.tagList = data.data;

                // 初始化选中状态
                for (var i = 0; i < _this29.tagList.length; i++) {
                    for (var j = 0; j < _this29.tagList[i].ChildrenTag.length; j++) {
                        _this29.tagList[i].ChildrenTag[j]['isSelect'] = false;
                    }
                }

                _this29.getUserTagsList();
            });
        },
        getUserTagsList: function getUserTagsList() {
            var _this30 = this;

            axiosGet(api.profile + "GetUserTagsList", { UserID: this.form.UserID }).then(function (res) {
                if (res.code == 1 && res.data != null) {
                    _this30.tagListData = res.data;
                    _this30.optTagList = res.data;

                    // 添加选中状态
                    for (var i = 0; i < _this30.tagList.length; i++) {
                        for (var j = 0; j < _this30.tagList[i].ChildrenTag.length; j++) {
                            for (var k = 0; k < _this30.optTagList.length; k++) {
                                if (_this30.tagList[i].ChildrenTag[j].Tags === _this30.optTagList[k].Tags) {
                                    _this30.tagList[i].ChildrenTag[j]['isSelect'] = true;

                                    _this30.optTagList[k].PKID = _this30.tagList[i].ChildrenTag[j].PKID;
                                }
                            }
                        }
                    }
                }
            });
        },
        saveUserTags: function saveUserTags() {
            var _this31 = this;

            this.form.Tags = this.optTagList.map(function (e) {
                return e.PKID;
            }).join(',');

            this.loading.submitLoading = true;
            axiosPost(api.profile + "SaveUserTags", this.form).then(function (data) {
                if (data.code == 1) {
                    _this31.unRedact();
                    appMyresume.snapUpdateStatus();
                }
                _this31.loading.submitLoading = false;
                _this31.$alert(data.msg, '提示');
            });
        },
        tagClose: function tagClose(item) {
            this.updateTags(item.PKID, true);
        },
        tagClick: function tagClick(item) {
            this.updateTags(item.PKID, item.isSelect);
        },
        updateTags: function updateTags(id, type) {
            if (type) {
                for (var i = 0; i < this.tagList.length; i++) {
                    for (var j = 0; j < this.tagList[i].ChildrenTag.length; j++) {
                        if (id == this.tagList[i].ChildrenTag[j].PKID) {
                            this.tagList[i].ChildrenTag[j]['isSelect'] = false;
                        }
                    }
                }
                for (var k = 0; k < this.optTagList.length; k++) {
                    if (id === this.optTagList[k].PKID) {
                        this.optTagList.splice(k, 1);
                    }
                }
            } else {
                if (this.optTagList.length >= 10) {
                    this.$alert('最多只能选择10个标签', '提示');
                    return;
                }
                for (var i = 0; i < this.tagList.length; i++) {
                    for (var j = 0; j < this.tagList[i].ChildrenTag.length; j++) {
                        if (id == this.tagList[i].ChildrenTag[j].PKID) {
                            this.tagList[i].ChildrenTag[j]['isSelect'] = true;

                            this.optTagList.push({
                                Tags: this.tagList[i].ChildrenTag[j].Tags,
                                PKID: this.tagList[i].ChildrenTag[j].PKID
                            });
                        }
                    }
                }
            }
        },
        redact: function redact() {
            var _this32 = this;

            if (bus.lockMessage(this, bus._data.isLocked)) return;

            if (this.activeRedactType == '' || this.activeRedactType == this.isRedactType) {
                bus.$emit('on-redact', { isRedact: true, isRedactType: this.isRedactType });
            } else {
                this.$confirm(bus._data.switchRedactText, '提示', {
                    confirmButtonText: '退出',
                    cancelButtonText: '取消'
                }).then(function (active) {
                    bus.$emit('on-redact', { isRedact: true, isRedactType: _this32.isRedactType });
                }).catch(function () {});
            }
        },
        unRedact: function unRedact() {
            bus.$emit('on-redact', { isRedact: false, isRedactType: '' });
        },
        pageBus: function pageBus() {
            var _this33 = this;

            bus.$on('on-redact', function (option) {
                _this33.activeRedactType = option.isRedactType;

                if (option.isRedact && (_this33.activeRedactType == _this33.isRedactType || _this33.activeRedactType == '')) {
                    _this33.isRedact = true;
                } else {
                    _this33.isRedact = false;
                }
            });
        }
    }
});
var appWork = new Vue({
    el: "#appWork",
    data: function data() {
        var checkNull = function checkNull(rule, value, callback) {
            if (value.trim() == "") {
                callback(new Error('请输入正确格式的值'));
            } else {
                callback();
            }
        };
        return {
            isRedact: false,
            isRedactType: 'appWork',
            isRedactItemType: 'appWorkItem',
            activeRedactType: '',
            nickName: "",
            avatarUrl: "",
            profileShow: false,
            isLocked: false,
            labelPosition: "top",
            workList: [],
            newExpirationDate: false,
            editExpirationDate: false,
            form: {
                UserID: 0,
                CompanyName: "",
                Position: "",
                StartDate: "",
                ExpirationDate: "",
                Introduction: ""
            },
            editForm: {
                UserID: 0,
                PKID: 0,
                CompanyName: "",
                Position: "",
                StartDate: "",
                ExpirationDate: "",
                Introduction: ""
            },
            rules: {
                CompanyName: [{ required: true, message: '公司名称不能为空.', trigger: 'blur' }, { validator: checkNull, trigger: 'blur' }],
                Position: [{ required: true, message: '头衔不能为空.', trigger: 'blur' }, { validator: checkNull, trigger: 'blur' }],
                StartDate: [{ required: true, message: '开始时间不能为空.', trigger: 'blur' }],
                ExpirationDate: [{ required: true, message: '结束时间不能为空.', trigger: 'blur' }]
            },
            dialogEditWork: {
                index: 0,
                title: "修改简历",
                dialogVisible: false,
                diaClose: false,
                labelPosition: "top"
            },
            loading: {
                submitLoading1: false,
                saveLoading2: false
            }

        };
    },

    watch: {
        newExpirationDate: function newExpirationDate(news, old) {
            this.form.ExpirationDate = news ? '至今' : '';
        },
        editExpirationDate: function editExpirationDate(news, old) {
            this.editForm.ExpirationDate = news ? '至今' : '';
        }
    },
    mounted: function mounted() {
        bus.getBasicInfo(this, this.getWorksInfo);
        this.pageBus();
    },
    methods: {
        getWorksInfo: function getWorksInfo() {
            var _this34 = this;

            axiosGet(api.profile + "GetWorkExperienceList", {
                userID: this.form.UserID
            }).then(function (res) {
                if (res.code == "1" && res.data) {
                    for (var i = 0; i < res.data.length; i++) {
                        res.data[i]._Introduction = fixText(res.data[i].Introduction);
                        res.data[i].isRedact = false;
                    }
                    _this34.workList = res.data;
                } else {
                    _this34.workList = [];
                }
            });
        },
        submitForm: function submitForm(form) {
            var _this35 = this;

            this.$refs[form].validate(function (valid) {
                if (valid) {
                    _this35.loading.submitLoading = true;
                    if (_this35.newExpirationDate) {
                        _this35.form.ExpirationDate = '至今';
                    } else if (_this35.form.ExpirationDate) {
                        _this35.form.ExpirationDate = _this35.form.ExpirationDate.split('-').map(function (e) {
                            return e.toString().replace(/\b(0+)/gi, "");
                        }).join('-');
                    }
                    axiosPost(api.profile + "AddWorkExperience", _this35.form).then(function (res) {
                        _this35.loading.submitLoading = false;
                        if (res.code == "1") {
                            _this35.$alert(res.msg, '提示');
                            _this35.form.CompanyName = "";
                            _this35.form.Position = "";
                            _this35.form.StartDate = "";
                            _this35.form.ExpirationDate = "";
                            _this35.form.Introduction = "";
                            _this35.newExpirationDate = false;

                            _this35.unRedact();
                            _this35.getWorksInfo();
                            appMyresume.getProfile();
                            appMyresume.snapUpdateStatus();
                        } else {
                            _this35.$alert(res.msg, '提示');
                        }
                    }).catch(function (ex) {
                        _this35.loading.submitLoading = false;
                        _this35.$alert(ex.response.data.Message, '提示');
                    });
                } else {
                    return false;
                }
            });
        },
        redirectProfile: function redirectProfile() {
            var id = getUrlKey("id");
            window.location.href = "/profile/profile?id=" + id;
        },
        // 修改工作经历
        updateForm: function updateForm(form, i) {
            var _this36 = this;

            this.$refs[form][i].validate(function (valid) {
                if (valid) {
                    _this36.loading.submitLoading = true;
                    if (_this36.editExpirationDate) {
                        _this36.editForm.ExpirationDate = '至今';
                    } else {
                        _this36.editForm.ExpirationDate = _this36.editForm.ExpirationDate.split('-').map(function (e) {
                            return e.toString().replace(/\b(0+)/gi, "");
                        }).join('-');
                    }
                    axiosPost(api.profile + "UpdateWorkExperience", _this36.editForm).then(function (res) {
                        _this36.loading.submitLoading = false;
                        _this36.$alert(res.msg, '提示');
                        if (res.code == "1") {
                            _this36.editExpirationDate = false;

                            _this36.unRedactItem(i);
                            _this36.getWorksInfo();
                            appMyresume.snapUpdateStatus();
                        }
                    }).catch(function (ex) {
                        _this36.loading.submitLoading = false;
                        _this36.$alert(ex.response.data.Message, '提示');
                    });
                } else {
                    return false;
                }
            });
        },
        removeWork: function removeWork(val) {
            var _this37 = this;

            if (bus.lockMessage(this, bus._data.isLocked)) return;
            this.$confirm('确定要删除此条工作经历？', '提示').then(function (active) {
                axiosGet(api.profile + "DestroyWorkExperience", {
                    PKID: val
                }).then(function (res) {

                    if (res.code == "1") {
                        _this37.$message.success(res.msg, '提示');
                        _this37.getWorksInfo();
                        appMyresume.getProfile();
                        appMyresume.snapUpdateStatus();
                    } else {
                        _this37.$message.error(res.msg, '提示');
                    }
                });
            }).catch(function () {});
        },
        redactItem: function redactItem(item, index) {
            var _this38 = this;

            console.log(item);
            if (bus.lockMessage(this, bus._data.isLocked)) return;

            this.editForm.PKID = item.PKID;
            this.editForm.CompanyName = item.CompanyName;
            this.editForm.Position = item.Position;
            this.editForm.StartDate = item.StartDate;
            this.editForm.Introduction = item.Introduction;

            if (item.ExpirationDate == '至今') {
                this.editExpirationDate = true;
            } else {
                this.editForm.ExpirationDate = item.ExpirationDate;
            }

            this.isRedactItemType = this.isRedactItemType + index;

            if (this.activeRedactType == '' || this.activeRedactType == this.isRedactItemType) {
                bus.$emit('on-redact', { isRedact: true, isRedactType: this.isRedactItemType, itemIndex: index });
            } else {
                this.$confirm(bus._data.switchRedactText, '提示', {
                    confirmButtonText: '退出',
                    cancelButtonText: '取消'
                }).then(function (active) {
                    bus.$emit('on-redact', { isRedact: true, isRedactType: _this38.isRedactItemType, itemIndex: index });
                }).catch(function () {});
            }
        },
        unRedactItem: function unRedactItem(index) {
            bus.$emit('on-redact', { isRedact: false, isRedactType: '', itemIndex: index });
        },
        redact: function redact() {
            var _this39 = this;

            if (bus.lockMessage(this, bus._data.isLocked)) return;

            if (this.activeRedactType == '' || this.activeRedactType == this.isRedactType) {
                bus.$emit('on-redact', { isRedact: true, isRedactType: this.isRedactType });
            } else {
                this.$confirm(bus._data.switchRedactText, '提示', {
                    confirmButtonText: '退出',
                    cancelButtonText: '取消'
                }).then(function (active) {
                    bus.$emit('on-redact', { isRedact: true, isRedactType: _this39.isRedactType });
                }).catch(function () {});
            }
        },
        unRedact: function unRedact() {
            bus.$emit('on-redact', { isRedact: false, isRedactType: '' });
        },
        pageBus: function pageBus() {
            var _this40 = this;

            bus.$on('on-redact', function (option) {
                _this40.activeRedactType = option.isRedactType;

                for (var i = 0; i < _this40.workList.length; i++) {
                    _this40.workList[i].isRedact = false;
                }

                if (option.itemIndex || option.itemIndex === 0) {
                    if (option.isRedact && (_this40.activeRedactType == _this40.isRedactItemType || _this40.activeRedactType == '')) {
                        _this40.workList[option.itemIndex].isRedact = true;
                    }
                } else {
                    if (option.isRedact && (_this40.activeRedactType == _this40.isRedactType || _this40.activeRedactType == '')) {
                        _this40.isRedact = true;
                    } else {
                        _this40.isRedact = false;
                    }
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
        }
    }
});

function fixText(text) {
    var replaceRegex = /(\n\r|\r\n|\r|\n)/g;

    text = text || '';

    return text.replace(replaceRegex, "<br/>");
}