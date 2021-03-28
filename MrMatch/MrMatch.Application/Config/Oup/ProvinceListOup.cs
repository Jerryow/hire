using MrMatch.Domain.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.Config.Oup
{
    public class ProvinceListOup : EntityDto
    {
        /// <summary>
        /// 地区名称
        /// </summary>
        public string DistrictName { get; set; }
        public List<ChildrenDistrictListOup> Children { get; set; }
    }
}
