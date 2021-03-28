using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Common.Wechat
{
    public static class WechatHelper
    {
        /// <summary>
        /// 获取微信的token
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        public static WeChatToken GetWechatAccessToken(string appid, string secret)
        { 
            var tokenUrl = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + appid + "&secret=" + secret + "";
            var result = Common.Tools.HttpHelper.HttpGetRequest(tokenUrl);

            return Newtonsoft.Json.JsonConvert.DeserializeObject<WeChatToken>(result);
        }

        /// <summary>
        /// 获取小程序二维码
        /// </summary>
        /// <param name="pic"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static WechatPicResponse GetMiniPic(MiniWeChatPic pic, string accessToken)
        {
            var oup = new WechatPicResponse();
            var url = string.Format("https://api.weixin.qq.com/wxa/getwxacodeunlimit?access_token={0}", accessToken);
            var postDataStr = Newtonsoft.Json.JsonConvert.SerializeObject(pic);

            var res = Common.Tools.HttpHelper.HttpPostRequest(url, postDataStr, "application/json");

            var data = res.GetResponseStream();

            var buffer = Common.Tools.HttpHelper.StreamToBytes(data);

            string retString = Common.Tools.HttpHelper.StreamReaderToString(buffer);
            data.Close();

            if (retString.ToLower().Contains("errcode"))
            {
                oup.IsOK = false;
                return oup;
            }

            oup.IsOK = true;
            oup.Buffer = buffer;

            return oup;
        }
    }
}
