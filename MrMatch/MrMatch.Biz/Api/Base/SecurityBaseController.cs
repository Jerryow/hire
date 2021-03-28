using MrMatch.Application.System;
using MrMatch.Application.System.Oup;
using MrMatch.Common.Encrypt;
using MrMatch.Common.Redis;
using MrMatch.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MrMatch.Biz.Api.Base
{
    [ApiFilters]
    [BasicAuthorize]
    //[ExceptionF]
    public class SecurityBaseController : ApiControllerBase
    {
        protected TP_Account CurrUser
        {
            get
            {
                try
                {
                    var cookie = Handler.CookiesManager.GetCookie("biz_user");
                    if (string.IsNullOrEmpty(cookie))
                    {
                        return new TP_Account();
                    }
                    string strTicket = Encryption.DecryptString(cookie);
                    var user = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenModel>(strTicket);

                    if (user == null || user.PKID <= 0)
                    {
                        return new TP_Account();
                    }
                    using (MysqlFramework.MrMatchDbContext db = new MysqlFramework.MrMatchDbContext())
                    {
                        var current = db.TP_Account.Where(x => x.PKID == user.PKID).FirstOrDefault();
                        return current;
                    }
                }
                catch (Exception ex)
                {
                    return new TP_Account();
                }

            }
        }
    }
}