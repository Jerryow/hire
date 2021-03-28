using MrMatch.Application.LoginOrRegist.Inp;
using MrMatch.Application.LoginOrRegist.Oup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.LoginOrRegist
{
    public interface ISignInOrUpService
    {
        #region backend

        /// <summary>
        /// 根据表达式获取用户
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        Task<SignInOup> AdminSignIn(AdminLoginInp input);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="oldPwd"></param>
        /// <param name="newPwd"></param>
        /// <returns></returns>
        Task<BoolMessageOup> AdminUpdatePwd(string loginName, string oldPwd, string newPwd);

        #endregion

        #region candidate
        /// <summary>
        /// 扫码登陆
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SignInOup> LoginQR(QRLoginInp input);

        /// <summary>
        /// 手机验证登陆
        /// </summary>
        /// <param name="input"></param>
        /// <param name="type">CommonEnum.TP_Candidate_Client</param>
        /// <returns></returns>
        Task<SignInOup> LoginMobile(MobileLoginInp input, int type);
        #endregion

        #region biz
        /// <summary>
        /// 手机验证登陆
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<BizSignInOup> BizLoginMobile(BizLoginInp input);

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<BizSignInOup> BizRegist(BizRegistInp input);
        #endregion
    }
}
