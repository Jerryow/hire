﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Common.Wechat
{
    public class WeChatToken
    {
        public string access_token { get; set; }

        public int expires_in { get; set; }

        public string errmsg { get; set; }

        public int errcode { get; set; }
    }
}
