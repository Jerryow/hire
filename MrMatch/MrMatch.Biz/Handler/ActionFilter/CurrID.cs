using System;
using MrMatch.Common.Encrypt;
using MrMatch.Common.Redis;
using MrMatch.Domain.Models;
using System.Linq;

namespace MrMatch.Biz.Handler.ActionFilter
{
    public class CurrID
    {
        public static TP_Account CurrentUser
        {
            get
            {
                try
                {
                    var cookie = CookiesManager.GetCookie("biz_user");
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

        public static string Tickets
        {
            get
            {
                var ticket = Handler.CookiesManager.GetCookie("biz_user");
                return ticket;
            }
        }
    }
}