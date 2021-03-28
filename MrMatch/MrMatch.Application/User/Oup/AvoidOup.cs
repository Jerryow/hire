using MrMatch.Domain.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.User.Oup
{
    public class AvoidOup : EntityDto
    {
        /// <summary>
        /// 屏蔽域名
        /// </summary>
        public string AvoidDomain { get; set; }
    }
}
