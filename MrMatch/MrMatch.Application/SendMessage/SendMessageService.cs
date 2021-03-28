using MrMatch.Application.SendMessage.Inp;
using MrMatch.Domain.EntityBase.Repository;
using MrMatch.MysqlFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MrMatch.Domain.Models;
using MrMatch.Common.ImageHelper;
using MrMatch.Common.MessageHelper;
using System.Configuration;
using MrMatch.Common.Redis;
using MrMatch.Application.Cache;
using System.Configuration;

namespace MrMatch.Application.SendMessage
{
    public class SendMessageService : ISendMessageService
    {
        private readonly IUnitOfWork unitOfWork;
        //private readonly IRepository<TP_PhoneMessageLog> phoneLog;
        //private readonly IRepository<TP_MailMessageLog> mailLog;
        private readonly IRepository<TP_MessageConfig> messageConfig;
        private readonly IRepository<TP_MessageTemplate> temp;
        public SendMessageService(
            IUnitOfWork _unitOfWork,
            //IRepository<TP_PhoneMessageLog> _phoneLog,
            //IRepository<TP_MailMessageLog> _mailLog,
            IRepository<TP_MessageConfig> _messageConfig,
            IRepository<TP_MessageTemplate> _temp)
        {
            unitOfWork = _unitOfWork;
            //phoneLog = _phoneLog;
            //mailLog = _mailLog;
            messageConfig = _messageConfig;
            temp = _temp;
        }

        public ImageOup GetVerifyImg(string phoneNumber)
        {
            return VerifyImg.Instance.GetRandImagePath("", phoneNumber);
        }

        public async Task<BoolMessageOup> SendPhoneMessage(SendPhoneInp input, string tempCode, string verifyCode, int type)
        {
            var now = DateTime.Now;
            var oup = new BoolMessageOup();
            oup.BoolResult = true;
            oup.Message = "发送成功";
            //验证获取验证码的有效性
            var rtn = VerifyImg.Instance.ValidateCaptcha(input.UserOffsetX, input.UserSign);
            if (!rtn)
            {
                oup.BoolResult = false;
                oup.Message = "图片验证失败";
                return oup;
            }

            //获取短信发送配置
            //var msgConfig = await messageConfig.FirstOrDefaultAsync(x => x.Valid == true && x.IsActivated == true && x.ConfigType == 10);
            //if (!msgConfig.IsNullEntity())
            //{
            //    oup.BoolResult = false;
            //    oup.Message = "短信配置出错,请重试.";
            //    return oup;
            //}

            //阿里云短信发送
            var aliMsg = new AliCloudMessageConfig();
            aliMsg.LoginName = ConfigurationManager.AppSettings["AccessKey"];
            aliMsg.VerifyKey = ConfigurationManager.AppSettings["AccessKeySecret"];
            aliMsg.Domain = ConfigurationManager.AppSettings["Domain"];
            aliMsg.PhoneNumber = input.PhoneNumber;
            aliMsg.SignName = ConfigurationManager.AppSettings["SignName"];
            aliMsg.TempCode = tempCode;
            aliMsg.VerifyCode = verifyCode;

            var sendRtn = AliCloudMessage.Send(aliMsg);

            if (sendRtn.Code.Trim().ToLower() != "ok")
            {
                oup.BoolResult = false;
                oup.Message = sendRtn.Message;
                return oup;
            }


            //保存短信发送日志
            //TP_PhoneMessageLog textMessage = new TP_PhoneMessageLog();
            //textMessage.PhoneType = type;
            //textMessage.PhoneNum = input.PhoneNumber;
            //textMessage.MsgContent = "验证码：" + verifyCode;
            //textMessage.State = sendRtn.Code.Trim().ToLower() == "ok" ? "成功" : "失败";
            //textMessage.SendTime = now;
            //textMessage.CreatedTime = now;
            //textMessage.LastModifiedTime = now;
            //textMessage.Valid = true;

            //unitOfWork.RegisterNew(textMessage);
            await unitOfWork.CommitAsync();

            //设置验证码缓存
            SetCacheHelper.SetVerifyCode(input.PhoneNumber, verifyCode, type);


            return oup;
        }

        public async Task<BoolMessageOup> SendEmailMessage(string email, string result)
        {
            var now = DateTime.Now;
            var oup = new BoolMessageOup();
            oup.BoolResult = true;
            oup.Message = "发送成功";

            //获取邮箱发送配置
            //var msgConfig = await messageConfig.FirstOrDefaultAsync(x => x.Valid == true && x.IsActivated == true && x.ConfigType == 20);
            //if (!msgConfig.IsNullEntity())
            //{
            //    oup.BoolResult = false;
            //    oup.Message = "短信配置出错,请重试";
            //    return oup;
            //}

            //邮箱验证码发送
            MailHelper mail = new MailHelper();
            mail.mailFrom = ConfigurationManager.AppSettings["EmailLoginName"];
            mail.displayName = ConfigurationManager.AppSettings["EmailSenderName"];
            mail.mailPwd = ConfigurationManager.AppSettings["EmailPassword"];
            mail.mailSubject = CommonEnum.TP_MessageTemplate.注册验证码.ToString();

            //先取缓存,取不到取数据库
            var body = Cache.GetCacheHelper.GetTemplate(CommonEnum.TP_MessageTemplate.注册验证码.GetHashCode().ToString());
            if (string.IsNullOrEmpty(body))
            {
                var code = CommonEnum.TP_MessageTemplate.注册验证码.GetHashCode().ToString();
                body = (await temp.FirstOrDefaultAsync(x => x.TemplateCode == code)).TemplateContent;
            }
            mail.mailBody = body;

            mail.mailBody = mail.mailBody.Replace("{{UserName}}", "新注册用户")
                .Replace("{{VerifyCode}}", result)
                .Replace("{{VerifyTime}}", DateTime.Now.AddMinutes(15).ToString("yyyy-MM-dd HH:mm:ss"));

            mail.isbodyHtml = true;    //是否是HTML
            mail.host = ConfigurationManager.AppSettings["EmailApiUrl"];// //"smtp.mxhichina.com";//如果是QQ邮箱则：smtp:qq.com,依次类推
            mail.port = Convert.ToInt32(ConfigurationManager.AppSettings["EmailPort"]);
            mail.enableSsl = true;
            mail.mailToArray = new string[] { email };//接收者邮件集合
            mail.mailCcArray = new string[] { };//抄送者邮件集合
            oup.BoolResult = mail.SendByMailMessage();

            if (!oup.BoolResult)
            {
                oup.Message = "发送失败,请稍后重试";
                return oup;
            }


            //保存短信发送日志
            //TP_MailMessageLog textMessage = new TP_MailMessageLog();
            //textMessage.MailType = 1;
            //textMessage.FromMail = ConfigurationManager.AppSettings["EmailLoginName"];
            //textMessage.ToMail = email;
            //textMessage.CCMail = "";
            //textMessage.Subject = mail.mailSubject;
            //textMessage.MailBody = mail.mailBody;
            //textMessage.State = oup.BoolResult ? "成功" : "失败";
            //textMessage.SendTime = now;
            //textMessage.CreatedTime = now;
            //textMessage.LastModifiedTime = now;
            //textMessage.Valid = true;

            //unitOfWork.RegisterNew(textMessage);
            await unitOfWork.CommitAsync();

            //设置验证码缓存
            SetCacheHelper.SetBizEmailCode(email, result);
            return oup;
        }

        public async Task<BoolMessageOup> SendInviteEmailMessage(string accountEmail, string inviteEmail, string companyName)
        {
            var now = DateTime.Now;
            var oup = new BoolMessageOup();
            oup.BoolResult = true;
            oup.Message = "发送成功";

            //邮箱验证码发送
            MailHelper mail = new MailHelper();
            mail.mailFrom = ConfigurationManager.AppSettings["EmailLoginName"];
            mail.displayName = ConfigurationManager.AppSettings["EmailSenderName"];
            mail.mailPwd = ConfigurationManager.AppSettings["EmailPassword"];
            mail.mailSubject = CommonEnum.TP_MessageTemplate.邀请您加入.ToString();

            //先取缓存,取不到取数据库
            var body = Cache.GetCacheHelper.GetTemplate(CommonEnum.TP_MessageTemplate.邀请您加入.GetHashCode().ToString());
            if (string.IsNullOrEmpty(body))
            {
                var code = CommonEnum.TP_MessageTemplate.邀请您加入.GetHashCode().ToString();
                body = (await temp.FirstOrDefaultAsync(x => x.TemplateCode == code)).TemplateContent;
            }
            mail.mailBody = body;

            mail.mailBody = mail.mailBody.Replace("{{ToUser}}", inviteEmail)
                .Replace("{{FromUser}}", accountEmail)
                .Replace("{{CompanyName}}", companyName)
                .Replace("{{VerifyTime}}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            mail.isbodyHtml = true;    //是否是HTML
            mail.host = ConfigurationManager.AppSettings["EmailApiUrl"];// //"smtp.mxhichina.com";//如果是QQ邮箱则：smtp:qq.com,依次类推
            mail.port = Convert.ToInt32(ConfigurationManager.AppSettings["EmailPort"]);
            mail.enableSsl = true;
            mail.mailToArray = new string[] { inviteEmail };//接收者邮件集合
            mail.mailCcArray = new string[] { };//抄送者邮件集合
            oup.BoolResult = mail.SendByMailMessage();

            if (!oup.BoolResult)
            {
                oup.Message = "发送失败,请稍后重试";
                return oup;
            }


            //保存短信发送日志
            //TP_MailMessageLog textMessage = new TP_MailMessageLog();
            //textMessage.MailType = 1;
            //textMessage.FromMail = accountEmail;
            //textMessage.ToMail = inviteEmail;
            //textMessage.CCMail = "";
            //textMessage.Subject = mail.mailSubject;
            //textMessage.MailBody = mail.mailBody;
            //textMessage.State = oup.BoolResult ? "成功" : "失败";
            //textMessage.SendTime = now;
            //textMessage.CreatedTime = now;
            //textMessage.LastModifiedTime = now;
            //textMessage.Valid = true;

            //unitOfWork.RegisterNew(textMessage);
            await unitOfWork.CommitAsync();

            return oup;
        }
    }
}
