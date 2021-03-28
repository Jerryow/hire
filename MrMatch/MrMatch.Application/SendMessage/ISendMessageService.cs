using MrMatch.Application.SendMessage.Inp;
using MrMatch.Common.ImageHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.SendMessage
{
    public interface ISendMessageService
    {
        /// <summary>
        /// 发送手机验证码
        /// </summary>
        /// <param name="input"></param>
        /// <param name="tempCode">阿里云验证码模板编号</param>
        /// <param name="verifyCode">验证码</param>
        /// <param name="type">1:B端  2:C端</param>
        /// <returns></returns>
        Task<BoolMessageOup> SendPhoneMessage(SendPhoneInp input, string tempCode, string verifyCode, int type);

        /// <summary>
        /// 获取滑动验证码图片
        /// </summary>
        /// <returns></returns>
        ImageOup GetVerifyImg(string phoneNumber);

        /// <summary>
        /// 发送注册邮箱验证码
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="result">验证码</param>
        /// <returns></returns>
        Task<BoolMessageOup> SendEmailMessage(string email, string result);

        /// <summary>
        /// 发送企业成员邀请
        /// </summary>
        /// <param name="accountEmail"></param>
        /// <param name="inviteEmail"></param>
        /// <param name="companyName"></param>
        /// <returns></returns>
        Task<BoolMessageOup> SendInviteEmailMessage(string accountEmail, string inviteEmail, string companyName);
    }
}
