var vm = new Vue({
    el: "#appLetter",
    data: {
        existingLetter: 0,
        limiteLetter: 0,
        form: {
            PKID: '',
            TemplateName: '',
            TemplateContent: ''
        },
        templateIndex: null,
        letterTemplateList: [],

        editDialogVisible: false
    },
    mounted: function () {
        this.getLetterList();
    },
    methods: {
        // 获取模板列表
        getLetterList: function () {
            axiosGet(api.account + "GetLetterList")
                .then((res) => {
                    if (res.code == 1) {
                        this.letterTemplateList = res.data.letterList
                        this.existingLetter = res.data.existingLetter
                        this.limiteLetter = res.data.limiteLetter
                    }
                })
        },
        // 获取模板列表
        getLetterDetails: function (id) {
            axiosGet(api.account + "GetLetterTemplateDetail?PKID="+id)
                .then((res) => {
                    if (res.code == 1) {
                        this.form.PKID = res.data.PKID;
                        this.form.TemplateName = res.data.TemplateName;
                        this.form.TemplateContent = res.data.TemplateContent;
                    } else {
                        this.$alert(res.msg, '提示');
                    }
                })
        },
        // 新建模板列表
        saveLetterTemplateDate: function () {
            axiosPost(api.account + "SaveLetterTemplate", this.form)
                .then((res) => {
                    if (res.code == 1) {
                        
                        this.$alert('保存成功', '提示')
                        this.getLetterList()
                        this.handleClose();
                    }
                })
        },
        // 删除模板列表
        destroyLetter: function (id) {
            this.$confirm('确认删除?', '提示').then(res => {
                axiosGet(api.account + "DestroyLetter?PKID=" + id)
                    .then((res) => {
                        if (res.code == 1) {
                            this.$alert('删除成功', '提示')
                            this.getLetterList()
                        }
                    })
            })
        },
        handleClose: function () {
            this.form.PKID = "";
            this.form.TemplateName = "";
            this.form.TemplateContent = "";
            this.editDialogVisible = false;
        }
    }
})