using MrMatch.Domain.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MrMatch.Common.AttributeHelper;

namespace MrMatch.Application.Company.Inp
{
    public class AddOrUpdateLetterInp : EntityDto
    {
        /// <summary>
        /// 模板名称
        /// </summary>
        [Validate("string","模板名称不能为空")]
        public string TemplateName { get; set; }
        /// <summary>
        /// 模板内容
        /// </summary>
        [Validate("string", "模板内容不能为空")]
        public string TemplateContent { get; set; }
    }
}
