using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application
{
    public class CommonEnum
    {
        public enum TP_Notice_SendClient
        {
            B端 = 1,
            C端 = 2
        }

        public enum TP_Notice_MessageType
        {
            所有用户 = 1,
            指定用户 = 2
        }

        public enum TP_MessageConfig_ConfigType
        {
            短信 = 10,
            邮件 = 20
        }

        public enum TP_MessageTemplate_TemplateType
        {
            小程序模板 = 10,
            邮件模板 = 20
        }

        public enum TP_Profile_ApproveStatus
        {
            未审核 = 0,
            审核中 = 1,
            已审核 = 2,
            审核驳回 = 3
        }

        public enum TP_MessageTemplate
        {
            注册验证码 = 20004,
            邀请您加入 = 20003,
            简历分享 = 20005,
            注册猫奇人才市集 = 13001,
            审核通过 = 13002,
            审核驳回 = 13003
        }

        public enum TP_Company_CheckStatus
        {
            未审核 = 1,
            待审核 = 2,
            已审核 = 3
        }

        public enum TP_Candidate_Client
        {
            小程序 = 1,
            PC简历 = 2
        }

        public enum TP_JobApply_ApplyStatus
        {
            未查看 = 1,
            已查看 = 2,
            接收 = 3,
            拒绝 = 4
        }

        public enum WebsocketEnum
        {
            群发消息 = 10001,
            指定用户消息回写 = 10002,
            回写成功 = 10003,
            心跳检测 = 10004
        }
        #region 枚举自定义处理
        public static string PrintEmployeeNumToString(int val)
        {
            switch (val)
            {
                case 1:
                    return "少于15人";
                case 2:
                    return "15-50人";
                case 3:
                    return "50-150人";
                case 4:
                    return "150-500人";
                case 5:
                    return "500-1000人";
                case 6:
                    return "1000-5000人";
                case 7:
                    return "5000-10000人";
                case 8:
                    return "10000人以上";
                default:
                    return "少于15人";
            }
        }
        public static int PrintEmployeeNumToInt(string val)
        {
            switch (val)
            {
                case "少于15人":
                    return 1;
                case "15-50人":
                    return 2;
                case "50-150人":
                    return 3;
                case "150-500人":
                    return 4;
                case "500-1000人":
                    return 5;
                case "1000-5000人":
                    return 6;
                case "5000-10000人":
                    return 7;
                case "10000人以上":
                    return 8;
                default:
                    return 1;
            }
        }

        public static string PrintCompanyTypeToString(int val)
        {
            switch (val)
            {
                case 11:
                    return "中国大陆私有持股企业";
                case 12:
                    return "中国大陆国有企业";
                case 13:
                    return "非营利机构/政府机构";
                case 14:
                    return "中国港澳台地区独资企业";
                case 15:
                    return "中国和港澳台合资企业";
                case 16:
                    return "外资独资企业(非中国港澳台地区)";
                case 17:
                    return "中外合资";
                default:
                    return "中国大陆私有持股企业";
            }
        }
        public static int PrintCompanyTypeToInt(string val)
        {
            switch (val)
            {
                case "中国大陆私有持股企业":
                    return 11;
                case "中国大陆国有企业":
                    return 12;
                case "非营利机构/政府机构":
                    return 13;
                case "中国港澳台地区独资企业":
                    return 14;
                case "中国和港澳台合资企业":
                    return 15;
                case "外资独资企业(非中国港澳台地区)":
                    return 16;
                case "中外合资":
                    return 17;
                default:
                    return 11;
            }
        }

        public static string PrintFanacingStatusToString(int val)
        {
            switch (val)
            {
                case 11:
                    return "不需要融资";
                case 12:
                    return "天使轮";
                case 13:
                    return "A轮";
                case 14:
                    return "B轮";
                case 15:
                    return "C轮";
                case 16:
                    return "D轮";
                case 17:
                    return "D轮+";
                case 18:
                    return "已上市";
                default:
                    return "不需要融资";
            }
        }

        public static int PrintFanacingStatusToInt(string val)
        {
            switch (val)
            {
                case "不需要融资":
                    return 11;
                case "天使轮":
                    return 12;
                case "A轮":
                    return 13;
                case "B轮":
                    return 14;
                case "C轮":
                    return 15;
                case "D轮":
                    return 16;
                case "D轮+":
                    return 17;
                case "已上市":
                    return 18;
                default:
                    return 11;
            }
        }


        public static string PrintRegisteredCapitalToString(int val)
        {
            switch (val)
            {
                case 11:
                    return "100万以下";
                case 12:
                    return "100-500万";
                case 13:
                    return "500-1000万";
                case 14:
                    return "1000-5000万";
                case 15:
                    return "5000万-1亿";
                case 16:
                    return "1-30亿";
                case 17:
                    return "30-80亿";
                case 18:
                    return "80-300亿";
                case 19:
                    return "300亿以上";
                default:
                    return "100万以下";
            }
        }
        public static int PrintRegisteredCapitalToInt(string val)
        {
            switch (val)
            {
                case "100万以下":
                    return 11;
                case "100-500万":
                    return 12;
                case "500-1000万":
                    return 13;
                case "1000-5000万":
                    return 14;
                case "5000万-1亿":
                    return 15;
                case "1-30亿":
                    return 16;
                case "30-80亿":
                    return 17;
                case "80-300亿":
                    return 18;
                case "300亿以上":
                    return 19;
                default:
                    return 11;
            }
        }

        public static string PrintCheckStatusToString(int val)
        {
            switch (val)
            {
                case 1:
                    return "未审核";
                case 2:
                    return "待审核";
                case 3:
                    return "已审核";
                default:
                    return "未审核";
            }
        }
        public static int PrintCheckStatusToInt(string val)
        {
            switch (val)
            {
                case "未审核":
                    return 1;
                case "待审核":
                    return 2;
                case "已审核":
                    return 3;
                default:
                    return 1;
            }
        }

        public static string PrintMessageTemplateTitle(int val)
        {
            switch (val)
            {
                case 13001:
                    return "欢迎注册猫奇人才市集";
                case 13002:
                    return "恭喜！您已通过审核";
                case 13003:
                    return "抱歉！您的审核没有通过";
                default:
                    return "";
            }
        }
        #endregion

    }
}
