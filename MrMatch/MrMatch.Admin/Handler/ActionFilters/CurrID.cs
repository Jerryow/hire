using System;
using MrMatch.Common.Encrypt;
using MrMatch.Common.Redis;
using MrMatch.Domain.Models;

namespace MrMatch.Admin.Handler.ActionFilters
{
    public class CurrID
    {
        public static long PKID
        {
            get
            {
                try
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
                catch (Exception ex)
                {

                    return 0;
                }
            }

        }

        public static string LoginName
        {
            get
            {
                try
                {
                    var cookie = Handler.CookiesManager.GetCookie("admin_user");
                    if (string.IsNullOrEmpty(cookie))
                    {
                        return "";
                    }

                    string strTicket = Encryption.DecryptString(cookie);
                    var user = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenModel>(strTicket);

                    if (user == null || user.PKID <= 0)
                    {
                        return "";
                    }
                    return user.LoginName;
                }
                catch (Exception ex)
                {

                    return "";
                }
            }

        }

        public static string Tickets
        {
            get
            {
                var ticket = Handler.CookiesManager.GetCookie("admin_user");
                return ticket;
            }
        }
    }
}