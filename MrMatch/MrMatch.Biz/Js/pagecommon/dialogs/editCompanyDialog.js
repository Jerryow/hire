Vue.component('editCompanyDialog', {
    name: 'editCompanyDialog',
    props: {
        option: null,
        visible: {
            type: Boolean,
            default: false
        },
        id: 0,
    },
    data: function () {
        return {
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
                Summary: '',
                Introduce: ''
            },
            isEdit: false,
            editCompanyDialog: false,
            comType: [
                { ID: 11, Name: "国有企业" },
                { ID: 12, Name: "集体企业" },
                { ID: 13, Name: "股份合作企业" },
                { ID: 14, Name: "联营企业" },
                { ID: 15, Name: "有限责任公司" },
                { ID: 16, Name: "股份有限公司" },
                { ID: 17, Name: "私营企业" },
                { ID: 18, Name: "港澳台商投资企业" },
                { ID: 19, Name: "外商投资企业" }
            ],
            employeeNum: [
                { ID: 1, Name: "少于15人" },
                { ID: 2, Name: "15-50人" },
                { ID: 3, Name: " 50-150人" },
                { ID: 4, Name: "150-500人" },
                { ID: 5, Name: " 500-1000人" },
                { ID: 6, Name: "1000-5000人" },
                { ID: 7, Name: "5000-10000人" },
                { ID: 8, Name: "10000人以上" }
            ]
        }
    },
    template:
        `
        <el-dialog :title="(isEdit?'编辑':'新增')+'企业信息'"
                   :visible.sync="editCompanyDialog"
                   width="650px">
            <div class="" style="padding:0 20px;">
                <div class="row">
                    <div class="col-md-12">
                        <el-form :model="form" :rules="rules" ref="form" label-width="120px" label-position="left" class="publish-form">
                            <div class="row margin-bottom-10 margin-top-10">
                                <div class="col col-12 ">
                                    <el-form-item label="公司全称" prop="CompanyName" >
                                        <el-input v-model="form.CompanyName" placeholder="请输入公司全称"></el-input>
                                    </el-form-item>
                                </div>
                            </div>
                            <div class="row margin-bottom-10" >
                                <div class="col col-12 ">
                                    <el-form-item label="对外显示名称" prop="ShortName" >
                                        <el-input v-model="form.ShortName"  placeholder="请输入对外显示名称"></el-input>
                                    </el-form-item>
                                </div>
                            </div>
                            <div class="row margin-bottom-10" >
                                <div class="col col-12 ">
                                    <el-form-item label="所在城市" prop="City" >
                                        <el-input v-model="form.City"  placeholder="请输入所在城市"></el-input>
                                    </el-form-item>
                                </div>
                            </div>
                            <div class="row margin-bottom-10" >
                                <div class="col col-12 ">
                                    <el-form-item label="企业类型" prop="CompnayType" >
                                        <el-select v-model="form.CompnayType" placeholder="请选择企业类型"  style="width:100%;">
                                            <el-option v-for="item in comType"
                                                        :key="item.ID"
                                                        :label="item.Name"
                                                        :value="item.ID">
                                            </el-option>
                                        </el-select>
                                    </el-form-item>
                                </div>
                            </div>
                            <div class="row margin-bottom-10" >
                                <div class="col col-12 ">
                                    <el-form-item label="企业规模" prop="CompnayType" >
                                        <el-select v-model="form.EmployeeNum" placeholder="请选择企业规模"  style="width:100%;">
                                            <el-option v-for="item in employeeNum"
                                                        :key="item.ID"
                                                        :label="item.Name"
                                                        :value="item.ID">
                                            </el-option>
                                        </el-select>
                                    </el-form-item>
                                </div>
                            </div>
                            <div class="row margin-bottom-10">
                                <div class="col col-12 ">
                                    <el-form-item label="公司简介">
                                        <el-input
                                            type="textarea"
                                            :autosize="{ minRows: 6, maxRows: 10}"
                                            resize="none"
                                            placeholder="请输入内容"
                                            v-model="form.Summary"
                                            ></el-input>
                                    </el-form-item>
                                </div>
                            </div>
                            <div class="row margin-bottom-10">
                                <div class="col col-12 ">
                                    <el-form-item label="补充说明">
                                        <el-input
                                            type="textarea"
                                            :autosize="{ minRows: 6, maxRows: 10}"
                                            resize="none"
                                            placeholder="请输入内容"
                                            v-model="form.Introduce"
                                            ></el-input>
                                    </el-form-item>
                                </div>
                            </div>
                   
                        </el-form>
                    </div>
                </div>
        
            </div>
            <span slot="footer" class="dialog-footer">
                <el-button @click="editCompanyDialog = false">取 消</el-button>
                <el-button type="primary" @click="submitForm('form')">确 定</el-button>
            </span>
        </el-dialog>
    `,
    watch: {
        option(newVal, oldVal) {
            this.form = newVal
            if (Object.keys(newVal).length > 0 && newVal.PKID) {
                this.isEdit = true
                this.GetJobCompany()
            }
        },
        visible(newVal, oldVal) {
            this.editCompanyDialog = newVal
        },
        editCompanyDialog(newVal, oldVal) {
            if (!newVal) this.$emit("hide", { value: newVal });
        }
    },
    mounted: function () {
    },
    methods: {

        GetJobCompany() {
            axiosGet(api.delivery + "GetAgentCompanyByID?PKID=" + this.form.PKID)
                .then((res) => {
                    if (res.code == 1) {
                        this.form = res.data
                        if (res.data.CompnayType == 0) this.form.CompnayType = ''
                        if (res.data.EmployeeNum == 0) this.form.EmployeeNum = ''
                    }
                })
        },
        submitForm(formName) {
            this.$refs[formName].validate((valid) => {
                if (valid) {
                    // true
                    axiosPost(api.delivery + "SaveAgentCompany", this.form)
                        .then((res) => {
                            this.$emit("result", { res: res });

                        })
                } else {
                    // false
                }
            });
        }

    }
})
