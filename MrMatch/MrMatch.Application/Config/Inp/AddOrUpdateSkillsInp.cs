using MrMatch.Domain.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.Config.Inp
{
    public class AddOrUpdateSkillsInp : EntityDto
    {
        /// <summary>
        /// 技能名称
        /// </summary>
        public string SkillName { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool OnEnabled { get; set; }
    }
}
