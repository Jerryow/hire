using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Common.Redis
{
    public class TokenModel
    {
        public long PKID { get; set; }

        public string LoginName { get; set; }

        public DateTime ExpiredTime { get; set; }
    }
}
