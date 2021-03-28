using MrMatch.Domain.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MrMatch.Common.AttributeHelper;

namespace MrMatch.Application.User.Inp
{
    public class AddOrUpdateUserJobBoardInp
    {
        [Validate("long", "用户标识不能为空")]
        public long UserID { get; set; }
        /// <summary>
        /// 字段名  1：意向职能ID 2：意向城市ID 3：意向年薪
        /// </summary>
        [Validate("int", "请传入修改项")]
        public int WhichProperty { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string PropertyValue { get; set; }
    }
}
