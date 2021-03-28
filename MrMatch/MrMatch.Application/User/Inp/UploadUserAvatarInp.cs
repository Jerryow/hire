using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.User.Inp
{
    public class UploadUserAvatarInp
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public long UserID { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public byte[] AvatarBytes { get; set; }
    }
}
