using MrMatch.Domain.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.Config.Oup
{
    public class AllParentTagsListOup : EntityDto
    {
        /// <summary>
        /// 父标签名称
        /// </summary>
        public string Tags { get; set; }
    }
}
