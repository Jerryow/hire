using MrMatch.Domain.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.User.Inp
{
    public class AddOrUpdateUserTagsInp
    {
        /// <summary>
        /// 候选人ID
        /// </summary>
        public long UserID { get; set; }
        /// <summary>
        /// 标签ids,用','隔开
        /// </summary>
        public string Tags { get; set; }
    }
}
