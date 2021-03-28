using MrMatch.Domain.EntityBase.Repository;
using MrMatch.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MrMatch.MysqlFramework.Extensions;
using MrMatch.Common.Wechat;
using MrMatch.Application.Wechat.Oup;

namespace MrMatch.Application.Wechat
{
    public class WechatService : IWechatService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<TP_AccessToken> accessToken;

        public WechatService(
            IUnitOfWork _unitOfWork,
            IRepository<TP_AccessToken> _accessToken)
        {
            unitOfWork = _unitOfWork;
            accessToken = _accessToken;
        }

        public async Task<WechatTokenOup> GetAccessToken(string appID, string secret)
        {
            var oup = new WechatTokenOup();
            var now = DateTime.Now;
            var existTokens = await accessToken.FirstOrDefaultAsync(
                x => x.Valid == true
                && x.AppID == appID
                && x.Secret == secret
                && x.ExpiresTime > now);
            if (existTokens.IsNullEntity())
            {
                oup.Token = existTokens.AccessToken;
                oup.IsOK = true;
                return oup;
            }

            var res = WechatHelper.GetWechatAccessToken(appID, secret);
            if (res.errcode > 0)
            {
                oup.IsOK = false;
                oup.Message = res.errmsg;
                return oup;
            }
            oup.Token = res.access_token;
            oup.IsOK = true;

            TP_AccessToken tokenInfo = new TP_AccessToken();
            tokenInfo.AccessToken = res.access_token;
            tokenInfo.AppID = appID;
            tokenInfo.Secret = secret;
            tokenInfo.ExpiresIn = res.expires_in;
            tokenInfo.ExpiresTime = DateTime.Now.AddSeconds(tokenInfo.ExpiresIn.Value);
            tokenInfo.CreatedTime = now;
            tokenInfo.LastModifiedTime = now;
            tokenInfo.Valid = true;
            tokenInfo = unitOfWork.RegisterNewEntity(tokenInfo);
            await unitOfWork.CommitAsync();


            return oup;
        }
    }
}
