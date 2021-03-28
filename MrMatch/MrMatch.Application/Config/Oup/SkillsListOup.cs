using MrMatch.Domain.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.Config.Oup
{
    public class SkillsListOup : EntityDto
    {
        /// <summary>
        /// 技能名称
        /// </summary>
        public string SkillName { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool OnEnabled { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }
    }
}
