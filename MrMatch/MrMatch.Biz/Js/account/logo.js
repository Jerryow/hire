var vm = new Vue({
    el: "#appLogo",
    data: {
        logo: "",
        uploadImg: '',
        imgT: "data:image/gif;base64,",
        header: {
            Authorization: "Bear " + _ticket
        },
        uploadFiles: {
            imgFile: null
        },
        ImgBytes:"",
        imgShow: true,
        loading: {
            submitLoading: false
        },

        dialogVisible: false,
        dialogImageUrl: ''
    },
    mounted: function () {
        this.getBasicInfo();
    },
    methods: {
        pictureCardPreview(url) {
            this.dialogImageUrl = url;
            this.dialogVisible = true;
        },
        getBasicInfo: function () {
            this.imgShow = true;
            this.uploadImg = "";
            this.uploadFiles.imgFile = null;
            axiosGet(api.account + "GetCompanyLogo")
                .then((res) => {
                    if (res.data != null) {
                        this.logo = res.data.url;
                    }
                })
        },
        uploadLogo: function () {
            if (!this.ImgBytes) {
                this.$alert("请选择先更新的图片后提交.", '提示')
                return
            }

            //var formdata1 = new FormData();// 创建form对象
            //formdata1.append('imgFile', this.uploadFiles.imgFile, this.uploadFiles.imgFile.name+".png");// 通过append向form对象添加数据,可以通过append继续添加数据
            //
            //let config = {
            //    headers: {
            //        'Content-Type': 'multipart/form-data',
            //        "Authorization": "Bear " + _ticket
            //    }
            //}

            this.loading.submitLoading = true;
            //axios.post("/api/accountapi/UploadLogo", formdata1,config)
            //    .then((res) => {
            //        this.loading.submitLoading = false;
            //        if (res.data.code == "1") {
            //            this.$alert("上传成功",'提示');
            //            this.getBasicInfo();
            //        } else {
            //            this.$alert("上传失败,请刷新页面后重试.",'提示')
            //        }
            //})
            axiosPost(api.account+"UploadLogo", {
                ImgBytes: this.ImgBytes
            })
                .then((res) => {
                    this.loading.submitLoading = false;
                    if (res.code == "1") {
                        this.$alert("上传成功",'提示');
                        this.getBasicInfo();
                    } else {
                        this.$alert("上传失败,请刷新页面后重试.",'提示')
                    }
            })
        },
        beforeAvatarUpload(file) {

            var img = "";
            var reader = new FileReader();
            reader.readAsDataURL(file);
            reader.onload = (e) => {
                img = e.target.result;
                console.log(img.split(','));
                this.ImgBytes = img.split(',')[1];
                this.uploadImg = img;
                this.imgShow = false;

                //var blob = this.dataURLtoBlob(img);
                //console.log(blob);
                //var file = this.blobToFile(blob, 'imgFile');
                //console.log(file);
                //this.uploadFiles.imgFile = file;
            }

            return false;
        },
        dataURLtoBlob(dataUrl) {
            var arr = dataUrl.split(','),
                mime = arr[0].match(/:(.*?);/)[1],
                bstr = atob(arr[1]),
                n = bstr.length,
                u8arr = new Uint8Array(n);
            while (n--) {
                u8arr[n] = bstr.charCodeAt(n);
            }
            return new Blob([u8arr], { type: mime });
        },
        blobToFile(theBlob, fileName) {
            theBlob.lastModifiedDate = new Date();
            theBlob.name = fileName;
            var file = new File([theBlob], fileName, { type: 'image/png', lastModified: Date.now() });
            return file;
        },
    }
})