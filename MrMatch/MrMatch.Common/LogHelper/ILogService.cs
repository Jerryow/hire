using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Common.LogHelper
{
    public interface ILogService
    {
        #region info
        void LogError(string throwMsg, Exception ex);
        #endregion

        #region error
        void LogInfo(string throwMsg);
        #endregion

        #region debug
        void LogDebug(string throwMsg);
        #endregion

        #region warning
        void LogWarning(string throwMsg);
        #endregion
    }
}
