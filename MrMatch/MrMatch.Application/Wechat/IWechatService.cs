using MrMatch.Application.Wechat.Oup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.Wechat
{
    public interface IWechatService
    {
        /// <summary>
        /// 获取微信AccessToken
        /// </summary>
        /// <param name="appID"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        Task<WechatTokenOup> GetAccessToken(string appID, string secret);
    }
}
