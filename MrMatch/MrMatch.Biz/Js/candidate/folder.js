var vm = new Vue({
    el: "#appFolder",
    data: {
        contract: false,
        contractHelp: "",
        form: {
            pageIndex: 1,
            pageSize: 10,
            cities: "",
            salary: "",
            functions: "",
            position: "",
            years: 0,
            folderId:0
        },
        profile: [],
        folders: [],
    },
    mounted: function () {
        this.getBasicProfile();
        this.getFolder();
    },
    methods: {
        getBasicProfile: function () {
            axiosGet(api.candidate + "GetAllFollowList", this.form)
                .then((res) => {
                    this.contract = res.data.contract;
                    this.contractHelp = res.data.contractHelp;
                    this.profile = res.data.data.DataList;
                })
        },
        getFolderProfile: function () {
            axiosGet(api.candidate + "GetUserFollowList", this.form)
                .then((res) => {
                    this.contract = res.data.contract;
                    this.contractHelp = res.data.contractHelp;
                    this.profile = res.data.data.DataList;
                })
        },
        getFolder: function () {
            axiosGet(api.candidate + "GetAccountFolders")
                .then((res) => {
                    this.folders = res.data;
                })
        }
    }
})