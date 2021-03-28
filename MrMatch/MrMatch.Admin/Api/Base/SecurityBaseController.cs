using MrMatch.Application.System;
using MrMatch.Application.System.Oup;
using MrMatch.Common.Encrypt;
using MrMatch.Common.Redis;
using MrMatch.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MrMatch.Admin.Api.Base
{
    [ApiFilters]
    [BasicAuthorize]
    public class SecurityBaseController : ApiControllerBase
    {
        protected long CurrID
        {
            get
            {
                var cookie = Handler.CookiesManager.GetCookie("admin_user");
                if (string.IsNullOrEmpty(cookie))
                {
                    return 0;
                }
                string strTicket = Encryption.DecryptString(cookie);
                var user = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenModel>(strTicket);

                if (user == null || user.PKID <= 0)
                {
                    return 0;
                }
                return user.PKID;
            }
        }
    }
}