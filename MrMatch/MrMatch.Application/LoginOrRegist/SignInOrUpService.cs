using MrMatch.Application.LoginOrRegist.Inp;
using MrMatch.Application.LoginOrRegist.Oup;
using MrMatch.Common.Encrypt;
using MrMatch.Common.Redis;
using MrMatch.Common.Redis.MyHelper;
using MrMatch.Domain.EntityBase.Repository;
using MrMatch.Domain.Models;
using MrMatch.MysqlFramework;
using MrMatch.MysqlFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MrMatch.Application.Cache;

namespace MrMatch.Application.LoginOrRegist
{
    public class SignInOrUpService : ISignInOrUpService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<TP_SystemUser> sysUser;
        private readonly IRepository<TP_Account> account;
        private readonly ICommunityRepository<mq_user> user;
        public SignInOrUpService(
            IUnitOfWork _unitOfWork,
            IRepository<TP_SystemUser> _sysUser,
            ICommunityRepository<mq_user> _user,
            IRepository<TP_Account> _account)
        {
            unitOfWork = _unitOfWork;
            sysUser = _sysUser;
            user = _user;
            account = _account;
        }

        #region backend
        public async Task<SignInOup> AdminSignIn(AdminLoginInp input)
        {
            var oup = new SignInOup();

            if (!input.CheckNRE())
            {
                oup.IsOK = false;
                oup.Msg = "登陆信息不全";
                oup.ReturnUrl = "";
                return oup;
            }
            oup.IsOK = true;
            oup.Msg = "登陆成功";
            oup.ReturnUrl = input.ReturnUrl;


            var user = await sysUser.FirstOrDefaultAsync(x => x.LoginName == input.LoginName && x.Valid == true);

            if (!user.IsNullEntity())
            {
                oup.IsOK = false;
                oup.Msg = "此用户未注册";
                oup.ReturnUrl = "";
                return oup;
            }

            if (user.Password != input.EncryptPwd())
            {
                oup.IsOK = false;
                oup.Msg = "登录名或密码错误";
                oup.ReturnUrl = "";
                return oup;
            }

            if (!user.OnActive)
            {
                oup.IsOK = false;
                oup.Msg = "此账户被停用";
                oup.ReturnUrl = "";
                return oup;
            }

            //生成token并存入redis
            var tokenModel = new TokenModel();
            tokenModel.ExpiredTime = DateTime.Now.AddHours(6);
            tokenModel.PKID = user.PKID;
            tokenModel.LoginName = user.LoginName;
            var tokenStr = Newtonsoft.Json.JsonConvert.SerializeObject(tokenModel);
            var token = Encryption.EncryptString(tokenStr);
            oup.Cookie = token;
            var client = ConfigurationManager.AppSettings["ProductionOrNot"];

            //SetCacheHelper.SetAdminLoginToken(user.PKID.ToString(), token, client);
            return oup;
        }

        public async Task<BoolMessageOup> AdminUpdatePwd(string loginName, string oldPwd, string newPwd)
        {
            var oup = new BoolMessageOup();
            oup.BoolResult = true;
            oup.Message = "修改成功";
            if (string.IsNullOrEmpty(loginName) || string.IsNullOrEmpty(oldPwd) || string.IsNullOrEmpty(newPwd))
            {
                oup.BoolResult = false;
                oup.Message = "信息不全";
                return oup;
            }

            var user = await sysUser.FirstOrDefaultAsync(x => x.LoginName == loginName && x.Valid == true);
            if (!user.IsNullEntity())
            {
                oup.BoolResult = false;
                oup.Message = "密码错误";
                return oup;
            }

            var pwd1 = loginName + "-" + oldPwd;
            var encryPwd = Encryption.EncryptString(pwd1);

            if (user.Password != encryPwd)
            {
                oup.BoolResult = false;
                oup.Message = "密码错误";
                return oup;
            }

            var newEncrypt = Encryption.EncryptString(loginName + "-" + newPwd);
            user.Password = newEncrypt;
            user.LastModifiedTime = DateTime.Now;
            unitOfWork.RegisterUpdate(user);
            var rtn = await unitOfWork.CommitAsync();
            if (!rtn)
            {
                oup.BoolResult = false;
                oup.Message = "修改密码失败,请重试";
                return oup;
            }
            return oup;
        }
        #endregion

        #region candidate
        public async Task<SignInOup> LoginQR(QRLoginInp input)
        {
            var oup = new SignInOup();

            if (input.PKID <= 0)
            {
                oup.IsOK = false;
                oup.Msg = "登陆信息不全";
                return oup;
            }
            oup.IsOK = true;
            oup.Msg = "登陆成功";
            oup.ReturnUrl = "/resume/index";


            var userInfo = await user.FirstOrDefaultAsync(x => x.id == input.PKID);

            if (!userInfo.IsNullEntity())
            {
                oup.IsOK = false;
                oup.Msg = "此用户未注册";
                return oup;
            }


            //生成token并存入redis
            var tokenModel = new TokenModel();
            tokenModel.ExpiredTime = DateTime.Now.AddHours(6);
            tokenModel.PKID = userInfo.id;
            tokenModel.LoginName = userInfo.phone;
            var tokenStr = Newtonsoft.Json.JsonConvert.SerializeObject(tokenModel);
            var token = Encryption.EncryptString(tokenStr);
            oup.Cookie = token;
            var client = ConfigurationManager.AppSettings["ProductionOrNot"];
            SetCacheHelper.SetCandidateLoginToken(userInfo.id.ToString(), token, client, false);
            oup.PKID = userInfo.id;
            oup.NickName = userInfo.nick_name;
            oup.AvatarUrl = userInfo.img;

            return oup;
        }

        public async Task<SignInOup> LoginMobile(MobileLoginInp input, int type)
        {
            var oup = new SignInOup();

            oup.IsOK = true;
            oup.Msg = "登陆成功";
            oup.ReturnUrl = "/resume/index";


            //判断是测试环境还是生产环境
            var client = ConfigurationManager.AppSettings["ProductionOrNot"];

            var now = DateTime.Now;

            //查看是登陆/注册
            var userInfo = await user.FirstOrDefaultAsync(x => x.phone == input.CellPhone);
            //查看是否免验证登陆
            if (!userInfo.IsNullEntity())
            {
                return await RegistMobile(input, type);
            }

            //测试环境不用校验验证码
            if (client.Trim().ToString() == "true")
            {
                //是否免验证登陆
                if (userInfo.IsVerify)
                {
                    var userCheck = GetCacheHelper.GetVerifyCode(input.CellPhone, CommonEnum.TP_Notice_SendClient.C端.GetHashCode());

                    if (string.IsNullOrEmpty(userCheck) || userCheck != input.VerifyCode)
                    {
                        oup.IsOK = false;
                        oup.Msg = "验证码错误";
                        return oup;
                    }
                    ClearCacheHelper.ClearVerifyCode(input.CellPhone, CommonEnum.TP_Notice_SendClient.C端.GetHashCode());
                }
            }

            oup.PKID = userInfo.id;
            oup.NickName = userInfo.nick_name;
            oup.AvatarUrl = userInfo.img;
            //生成token并存入redis
            var tokenModel = new TokenModel();
            tokenModel.ExpiredTime = DateTime.Now.AddHours(6);
            tokenModel.PKID = userInfo.id;
            tokenModel.LoginName = userInfo.phone;
            var tokenStr = Newtonsoft.Json.JsonConvert.SerializeObject(tokenModel);
            var token = Encryption.EncryptString(tokenStr);
            oup.Cookie = token;
            if (type == CommonEnum.TP_Candidate_Client.小程序.GetHashCode())
            {
                SetCacheHelper.SetCandidateApiLoginToken(oup.PKID.ToString(), token, client, input.AutoLogin);
            }
            else
            {
                SetCacheHelper.SetCandidateLoginToken(oup.PKID.ToString(), token, client, input.AutoLogin);
            }

            return oup;
        }

        public async Task<SignInOup> RegistMobile(MobileLoginInp input, int type)
        {
            var oup = new SignInOup();
            oup.IsOK = true;
            oup.Msg = "注册成功";
            var client = ConfigurationManager.AppSettings["ProductionOrNot"];
            //测试环境不用校验验证码
            if (client.Trim().ToString() == "true")
            {
                //是否免验证登陆
                var userCheck = GetCacheHelper.GetVerifyCode(input.CellPhone, CommonEnum.TP_Notice_SendClient.C端.GetHashCode());

                if (string.IsNullOrEmpty(userCheck) || userCheck != input.VerifyCode)
                {
                    oup.IsOK = false;
                    oup.Msg = "验证码错误";
                    return oup;
                }
                ClearCacheHelper.ClearVerifyCode(input.CellPhone, CommonEnum.TP_Notice_SendClient.C端.GetHashCode());

            }

            var now = DateTime.Now;

            mq_user newUser = new mq_user();
            newUser.phone = input.CellPhone;
            newUser.country_code = input.CountryCode;
            newUser.token = Guid.NewGuid().ToString().Replace("-", "");
            newUser.nick_name = Common.Tools.UniversalHelper.GetNickName();
            newUser.img = "https://oss.mrmatch.net/static/useravatar/candidate_accountAvatar.png";
            newUser.user_type = 0;
            newUser.is_delete = "N";
            newUser.create_time = DateTime.Now;
            newUser.update_time = DateTime.Now;
            newUser.is_real = "N";
            newUser.status = 0;
            newUser.ApproveStatus = CommonEnum.TP_Profile_ApproveStatus.未审核.GetHashCode();
            newUser.ActiveStatus = false;
            newUser.IntentionFunctionIDs = "";
            newUser.AnnualSalary = "";
            newUser.LocationIDs = "";
            newUser = unitOfWork.RegisterNewEntity(newUser);
            await unitOfWork.CommitAsync();
            if (newUser.id <= 0)
            {
                oup.IsOK = false;
                oup.Msg = "注册失败,请重试";
                return oup;
            }
            oup.PKID = newUser.id;
            oup.NickName = newUser.nick_name;
            oup.AvatarUrl = newUser.img;
            //生成token并存入redis
            var tokenModel = new TokenModel();
            tokenModel.ExpiredTime = DateTime.Now.AddHours(6);
            tokenModel.PKID = newUser.id;
            tokenModel.LoginName = newUser.phone;
            var tokenStr = Newtonsoft.Json.JsonConvert.SerializeObject(tokenModel);
            var token = Encryption.EncryptString(tokenStr);
            oup.Cookie = token;
            oup.ReturnUrl = "/resume/index";
            if (type == CommonEnum.TP_Candidate_Client.小程序.GetHashCode())
            {
                SetCacheHelper.SetCandidateApiLoginToken(oup.PKID.ToString(), token, client, false);
            }
            else
            {
                SetCacheHelper.SetCandidateLoginToken(oup.PKID.ToString(), token, client, false);
            }

            return oup;
        }
        #endregion

        #region biz
        public async Task<BizSignInOup> BizLoginMobile(BizLoginInp input)
        {
            var oup = new BizSignInOup();
            oup.IsOK = true;
            oup.Msg = "登陆成功";
            oup.ReturnUrl = "/account/index";


            //判断是测试环境还是生产环境
            var client = ConfigurationManager.AppSettings["ProductionOrNot"];

            var now = DateTime.Now;

            //查看是登陆/注册
            var accountInfo = await account.FirstOrDefaultAsync(x => x.CellPhone == input.CellPhone);
            if (!accountInfo.IsNullEntity())
            {
                oup.IsOK = false;
                oup.Msg = "此用户未注册";
                oup.ReturnUrl = "";
                return oup;
            }

            //测试环境不用校验验证码
            if (client.Trim().ToString() == "true")
            {
                //是否免验证登陆
                if (accountInfo.IsVerify)
                {
                    var userCheck = GetCacheHelper.GetVerifyCode(input.CellPhone, CommonEnum.TP_Notice_SendClient.B端.GetHashCode());

                    if (string.IsNullOrEmpty(userCheck) || userCheck != input.VerifyCode)
                    {
                        oup.IsOK = false;
                        oup.Msg = "验证码错误";
                        oup.ReturnUrl = "";
                        return oup;
                    }

                    ClearCacheHelper.ClearVerifyCode(input.CellPhone, CommonEnum.TP_Notice_SendClient.B端.GetHashCode());
                }
            }


            //生成token并存入redis
            var tokenModel = new TokenModel();
            tokenModel.ExpiredTime = DateTime.Now.AddHours(6);
            tokenModel.PKID = accountInfo.PKID;
            tokenModel.LoginName = accountInfo.CellPhone;
            var tokenStr = Newtonsoft.Json.JsonConvert.SerializeObject(tokenModel);
            var token = Encryption.EncryptString(tokenStr);
            oup.Cookie = token;
            SetCacheHelper.SetBizLoginToken(accountInfo.PKID.ToString(), token, client, input.AutoLogin);

            return oup;
        }

        public async Task<BizSignInOup> BizRegist(BizRegistInp input)
        {
            var oup = new BizSignInOup();
            var now = DateTime.Now;
            #region 排重
            var existPhone = await account.FirstOrDefaultAsync(x => x.CellPhone == input.CellPhone && x.Valid == true);
            if (existPhone.IsNullEntity())
            {
                oup.IsOK = false;
                oup.Msg = "此手机号已注册,请直接登陆或重新输入";
                return oup;
            }
            var existEmail = await account.FirstOrDefaultAsync(x => x.Email == input.Email && x.Valid == true);
            if (existEmail.IsNullEntity())
            {
                oup.IsOK = false;
                oup.Msg = "此邮箱已绑定注册,请更换";
                return oup;
            }
            #endregion

            var client = ConfigurationManager.AppSettings["ProductionOrNot"];
            #region 验证码
            if (client.Trim() == "true")
            {
                var emailCode = Cache.GetCacheHelper.GetBizEmailCode(input.Email);
                var phoneCode = Cache.GetCacheHelper.GetVerifyCode(input.CellPhone, 1);
                if (string.IsNullOrEmpty(emailCode))
                {
                    oup.IsOK = false;
                    oup.Msg = "邮箱验证码失效,请重新获取";
                    return oup;
                }
                if (string.IsNullOrEmpty(phoneCode))
                {
                    oup.IsOK = false;
                    oup.Msg = "手机验证码失效,请重新获取";
                    return oup;
                }
                if (emailCode != input.EmailVerifyCode)
                {
                    oup.IsOK = false;
                    oup.Msg = "邮箱验证码错误,请重新输入";
                    return oup;
                }
                if (phoneCode != input.PhoneVerifyCode)
                {
                    oup.IsOK = false;
                    oup.Msg = "手机验证码错误,请重新输入";
                    return oup;
                }
            }
            #endregion

            //判断是测试环境还是生产环境
            ClearCacheHelper.ClearVerifyCode(input.CellPhone, 1);
            ClearCacheHelper.ClearBizEmailVerifyCode(input.Email);
            //注册基本信息
            TP_Account newData = new TP_Account();
            newData.CompanyID = 0;
            newData.Email = input.Email;
            newData.CellPhone = input.CellPhone;
            newData.Domain = input.Email.Split('@')[1];
            newData.AvatarUrl = "https://oss.mrmatch.net/static/useravatar/biz_accountAvatar.png";
            newData.IsAdmin = true;
            newData.OnActive = true;
            newData.IsVerify = true;
            newData.DraftLimiteCount = 5;
            newData.TemplateLimiteCount = 10;
            newData.CreatedTime = now;
            newData.LastModifiedTime = now;
            newData.LastLoginTime = now;
            newData.Valid = true;
            newData = unitOfWork.RegisterNewEntity(newData);
            oup.IsOK = await unitOfWork.CommitAsync();
            if (!oup.IsOK)
            {
                oup.Msg = "注册失败";
            }
            oup.Msg = "注册成功";
            //生成token并存入redis
            var tokenModel = new TokenModel();
            tokenModel.ExpiredTime = DateTime.Now.AddHours(6);
            tokenModel.PKID = newData.PKID;
            tokenModel.LoginName = input.CellPhone;
            var tokenStr = Newtonsoft.Json.JsonConvert.SerializeObject(tokenModel);
            var token = Encryption.EncryptString(tokenStr);
            oup.Cookie = token;
            SetCacheHelper.SetBizLoginToken(newData.PKID.ToString(), token, client, false);
            oup.ReturnUrl = "/account/index";
            return oup;
        }
        #endregion
    }
}
