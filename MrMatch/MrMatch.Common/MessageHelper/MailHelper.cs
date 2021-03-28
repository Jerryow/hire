using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MrMatch.Common.MessageHelper
{
    public class MailHelper
    {
        /// <summary>
        /// 发送者
        /// </summary>
        public string mailFrom { get; set; }
        /// <summary>
        /// 显示名字
        /// </summary>
        public string displayName { get; set; }

        /// <summary>
        /// 收件人
        /// </summary>
        public string[] mailToArray { get; set; }
        /// <summary>
        /// 抄送
        /// </summary>
        public string[] mailCcArray { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string mailSubject { get; set; }
        /// <summary>
        /// 正文
        /// </summary>
        public string mailBody { get; set; }
        /// <summary>
        /// 发件人密码
        /// </summary>
        public string mailPwd { get; set; }
        /// <summary>
        /// SMTP邮件服务器
        /// </summary>
        public string host { get; set; }
        /// <summary>
        /// 发送端口
        /// </summary>
        public int port { get; set; }
        /// <summary>
        /// 是否SSL发送
        /// </summary>
        public bool enableSsl { get; set; }

        /// <summary>
        /// 正文是否是html格式
        /// </summary>
        public bool isbodyHtml { get; set; }
        /// <summary>
        /// 附件
        /// </summary>
        public string[] attachmentsPath { get; set; }
        public bool SendByMailMessage()
        {
            //使用指定的邮件地址初始化MailAddress实例
            System.Net.Mail.MailAddress maddr = new System.Net.Mail.MailAddress(mailFrom, displayName);
            //初始化MailMessage实例
            System.Net.Mail.MailMessage myMail = new System.Net.Mail.MailMessage();
            //向收件人地址集合添加邮件地址
            if (mailToArray != null)
            {
                for (int i = 0; i < mailToArray.Length; i++)
                {
                    myMail.To.Add(mailToArray[i].ToString());
                }
            }
            //向抄送收件人地址集合添加邮件地址
            if (mailCcArray != null)
            {
                for (int i = 0; i < mailCcArray.Length; i++)
                {
                    myMail.CC.Add(mailCcArray[i].ToString());
                }
            }
            //发件人地址
            myMail.From = maddr;
            //电子邮件的标题
            myMail.Subject = mailSubject;
            //电子邮件的主题内容使用的编码
            myMail.SubjectEncoding = Encoding.UTF8;
            //电子邮件正文
            myMail.Body = mailBody;
            //电子邮件正文的编码
            myMail.BodyEncoding = Encoding.Default;
            myMail.Priority = System.Net.Mail.MailPriority.High;
            myMail.IsBodyHtml = isbodyHtml;
            //在有附件的情况下添加附件
            try
            {
                if (attachmentsPath != null && attachmentsPath.Length > 0)
                {
                    System.Net.Mail.Attachment attachFile = null;
                    foreach (string path in attachmentsPath)
                    {
                        attachFile = new System.Net.Mail.Attachment(path);
                        myMail.Attachments.Add(attachFile);
                    }
                }
            }
            catch (Exception err)
            {
                throw new Exception("在添加附件时有错误:" + err);
            }

            //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();

            //指定发件人的邮件地址和密码以验证发件人身份
            smtp.Credentials = new System.Net.NetworkCredential(mailFrom, mailPwd);
            //设置SMTP邮件服务器
            smtp.Host = host;
            smtp.Port = port;
            smtp.EnableSsl = enableSsl;
            try
            {
                //将邮件发送到SMTP邮件服务器
                smtp.Send(myMail);
                return true;
            }
            catch (System.Net.Mail.SmtpException)
            {
                return false;
            }
        }

        public bool ValidateEmail(String EmailString)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?){1}";
            Regex regex = new Regex(strRegex, RegexOptions.IgnoreCase);
            if (regex.Match(EmailString).Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    //调用
    /*
        Email email = new Email();
        email.mailFrom = "";//发送人的邮箱地址
        email.mailPwd = "";//发送人邮箱的密码
        email.mailSubject = "";//邮件主题
        email.mailBody = "";//邮件内容
        email.isbodyHtml = true;    //是否是HTML
        email.host = "smtp.163.com";//如果是QQ邮箱则：smtp:qq.com,依次类推
        email.mailToArray = new string[] { "3***9030@qq.com" };//接收者邮件集合
        //email.mailCcArray = new string[] { "******@qq.com" };//抄送者邮件集合
        email.Send().ToString();
    */
}
