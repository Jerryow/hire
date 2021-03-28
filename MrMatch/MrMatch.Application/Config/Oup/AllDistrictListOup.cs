using MrMatch.Domain.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.Config.Oup
{
    public class AllDistrictListOup : EntityDto
    {
        /// <summary>
        /// 地区名称
        /// </summary>
        public string DistrictName { get; set; }
    }
}
