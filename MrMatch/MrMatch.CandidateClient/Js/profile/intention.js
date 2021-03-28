var appIntention = new Vue({
    el: "#appIntention",
    data() {
        return {
            isAdded:false,
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
            },
        };
    },
    filters: {
        functionNameFilters: function (val) {
            console.log(val)
            var str = ''
            for (var i = 0; i < this.functionList.length; i++) {
                if (val[0] == this.functionList[i].value) {
                    str += this.functionList[i].label

                }
            }

            return 0;
        }
    },
    mounted: function () {
        //this.getBasicInfo()

        bus.getBasicInfo(this, this.getBasicInfo)
        this.pageBus()

    },
    methods: {
        getBasicInfo: function () {
            this.getFunctionList();
            this.getDistrictList();
        },
        // 获取职能数据
        getFunctionList: function () {
            axiosGet(api.profile + "GetFunctionForCascader")
                .then((data) => {
                    if (data.code == 1) {
                        this.FunctionsList = data.data
                        this.FunctionsList.unshift({
                            label: "暂不选择",
                            value: -1
                        })

                        this.getJobIntention();
                    }
                });
        },
        // 获取城市数据
        getDistrictList: function () {
            axiosGet(api.profile + "GetAllDistrictList")
                .then((data) => {
                    if (data.code == 1) {
                        this.districtList = data.data.hot
                        this.allDistrictList = data.data.basic

                        // 
                        this.initDistrictList()


                        //for (var i = 0; i < this.districtList.length; i++) {
                        //    this.districtNameList.push(this.districtList[i].DistrictName)
                        //}
                    }
                });
        },
        // 更新城市数据状态
        initDistrictList() {
            let districtList = this.districtList
            let allDistrictList = this.allDistrictList
            let LocationNames = []
            var _arr = this.LocationIDs 

            console.log(100)
            // 更新热门城市选中状态
            for (let i = 0; i < districtList.length; i++) {
                districtList[i].checked = false

                for (let j = 0; j < _arr.length; j++) {
                    if (_arr[j] == districtList[i].PKID) {
                        districtList[i].checked = true
                    }
                }
            }

            // 更新全部城市选中状态
            for (let i = 0; i < allDistrictList.length; i++) {
                for (let k = 0; k < allDistrictList[i].Children.length; k++) {
                    allDistrictList[i].Children[k].checked = false

                    for (let j = 0; j < _arr.length; j++) {
                        if (_arr[j] == allDistrictList[i].Children[k].PKID) {
                            allDistrictList[i].Children[k].checked = true
                            LocationNames.push(allDistrictList[i].Children[k].DistrictName)
                        }
                    }
                }

            }

            this.districtList = null
            this.allDistrictList = null

            this.districtList = districtList
            this.allDistrictList = allDistrictList
            this.LocationNames = LocationNames


        },
        getJobIntention: function () {
            var _this = this
            axiosGet(api.profile + "GetJobIntention", { UserID: this.form.UserID })
                .then((data) => {
                    if (data.code == 1) {
                        console.log(this.form.PKID)
                        if (data.data != null) {
                            this.form.AnnualSalary = data.data.AnnualSalary ? data.data.AnnualSalary : ''
                            this.form.FunctionIDs = data.data.FunctionIDs
                            this.form.Functions = data.data.Functions
                            this.form.LocationIDs = data.data.LocationIDs
                            this.form.Locations = data.data.Locations
                            this.form.PKID = data.data.PKID

                            this.intentionInfo = data.data

                            this.LocationIDs = data.data.LocationIDs.split(',').map(function (e) {
                                return parseInt(e)
                            })
                            this.checkedCities = data.data.Locations.split(',')
                            this.functionIDs = data.data.FunctionIDs.split(',').map(function (e, index) {
                                _this.updateData(e.split('-'), index, 'function')
                                return e.split('-')
                            })
                            //this.intentionIDs = data.data.IntentionFunctionIDs.split(',').map(function (e, index) {
                            //    _this.updateData(e.split('-'), index, 'intention')
                            //    return e.split('-')
                            //})

                            this.initDistrictList()

                            this.isAdded = false
                        } else {
                            this.isAdded = true
                        }
                    }
                });
        },
        submitForm: function (form) {

            var _form = {
                UserID: this.form.UserID,
                FunctionIDs: this.form.FunctionIDs,
                LocationIDs: this.LocationIDs.join(','),
                AnnualSalary: this.form.AnnualSalary
            }

            if (_form.FunctionIDs.trim() == "") {
                this.$alert("请选择您过往的职能资历", '提示');
                return false;
            }
            if (_form.LocationIDs.trim() == "") {
                this.$alert("请选择您的意向城市", '提示');
                return false;
            }


            this.$refs[form].validate((valid) => {
                if (valid) {
                    this.loading.submitLoading = true
                    if (this.isAdded) {

                        axiosPost(api.profile + "AddJobIntention", _form)
                            .then((res) => {
                                this.loading.submitLoading = false
                                this.$alert(res.msg, '提示');
                                if (res.code == "1") {
                                    this.unRedact()
                                    this.intentionNames = []
                                    this.functionNames = []
                                    this.getJobIntention()
                                    appMyresume.getProfile();
                                    appMyresume.snapUpdateStatus();
                                }
                            })
                            .catch((ex) => {
                                this.loading.submitLoading = false
                                this.$aler(ex.response.data.Message, '提示');
                            });
                    } else {
                        _form.PKID = this.form.PKID
                        axiosPost(api.profile + "UpdateJobIntention", _form)
                            .then((res) => {
                                this.loading.submitLoading = false
                                this.$alert(res.msg, '提示');
                                if (res.code == "1") {
                                    this.unRedact()
                                    this.intentionNames = []
                                    this.functionNames = []
                                    this.getJobIntention()
                                    appMyresume.getProfile();
                                    appMyresume.snapUpdateStatus();
                                }
                            })
                            .catch((ex) => {
                                this.loading.submitLoading = false
                                this.$aler(ex.response.data.Message, '提示');
                            });

                    }
                } else {
                    return false;
                }
            });
        },
        redirectProfile: function () {
            var id = getUrlKey("id");
            window.location.href = "/profile/profile?id=" + id;
        },
        // 变更城市 //作废
        checkedCitiesChange(e) {
            console.log(e)
            var arr = []
            for (var i = 0; i < e.length; i++) {
                for (var j = 0; j < this.districtList.length; j++) {
                    if (e[i] == this.districtList[j].DistrictName) {
                        arr.push(this.districtList[j].PKID)
                    }
                }
            }


            this.form.LocationIDs = arr.join(',')

        },
        // 选择城市
        cityClick(item,i) {
            let LocationIDs = this.LocationIDs ? this.LocationIDs : []
            let LocationNames = this.LocationNames

            for (let k = 0; k < LocationIDs.length; k++) {
                if (item.PKID == LocationIDs[k]) {
                    this.LocationIDs.splice(k, 1)
                    this.initDistrictList()
                    return
                }
            }

            if (LocationIDs.length >= 3) {
                this.$message("最多选择三个意向城市");
                return
            }

            this.LocationIDs.push(item.PKID)
            this.initDistrictList()

        },
        // 简历概括
        clickFunctionItem: function (i, type) {
            this.selectType = type

            this.functionIndex = i
            this.drawer = true

            this.secondList = []
            this.thirdList = []
        },
        firstListClick: function (i) {
            if (this.FunctionsList[i].value == -1) {
                this.updateData(-1, this.functionIndex)
            } else {
                this.functionId[0] = this.FunctionsList[i].value
                this[this.selectType + 'Id'][0] = this.FunctionsList[i].value
                this.secondList = this.FunctionsList[i].children
                this.thirdList = []
            }
        },
        secondListClick: function (i) {
            this[this.selectType + 'Id'][1] = this.secondList[i].value
            if (this.secondList[i].children) {
                this.thirdList = this.secondList[i].children
            } else {
                if (this[this.selectType + 'Id'][2]) {
                    this[this.selectType + 'Id'].pop()
                }
                this.addFunctionItem()
            }
        },
        thirdListClick: function (i) {
            this[this.selectType + 'Id'][2] = this.thirdList[i].value
            this.addFunctionItem()
        },
        addFunctionItem: function (item) {
            console.log(0, this[this.selectType + 'Id'])

            this.drawer = false
            this.updateData(this[this.selectType + 'Id'], this.functionIndex)

            console.log(1, this[this.selectType + 'IDs'])
        },
        // 更新数据
        updateData(val, index, type) {
            if (type) this.selectType = type
            var str = ''
            var arr = []
            if (val == -1) {
                this[this.selectType + 'Names'][index] = null
                this[this.selectType + 'IDs'][index] = []
                this.drawer = false
            } else {
                for (var i = 0; i < this.FunctionsList.length; i++) {
                    if (val[0] == this.FunctionsList[i].value) {
                        str += this.FunctionsList[i].label;
                        arr[0] = this.FunctionsList[i].value

                        for (var j = 0; j < this.FunctionsList[i].children.length; j++) {
                            if (val[1] == this.FunctionsList[i].children[j].value) {

                                str += (' / ' + this.FunctionsList[i].children[j].label);
                                arr[1] = this.FunctionsList[i].children[j].value

                                if (val.length == 3) {
                                    for (var k = 0; k < this.FunctionsList[i].children[j].children.length; k++) {

                                        if (val[2] == this.FunctionsList[i].children[j].children[k].value) {
                                            str += (' / ' + this.FunctionsList[i].children[j].children[k].label);
                                            arr[2] = this.FunctionsList[i].children[j].children[k].value
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
                    return e.join('-')
                }).join(',').replace(/^,+|,+$/g, "").replace(",,", ",")
            }
            if (this.selectType == 'intention') {
                this.form['IntentionFunctionIDs'] = this[this.selectType + 'IDs'].map(function (e, i) {
                    //var a = e ? e.join('-'):''
                    return e.join('-')
                }).join(',').replace(/^,+|,+$/g, "").replace(",,", ",")
            }


        },
        redact: function () {
            if (bus.lockMessage(this, bus._data.isLocked)) return

            if (this.activeRedactType == '' || this.activeRedactType == this.isRedactType) {
                bus.$emit('on-redact', { isRedact: true, isRedactType: this.isRedactType });
            } else {
                this.$confirm(bus._data.switchRedactText, '提示', {
                    confirmButtonText: '退出',
                    cancelButtonText: '取消',
                }).then( (active)=> {
                    bus.$emit('on-redact', { isRedact: true, isRedactType: this.isRedactType });
                }).catch(() => {
                });
            }
        },
        unRedact: function () {
            this.isRedact = false
            bus.$emit('on-redact', { isRedact: false, isRedactType: '' });
        },
        pageBus: function () {
            bus.$on('on-redact',  (option)=> {
                this.activeRedactType = option.isRedactType;

                if (option.isRedact && (this.activeRedactType == this.isRedactType || this.activeRedactType == '')) {
                    this.isRedact = true
                } else{
                    this.isRedact = false
                }
            })
        }
    }
});