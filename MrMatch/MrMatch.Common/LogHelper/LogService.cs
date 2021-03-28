using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Common.LogHelper
{
    public class LogService : ILogService
    {

        public readonly log4net.ILog logInfo;
        public readonly log4net.ILog logError;
        public readonly log4net.ILog logDebug;
        public readonly log4net.ILog logWarning;
        //public static readonly log4net.ILog logSystem = log4net.LogManager.GetLogger("SystemLog");
        public LogService()
        {

            //XmlConfigurator.Configure(new FileInfo("Configs/log4net.config"));
            logInfo = LogManager.GetLogger("InfoLog");
            logError = LogManager.GetLogger("ErrorLog");
            logDebug = LogManager.GetLogger("DebugLog");
            logWarning = LogManager.GetLogger("WarningLog");
        }

        /// <summary>
        /// 日志等级
        /// </summary>
        public enum LogLevel
        {
            Error,
            Debug,
            Warning,
            Info
        }
        /// <summary>
        /// 日志类型
        /// </summary>
        public enum LogType
        {
            InfoLog,
            ErrorLog,
            DebugLog,
            WarningLog,
            SystemLog
        }



        #region error
        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="throwMsg"></param>
        /// <param name="ex"></param>
        public void LogError(string throwMsg, Exception ex)
        {
            string errorMsg = string.Format("【抛出信息】：{0} <br>【异常类型】：{1} <br>【异常信息】：{2} <br>【堆栈调用】：{3}", new object[] { throwMsg,
                ex.GetType().Name, ex.Message, ex.StackTrace });
            errorMsg = errorMsg.Replace("\r\n", "<br>");
            errorMsg = errorMsg.Replace("位置", "<strong style=\"color:red\">位置</strong>");
            logError.Error(errorMsg);
        }
        #endregion


        #region info
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="throwMsg"></param>
        /// <param name="ex"></param>
        public void LogInfo(string throwMsg)
        {
            string errorMsg = string.Format("【记录信息】：{0} <br>", throwMsg);
            logInfo.Info(errorMsg);
        }
        #endregion


        #region debug
        /// <summary>
        /// 调试日志
        /// </summary>
        /// <param name="throwMsg"></param>
        /// <param name="ex"></param>
        public void LogDebug(string throwMsg)
        {
            string errorMsg = string.Format("【调试信息记录】：{0} <br>", throwMsg);
            logDebug.Debug(errorMsg);
        }
        #endregion


        #region warning
        /// <summary>
        /// 警告日志
        /// </summary>
        /// <param name="throwMsg"></param>
        /// <param name="ex"></param>
        public void LogWarning(string throwMsg)
        {
            string errorMsg = string.Format("【警告信息记录】：{0} <br>", throwMsg);
            logWarning.Warn(errorMsg);
        }
        #endregion
    }
}
