using MrMatch.Domain.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.System.Inp
{
    public class AddOrUpdateMsgTemplateInp : EntityDto
    {
        /// <summary>
        /// 模板类型 10小程序模板  20邮件模板
        /// </summary>
        public int TemplateType { get; set; }
        /// <summary>
        /// 模板编码
        /// </summary>
        public string TemplateCode { get; set; }
        /// <summary>
        /// 模板主题
        /// </summary>
        public string TemplateTitle { get; set; }
        /// <summary>
        /// 模板内容
        /// </summary>
        public string TemplateContent { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActivated { get; set; }
    }
}
