using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.User.Oup
{
    public class ProfileSnapOup
    {
        #region profile
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 性别 1男 2女
        /// </summary>
        public int Gender { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birthday { get; set; }
        /// <summary>
        /// 学历
        /// </summary>
        public string Education { get; set; }
        /// <summary>
        /// 出生地
        /// </summary>
        public string BirthPlace { get; set; }
        /// <summary>
        /// 居住地
        /// </summary>
        public string Residence { get; set; }
        /// <summary>
        /// 户籍所在地
        /// </summary>
        public string CensusRegister { get; set; }
        /// <summary>
        /// 当前企业
        /// </summary>
        public string CurrentCompany { get; set; }
        /// <summary>
        /// 当前职位
        /// </summary>
        public string CurrentPosition { get; set; }
        /// <summary>
        /// 工作年限
        /// </summary>
        public int WorkYears { get; set; }
        /// <summary>
        /// 当前年薪
        /// </summary>
        public decimal CurrentAnnualSalary { get; set; }
        /// <summary>
        /// 薪资是否公开
        /// </summary>
        public bool CurrentOnOpen { get; set; }
        /// <summary>
        /// 求职状态
        /// </summary>
        public int JobStatus { get; set; }
        /// <summary>
        /// 工作介绍
        /// </summary>
        public string Introduction { get; set; }
        #endregion

        #region intention
        /// <summary>
        /// 过往资历ID 用逗号隔开
        /// </summary>
        public string FunctionIDs { get; set; }
        /// <summary>
        /// 意向城市ID 用逗号隔开
        /// </summary>
        public string LocationIDs { get; set; }
        /// <summary>
        /// 年薪
        /// </summary>
        public decimal IntentionAnnualSalary { get; set; }
        /// <summary>
        /// 意向年薪是否公开
        /// </summary>
        public bool IntentionOnOpen { get; set; }
        #endregion
    }
}
