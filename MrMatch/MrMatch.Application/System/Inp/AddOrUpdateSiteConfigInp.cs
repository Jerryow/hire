using MrMatch.Domain.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.System.Inp
{
    public class AddOrUpdateSiteConfigInp :EntityDto
    {
        /// <summary>
        /// 配置名称
        /// </summary>
        public string ConfigName { get; set; }
        /// <summary>
        /// 配置编码
        /// </summary>
        public string ConfigCode { get; set; }
        /// <summary>
        /// 配置值
        /// </summary>
        public string ConfigValue { get; set; }
    }
}
