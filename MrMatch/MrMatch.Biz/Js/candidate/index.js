var vm = new Vue({
    el: "#appCandidate",
    data: {
        contract: false,
        profile: null,
        profileInfoList: {},
        searchLogList: null,
        searchLogName: null,
        form: {
            pageIndex: 1,
            pageSize: 10,
            total: 0,
            cities: '',
            salary: '',
            functions: '',
            position: '',
            years: 0
        },
        functionsDefault: null,
        cities: '',
        salaryList: [
            { value: '0', selected: false },
            { value: '120', selected: false },
            { value: '150', selected: false },
            { value: '200', selected: false },
            { value: '250', selected: false },
            { value: '300', selected: false },
            { value: '400', selected: false },
            { value: '500', selected: false },
            { value: '800', selected: false },
            { value: '1000', selected: false },
            { value: '1200', selected: false },
            { value: '1500', selected: false },
            { value: '2000', selected: false }
        ],
        salary: null,
        salaryMax: null,
        salaryMin: null,
        props: { multiple: true },
        cityOptions: [],
        jobOptions: [],
        jobOptionsHot: [],
        headerText: false,

        accountFolders: [],
        newFolderName: '',
        saveFolders: null,
        saveFoldersUserId: 0,
        saveFoldersIndex: 0,

        salaryDialogVisible: false,
        searchLogDialogVisible: false,
        followUserDialogVisible: false,
        addfollowUserDialogVisible: false,

        loading: {
            searchResultLoading: false,
            searchLogLoading: false,
        }
    },
    mounted: function () {
        this.getProfileList();
        this.getFunctionForCascader();
        this.getAllDistrictList();
        this.getSearchLogList();
        this.getAccountFolders();
    },
    computed: {
    },
    watch: {
        'form.years': function (curVal, oldVal) {
            if (!curVal) {
                this.form.years = ''
                return false
            }
            // 实时把非数字的输入过滤掉
            this.form.years = curVal.match(/\d/ig) ? curVal.match(/\d/ig).join('') : ''
        },
        //'salaryMin': function (curVal, oldVal) {
        //    if (!curVal) {
        //        this.salaryMin = ''
        //        return false
        //    }
        //    // 实时把非数字的输入过滤掉
        //    this.salaryMin = curVal.match(/\d/ig) ? curVal.match(/\d/ig).join('') : ''
        //    if (this.salaryMax !== null) {
        //        this.salary = this.salaryMin + '~' + this.salaryMax
        //    } else {
        //        this.salary = this.salaryMin
        //    }
        //},
        //'salaryMax': function (curVal, oldVal) {
        //    console.log(curVal)
        //    if (!curVal) {
        //        this.salaryMax = ''
        //        return false
        //    }
        //    // 实时把非数字的输入过滤掉
        //    this.salaryMax = curVal.match(/\d/ig) ? curVal.match(/\d/ig).join('') : ''
        //    if (this.salaryMin !== null) {
        //        this.salary = this.salaryMin + '~' + this.salaryMax
        //    } else {
        //        this.salary = '0,' + this.salaryMax
        //    }
        //}
    },
    methods: {
        // 获取职位
        getFunctionForCascader: function () {
            axiosGet(api.candidate + "GetFunctionForCascader", this.form)
                .then((res) => {
                    if (res.code == 1) {
                        this.jobOptions = res.data

                        for (var i = 0; i < res.data.length; i++) {
                            if (res.data[i].IsPopular) {
                                this.jobOptionsHot.push(i)
                            }
                        }
                    }
                })
        },
        // 获取城市
        getAllDistrictList: function () {
            axiosGet(api.candidate + "GetAllDistrictList", this.form)
                .then((res) => {
                    if (res.code == 1) {
                        this.cityOptions = res.data
                    }
                })
        },
        // 获取搜索结果
        getProfileList: function () {
            this.loading.searchResultLoading = true;
            axiosGet(api.candidate + "GetProfileList", this.form)
                .then((res) => {
                    this.loading.searchResultLoading = false;
                    if (res.code == 1) {
                        this.contract = res.data.contract
                        this.profile = res.data.data.DataList
                        this.form.total = res.data.data.Total
                        this.form.pageIndex = res.data.data.PageIndex
                    } else {
                        this.$confirm(res.msg, '提示', {
                            confirmButtonText: '确定',
                            showCancelButton: false
                        })
                    }
                })
        },
        // 提交搜索
        search: function () {
            this.form.pageIndex = 1
            this.form.years = this.form.years == '' ? 0 : this.form.years

            this.getProfileList()
        },
        // 重置搜索
        resetSearch: function () {
            this.form = {
                pageIndex: 1,
                pageSize: 10,
                total: 0,
                cities: '',
                salary: '',
                functions: '',
                position: '',
                years: 0
            }
            this.salary = null
            this.functionsDefault = null
            this.cities = null
        },
        // 选择城市
        citiesChange: function () {
            this.form.cities = this.cities.join(',');
        },
        // 选择职能
        jobChang: function (e) {
            console.log('jobChang', e)
            var str = ''
            for (var i = 0; i < e.length; i++) {
                str += i == 0 ? e[i].join('-') : (',' + e[i].join('-'))
            }
            this.form.functions = str

            this.jobCascaderVisibleChange()
        },
        // 联级选择器显隐
        jobCascaderVisibleChange(e) {
            var _this = this
            var timer = setTimeout(function () {
                var jobCascader = document.getElementsByClassName('jobCascader')[0]
                console.log(jobCascader)
                var scrollbar = jobCascader.getElementsByClassName('el-scrollbar')[0]
                console.log(scrollbar)
                var item = scrollbar.getElementsByTagName('li')
                console.log(item)

                for (var i = 0; i < _this.jobOptionsHot.length; i++) {
                    for (var j = 0; j < item.length; j++) {
                        if (_this.jobOptionsHot[i] == j) {
                            item[j].className = item[j].className + ' hot'
                        }
                    }
                }
            },100) 
        },
        // 确定年薪
        salaryChange: function () {
            //if (this.salary && this.salaryMax === null && this.salaryMin === null) {
            //    this.form.salary = this.salary
            //} 

            var salaryMax = parseInt(this.salaryMax)
            var salaryMin = parseInt(this.salaryMin)

            console.log(salaryMax < salaryMin, salaryMax, salaryMin, salaryMax < salaryMin)

            if (isNaN(salaryMax)) {
                this.$confirm('请输入年薪上限', '提示', {
                    confirmButtonText: '确定',
                    showCancelButton: false
                })
                return
            }
            if (isNaN(salaryMin)) {
                this.$confirm('请输入年薪下限', '提示', {
                    confirmButtonText: '确定',
                    showCancelButton: false
                })
                return
            }
            if (salaryMin > salaryMax) {
                this.$confirm('年薪下限要小于上限', '提示', {
                    confirmButtonText: '确定',
                    showCancelButton: false
                })
            } else if (isNaN(salaryMax) && isNaN(salaryMin)) {
                this.salary = ''
                this.form.salary = ''
                this.salaryDialogVisible = false
            }else{
                this.salary = salaryMin + '~' + salaryMax
                this.form.salary = this.salary.replace('~', ',')
                this.salaryDialogVisible = false
            }
        },
        // 根据招聘岗位条件进行搜索
        getSearchLog: function (url) {
            this.form = JSON.parse(url)
            this.salary = this.form.salary
            this.functionsDefault = []
            this.cities = this.form.cities.split(',').map(function (e) {
                return parseInt(e)
            })
            var functions = this.form.functions.split(',')
            for (var i = 0; i < functions.length; i++) {
                this.functionsDefault.push(functions[i].split('-'))
            }

            this.getProfileList()
        },
        // 获取招聘岗位列表
        getSearchLogList: function () {
            axiosGet(api.candidate + "GetSearchLogList", this.form)
                .then((res) => {
                    if (res.code == 1) {
                        this.searchLogList = res.data
                    }
                })
        },
        // 新建招聘岗位
        saveSearchLog: function () {
            var urls = this.form
            urls.pageIndex = 1
            urls.pageSize = 10

            if (this.searchLogName == null || this.searchLogName == '') {
                this.$alert('请输入岗位名称', '提示')
                return false
            }
            this.loading.searchLogLoading = true
            axiosGet(api.candidate + "SaveSearchLog", {
                name: this.searchLogName,
                url: urls
            }).then((res) => {
                this.loading.searchLogLoading = false
                if (res.code == 1) {
                    this.searchLogName = ''
                    this.searchLogDialogVisible = false
                    this.$alert('新增成功', '提示')
                    this.getSearchLogList()
                } else {
                    this.$alert(res.msg, '提示')
                }
            })
        },
        // 删除招聘岗位
        delSearchLog: function (id, name) {
            this.$confirm('确定删除记录 ' + name, '提示').then(active => {
                axiosGet(api.candidate + "DestroySearchLog", {
                    PKID: id
                }).then((res) => {
                    if (res.code == 1) {
                        this.$alert('删除成功', '提示')
                        this.getSearchLogList()
                    } else {
                        this.$alert(res.msg, '提示')
                    }
                })
            })
        },
        // 获取用户收藏夹
        getAccountFolders: function () {
            axiosGet(api.candidate + "GetAccountFolders").then((res) => {
                if (res.code == 1) {
                    this.accountFolders = res.data
                }
            })
        },
        // 新建收藏夹
        saveUserFolder: function () {
            axiosPost(api.candidate + "SaveUserFolder", {
                PKID: 0,
                FolderName: this.newFolderName
            }).then((res) => {
                if (res.code == 1) {
                    this.addfollowUserDialogVisible = false
                    this.getAccountFolders()
                    if (!this.followUserDialogVisible) {
                        this.followUserDialogVisible = true
                    }
                } else {
                    this.$alert(res.msg, '提示')
                }
            })
        },
        // 点击收藏按钮 
        clickFollowUser: function (id, index) {
            this.saveFoldersUserId = id;
            this.saveFoldersIndex = index;

            if (this.accountFolders.length > 0) {
                this.followUserDialogVisible = true;
            } else {
                this.addfollowUserDialogVisible = true
            }
        },
        // 点击收藏
        followUser: function () {
            axiosGet(api.candidate + "FollowUser", {
                userId: this.saveFoldersUserId,
                folderIds: this.saveFolders.join(',')
            }).then((res) => {
                if (res.code == 1) {
                    this.followUserDialogVisible = false
                    this.profile[this.saveFoldersIndex].IsCollected = true
                    this.$alert('收藏成功', '提示')
                } else if (res.code == 0) {
                    this.$alert('请选择收藏夹', '提示')
                } else {
                    this.$alert(res.msg, '提示')
                }
            })
        },
        // 点击取消收藏
        unFollowUser: function () {
            this.$alert('请到收藏页面操作', '提示')
        },
        // 切换list
        workListToggle: function (id) {
            var lis = this.$refs['workList' + id][0].getElementsByTagName('li')
            var btn = this.$refs['workListBtn' + id][0].getElementsByTagName('i')[0]
            if (lis[lis.length - 1].className.indexOf('hidden') !== -1) {
                for (var i = 0; i < lis.length; i++) {
                    lis[i].className = ""
                }
            } else {
                for (var i = 0; i < lis.length; i++) {
                    if (i > 1) {
                        lis[i].className = "hidden"
                    }
                }

            }
            if (btn.className.indexOf('fa-angle-double-down') !== -1) {
                btn.className = btn.className.replace("fa-angle-double-down", "fa-angle-double-up")
            } else {
                btn.className = btn.className.replace("fa-angle-double-up", "fa-angle-double-down")
            }
        },
        // 点击分页
        currentChange: function (e) {
            this.form.pageIndex = e
            this.getProfileList()
        },
        // 计算是否本周发布
        computePutTime: function (time) {
            var last = Date.parse(time)
            var now = Date.now()
            return now - last <= 60 * 60 * 24 * 7 * 1000
        },
        // 计算单个工作时间
        formatTime: function (a, b) {
            var x = '';
            var start = a.slice(0, 7).split('-');
            var end = b.slice(0, 7).split('-');

            start[1] = start[1].replace(/\b(0+)/gi, "");
            end[1] = end[1].replace(/\b(0+)/gi, "");

            var year = 0;
            var month = 0;
            if (end[1] < start[1] && end[0] > start[0]) {
                year = end[0] - start[0] - 1;
                month = 12 - end[1] + start[1];

                x = year == 0 ? (month + '个月') : (year + '年' + month + '个月');
            } else if (end[1] >= start[1] && end[0] > start[0]) {
                year = end[0] - start[0];
                month = end[1] - start[1];

                x = year == 0 ? (month + '个月') : (month == 0 ? (year + '年') : (year + '年' + month + '个月'));
            } else {
                x = '--';
            }
            return x;
        }
    },
    filters: {
        monthFormat: function (val) {
            if (val == "至今") {
                return "至今"
            }
            return val.split('-')[0] + "." + val.split("-")[1];
        },
        dateFormat: function (val) {
            var year = val.split("T")[0].split('-')[0];
            var month = val.split("T")[0].split('-')[1].replace(/^0/, "");
            return year + "." + month;
        }
    }
})