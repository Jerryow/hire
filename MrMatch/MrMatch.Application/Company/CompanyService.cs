using MrMatch.Application.Company.Inp;
using MrMatch.Application.Company.Oup;
using MrMatch.Domain.EntityBase.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MrMatch.Domain.Models;
using MrMatch.Common.Mapper;
using MrMatch.MysqlFramework.Extensions;
using MrMatch.Application.SendMessage;
using System.Configuration;
using MrMatch.Common.ImageHelper;

namespace MrMatch.Application.Company
{
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<TP_Company> company;
        private readonly IRepository<TP_Job> job;
        private readonly IRepository<TP_AgentCompany> agentCompany;
        private readonly IRepository<TP_CompanyContract> companyContract;
        private readonly IRepository<TP_LetterTemplate> letterTemplate;
        private readonly IRepository<TP_AccountInvitation> accountInvitation;
        private readonly IRepository<TP_Account> account;
        public CompanyService(
            IUnitOfWork _unitOfWork,
            IRepository<TP_Company> _company,
            IRepository<TP_Job> _job,
            IRepository<TP_AgentCompany> _agentCompany,
            IRepository<TP_CompanyContract> _companyContract,
            IRepository<TP_AccountInvitation> _accountInvitation,
            IRepository<TP_Account> _account,
            IRepository<TP_LetterTemplate> _letterTemplate)
        {
            unitOfWork = _unitOfWork;
            company = _company;
            companyContract = _companyContract;
            accountInvitation = _accountInvitation;
            account = _account;
            letterTemplate = _letterTemplate;
            agentCompany = _agentCompany;
            job = _job;
        }


        public async Task<List<InviteCompaniesOup>> GetInvitationAsync(string email)
        {
            //查询是否存在邀请数据
            var invitations = await accountInvitation.GetAllListAsync(x => x.Valid == true && x.Email == email && x.IsRegisted == false);
            if (invitations.Count <= 0)
            {
                return null;
            }
            List<long> ids = new List<long>();
            invitations.ForEach(item =>
            {
                ids.Add(item.CompanyID);
            });
            var data = (await company.GetAllListAsync(x => ids.Contains(x.PKID))).MapToList<TP_Company, InviteCompaniesOup>();
            return data;
        }

        public async Task<BoolMessageOup> RegistCompanyAsync(RegistCompanyInp input, long currentID)
        {
            var oup = new BoolMessageOup();
            var user = await account.FirstOrDefaultAsync(x => x.PKID == currentID);
            var now = DateTime.Now;

            //收到邀请并注册的用户
            if (input.CompanyID > 0)
            {
                //验证是否存在该企业的邀请
                var invites = await accountInvitation.GetAllListAsync
                    (x => x.Valid == true
                    && x.Email == user.Email
                    && x.CompanyID == input.CompanyID
                    && x.IsRegisted == false);
                if (invites.Count <= 0)
                {
                    oup.BoolResult = false;
                    oup.Message = "该用户未收到邀请";
                    return oup;
                }

                user.CompanyID = input.CompanyID;
                user.AccountName = input.AccountName;
                user.Position = input.Position;
                user.IsAdmin = false;
                user.LastLoginTime = now;
                user.TemplateLimiteCount = 10;
                user.DraftLimiteCount = 5;
                unitOfWork.RegisterUpdate(user);

                //回写邀请信
                invites.ForEach(item =>
                {
                    item.IsRegisted = true;
                    item.LastModifiedTime = now;
                    unitOfWork.RegisterUpdate(item);
                });
                oup.BoolResult = await unitOfWork.CommitAsync();
                if (!oup.BoolResult)
                {
                    oup.Message = "完善信息失败";
                    return oup;
                }
                oup.Message = "完善信息成功";
                return oup;
            }
            else
            {
                //排重
                var existCom = await company.FirstOrDefaultAsync(x => x.Valid == true && x.CompanyName == input.CompanyName);
                if (existCom.IsNullEntity())
                {
                    oup.BoolResult = false;
                    oup.Message = "公司名为:" + input.CompanyName + " 已被注册";
                    return oup;
                }

                //新建企业信息
                TP_Company newData = new TP_Company();
                newData.CompanyName = input.CompanyName;
                newData.CheckedStatus = CommonEnum.TP_Company_CheckStatus.未审核.GetHashCode();
                newData.CreatedTime = now;
                newData.LastModifiedTime = now;
                newData.Valid = true;
                newData.AgentOrNot = input.AgentOrNot;
                newData = unitOfWork.RegisterNewEntity(newData);
                oup.BoolResult = await unitOfWork.CommitAsync();

                if (!oup.BoolResult)
                {
                    oup.Message = "完善信息失败,请重试";
                    return oup;
                }

                //回写账户信息
                user.CompanyID = newData.PKID;
                user.Position = input.Position;
                user.AccountName = input.AccountName;
                user.IsAdmin = true;
                user.LastLoginTime = now;
                user.TemplateLimiteCount = 10;
                user.DraftLimiteCount = 5;
                unitOfWork.RegisterUpdate(user);
                await unitOfWork.CommitAsync();

                oup.Message = "完善信息成功";
                return oup;
            }
        }

        public async Task<CompanyOup> GetCompanyAsync(long PKID)
        {
            var data = await company.FirstOrDefaultAsync(x => x.PKID == PKID);
            if (!data.IsNullEntity())
            {
                return null;
            }
            var oup = data.MapTo<TP_Company, CompanyOup>();
            oup.CheckedStatus = CommonEnum.PrintCheckStatusToString(data.CheckedStatus);
            return oup;
        }

        public async Task<string> GetCompanyLogoAsync(long PKID)
        {
            var data = await company.FirstOrDefaultAsync(x => x.PKID == PKID);
            if (!data.IsNullEntity())
            {
                return null;
            }
            return data.LogoImageUrl;
        }

        public async Task<string> GetCompanyLicenseAsync(long PKID)
        {
            var data = await company.FirstOrDefaultAsync(x => x.PKID == PKID);
            if (!data.IsNullEntity())
            {
                return null;
            }
            return data.LicenseImageUrl;
        }

        public async Task<AccountOup> GetAccountAsync(long PKID)
        {
            var user = (await account.FirstOrDefaultAsync(x => x.PKID == PKID))
                .MapTo<TP_Account, AccountOup>();
            if (!user.IsNullEntity())
            {
                return null;
            }
            return user;
        }

        public async Task<List<AccountOup>> GetAccountListAsync(long companyID)
        {
            var data = (await account.GetAllListAsync(x => x.CompanyID == companyID))
                .MapToList<TP_Account, AccountOup>();
            if (data.Count <= 0)
            {
                return null;
            }
            return data;
        }

        public async Task<List<TP_CompanyContract>> GetContractListAsync(long companyID)
        {
            var time = DateTime.Now.AddYears(-3);
            var data = await companyContract.GetAllListAsync(x => x.Valid == true && x.CompanyID == companyID && x.StartDate >= time);
            if (data.Count <= 0)
            {
                return null;
            }
            return data;
        }

        public async Task<BoolMessageOup> UpdateCompanyInfoAsync(UpdateCompanyInp input)
        {
            var oup = new BoolMessageOup();
            var existData = await company.FirstOrDefaultAsync(x => x.PKID == input.PKID);
            if (!existData.IsNullEntity())
            {
                oup.BoolResult = false;
                oup.Message = "入参错误";
                return oup;
            }

            existData = input.MapTo<TP_Company>(existData);
            unitOfWork.RegisterUpdate(existData);
            oup.BoolResult = await unitOfWork.CommitAsync();
            if (!oup.BoolResult)
            {
                oup.Message = "修改失败,请重试";
            }
            oup.Message = "修改成功";
            return oup;
        }

        public async Task<BoolMessageOup> SendInviteAsync(TP_Account currentAccount, string email)
        {
            var oup = new BoolMessageOup();
            //排重
            var newEmail = email + "@" + currentAccount.Domain;
            var existData = await account.FirstOrDefaultAsync(x => x.Valid == true && x.Email == newEmail && x.CompanyID == currentAccount.CompanyID);
            if (existData.IsNullEntity())
            {
                oup.BoolResult = false;
                oup.Message = "当前邀请邮箱已注册";
                return oup;
            }

            //验证邀请信息
            var invite = await accountInvitation.FirstOrDefaultAsync
                (x => x.Valid == true
                && x.Email == newEmail
                && x.CompanyID == currentAccount.CompanyID
                && x.IsRegisted == false);
            if (invite.IsNullEntity())
            {
                //5分钟内不能多次邀请
                if (DateTime.Now.Subtract(invite.LastModifiedTime).Minutes > 5)
                {
                    invite.SenderID = currentAccount.PKID;
                    invite.LastModifiedTime = DateTime.Now;
                    unitOfWork.RegisterUpdate(invite);
                }
                else
                {
                    oup.BoolResult = false;
                    oup.Message = "邀请太频繁,请稍后再试";
                    return oup;
                }
            }
            else
            {
                invite = new TP_AccountInvitation();
                invite.CompanyID = currentAccount.CompanyID;
                invite.Email = newEmail;
                invite.Domain = currentAccount.Domain;
                invite.SenderID = currentAccount.PKID;
                invite.IsRegisted = false;
                invite.CreatedTime = DateTime.Now;
                invite.LastModifiedTime = DateTime.Now;
                invite.Valid = true;
                unitOfWork.RegisterNew(invite);
            }

            oup.BoolResult = await unitOfWork.CommitAsync();
            if (oup.BoolResult)
            {
                oup.Message = "邀请发送成功";
                return oup;
            }
            oup.BoolResult = false;
            oup.Message = "邀请发送失败,请再次确认邮箱账号";
            return oup;
        }

        public async Task<BoolMessageOup> AccountActivityAsync(long PKID, bool onActive)
        {
            var oup = new BoolMessageOup();
            var user = await account.FirstOrDefaultAsync(x => x.PKID == PKID);
            if (!user.IsNullEntity())
            {
                oup.BoolResult = false;
                oup.Message = "修改失败,请重试";
                return oup;
            }
            user.OnActive = onActive;
            user.LastModifiedTime = DateTime.Now;
            unitOfWork.RegisterUpdate(user);
            oup.BoolResult = await unitOfWork.CommitAsync();
            if (!oup.BoolResult)
            {
                oup.Message = "修改失败,请重试";
            }
            else
            {
                oup.Message = "修改成功";
            }
            return oup;
        }

        public async Task<BoolMessageOup> UpdateAccountAvatarAsync(long accountID, byte[] img)
        {
            var oup = new BoolMessageOup();
            var key = ConfigurationManager.AppSettings["AccessKeyId"];
            var secret = ConfigurationManager.AppSettings["AccessKeySecret"];
            var endpoint = ConfigurationManager.AppSettings["Endpoint"];
            var bucket = ConfigurationManager.AppSettings["BucketName"];
            AliyunOssHelper oss = new AliyunOssHelper(key, secret, endpoint, bucket);

            //判断是生产还是测试
            var client = ConfigurationManager.AppSettings["ProductionOrNot"];
            var fileName = "";
            if (client.Trim().ToString() == "false")
            {
                fileName = "deve/tp/AccountPersonlPicUrl/" + "avartar_" + accountID.ToString() + "_" + Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
            }
            else
            {
                fileName = "prod/AccountPersonlPicUrl/" + "avartar_" + accountID.ToString() + "_" + Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
            }


            var res = oss.PushImg(img, fileName);
            //上传成功,回写信息
            if (res.Success)
            {
                var accountInfo = await account.FirstOrDefaultAsync(x => x.PKID == accountID);
                if (!accountInfo.IsNullEntity())
                {
                    oup.BoolResult = false;
                    oup.Message = "上传失败,请重试";
                    return oup;
                }
                accountInfo.AvatarUrl = "https://oss.mrmatch.net/" + fileName;
                accountInfo.LastModifiedTime = DateTime.Now;
                unitOfWork.RegisterUpdate(accountInfo);
                oup.BoolResult = await unitOfWork.CommitAsync();
                if (!oup.BoolResult)
                {
                    oup.Message = "上传失败,请重试";
                }
                else
                {
                    oup.Message = "上传成功";
                }
                return oup;
            }
            oup.BoolResult = false;
            oup.Message = "上传失败,请重试";
            return oup;
        }

        public async Task<BoolMessageOup> UpdateAccountWechatAsync(long accountID, byte[] img)
        {
            var oup = new BoolMessageOup();
            var key = ConfigurationManager.AppSettings["AccessKeyId"];
            var secret = ConfigurationManager.AppSettings["AccessKeySecret"];
            var endpoint = ConfigurationManager.AppSettings["Endpoint"];
            var bucket = ConfigurationManager.AppSettings["BucketName"];
            AliyunOssHelper oss = new AliyunOssHelper(key, secret, endpoint, bucket);

            //判断是生产还是测试
            var client = ConfigurationManager.AppSettings["ProductionOrNot"];
            var fileName = "";
            if (client.Trim().ToString() == "false")
            {
                fileName = "deve/tp/AccountPersonlPicUrl/" + "wechat_" + accountID.ToString() + "_" + Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
            }
            else
            {
                fileName = "prod/AccountPersonlPicUrl/" + "wechat_" + accountID.ToString() + "_" + Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
            }


            var res = oss.PushImg(img, fileName);
            //上传成功,回写信息
            if (res.Success)
            {
                var accountInfo = await account.FirstOrDefaultAsync(x => x.PKID == accountID);
                if (!accountInfo.IsNullEntity())
                {
                    oup.BoolResult = false;
                    oup.Message = "上传失败,请重试";
                    return oup;
                }
                accountInfo.WechatContactUrl = "https://oss.mrmatch.net/" + fileName;
                accountInfo.LastModifiedTime = DateTime.Now;
                unitOfWork.RegisterUpdate(accountInfo);
                oup.BoolResult = await unitOfWork.CommitAsync();
                if (!oup.BoolResult)
                {
                    oup.Message = "上传失败,请重试";
                }
                else
                {
                    oup.Message = "上传成功";
                }
                return oup;
            }
            oup.BoolResult = false;
            oup.Message = "上传失败,请重试";
            return oup;
        }

        public async Task<BoolMessageOup> UpdateAccountBasicAsync(long PKID, string accountName, string position, string wechatAccount, string linkin, string focus, string introduction)
        {
            var oup = new BoolMessageOup();
            var user = await account.FirstOrDefaultAsync(x => x.PKID == PKID);
            if (!user.IsNullEntity())
            {
                oup.BoolResult = false;
                oup.Message = "修改失败,请重试";
                return oup;
            }
            user.WechatAccount = wechatAccount;
            user.LinkinUrl = linkin;
            user.FocusArea = focus;
            user.Introduction = introduction;
            user.AccountName = accountName;
            user.Position = position;
            user.LastModifiedTime = DateTime.Now;
            unitOfWork.RegisterUpdate(user);
            oup.BoolResult = await unitOfWork.CommitAsync();
            if (!oup.BoolResult)
            {
                oup.Message = "修改失败";
            }
            else
            {
                oup.Message = "修改成功";
            }
            return oup;
        }

        public async Task<AccountBasicOup> GetAccountBasicAsync(long PKID)
        {
            var user = await account.FirstOrDefaultAsync(x => x.PKID == PKID);
            if (!user.IsNullEntity())
            {
                return null;
            }
            var oup = user.MapTo<TP_Account, AccountBasicOup>();
            oup.CompanyName = (await company.FirstOrDefaultAsync(x => x.PKID == user.CompanyID)).CompanyName;
            return oup;
        }

        public async Task<BoolMessageOup> UpdateLogoAsync(long companyID, byte[] img)
        {
            var oup = new BoolMessageOup();
            var key = ConfigurationManager.AppSettings["AccessKeyId"];
            var secret = ConfigurationManager.AppSettings["AccessKeySecret"];
            var endpoint = ConfigurationManager.AppSettings["Endpoint"];
            var bucket = ConfigurationManager.AppSettings["BucketName"];
            AliyunOssHelper oss = new AliyunOssHelper(key, secret, endpoint, bucket);

            //判断是生产还是测试
            var client = ConfigurationManager.AppSettings["ProductionOrNot"];
            var fileName = "";
            if (client.Trim().ToString() == "false")
            {
                fileName = "deve/tp/CompanyLogoUrl/" + companyID.ToString() + "_" + Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
            }
            else
            {
                fileName = "prod/CompanyLogoUrl/" + companyID.ToString() + "_" + Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
            }


            var res = oss.PushImg(img, fileName);
            //上传成功,回写信息
            if (res.Success)
            {
                var companyInfo = await company.FirstOrDefaultAsync(x => x.PKID == companyID);
                if (!companyInfo.IsNullEntity())
                {
                    oup.BoolResult = false;
                    oup.Message = "上传失败,请重试";
                    return oup;
                }
                companyInfo.LogoImageUrl = "https://oss.mrmatch.net/" + fileName;
                companyInfo.LastModifiedTime = DateTime.Now;
                unitOfWork.RegisterUpdate(companyInfo);
                oup.BoolResult = await unitOfWork.CommitAsync();
                if (!oup.BoolResult)
                {
                    oup.Message = "上传失败,请重试";
                }
                else
                {
                    oup.Message = "上传成功";
                }
                return oup;
            }
            oup.BoolResult = false;
            oup.Message = "上传失败,请重试";
            return oup;
        }

        public async Task<BoolMessageOup> UpdateLisenceAsync(long companyID, byte[] img)
        {
            var oup = new BoolMessageOup();
            var key = ConfigurationManager.AppSettings["AccessKeyId"];
            var secret = ConfigurationManager.AppSettings["AccessKeySecret"];
            var endpoint = ConfigurationManager.AppSettings["Endpoint"];
            var bucket = ConfigurationManager.AppSettings["BucketName"];
            AliyunOssHelper oss = new AliyunOssHelper(key, secret, endpoint, bucket);

            //判断是生产还是测试
            var client = ConfigurationManager.AppSettings["ProductionOrNot"];
            var fileName = "";
            if (client.Trim().ToString() == "false")
            {
                fileName = "deve/tp/CompanyLisenceUrl/" + companyID.ToString() + "_" + Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
            }
            else
            {
                fileName = "prod/CompanyLisenceUrl/" + companyID.ToString() + "_" + Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
            }

            var res = oss.PushImg(img, fileName);
            //上传成功,回写信息
            if (res.Success)
            {
                var companyInfo = await company.FirstOrDefaultAsync(x => x.PKID == companyID);
                if (!companyInfo.IsNullEntity())
                {
                    oup.BoolResult = false;
                    oup.Message = "上传失败,请重试";
                    return oup;
                }
                companyInfo.LicenseImageUrl = "https://oss.mrmatch.net/" + fileName;
                companyInfo.LastModifiedTime = DateTime.Now;
                unitOfWork.RegisterUpdate(companyInfo);
                oup.BoolResult = await unitOfWork.CommitAsync();
                if (!oup.BoolResult)
                {
                    oup.Message = "上传失败,请重试";
                }
                else
                {
                    oup.Message = "上传成功";
                }
                return oup;
            }
            oup.BoolResult = false;
            oup.Message = "上传失败,请重试";
            return oup;
        }

        public async Task<List<LetterOup>> GetLetterListAsync(long accountID)
        {
            var oup = await letterTemplate.GetAllListAsync(x => x.Valid == true && x.AccountID == accountID);
            if (oup.Count <= 0)
            {
                return null;
            }
            return oup.MapToList<TP_LetterTemplate, LetterOup>();
        }

        public async Task<LetterOup> GetLetterAsync(long PKID)
        {
            var oup = await letterTemplate.FirstOrDefaultAsync(x => x.Valid == true && x.PKID == PKID);
            if (!oup.IsNullEntity())
            {
                return null;
            }
            return oup.MapTo<TP_LetterTemplate, LetterOup>();
        }

        public async Task<BoolMessageOup> DestroyLetterAsync(long PKID)
        {
            var oup = new BoolMessageOup();
            var data = await letterTemplate.FirstOrDefaultAsync(x => x.Valid == true && x.PKID == PKID);
            if (!data.IsNullEntity())
            {
                oup.BoolResult = false;
                oup.Message = "删除失败,请重试";
                return oup;
            }

            data.Valid = false;
            data.LastModifiedTime = DateTime.Now;
            unitOfWork.RegisterUpdate(data);
            oup.BoolResult = await unitOfWork.CommitAsync();
            if (!oup.BoolResult)
            {
                oup.Message = "删除失败,请重试";
            }
            else
            {
                oup.Message = "删除成功";
            }
            return oup;
        }

        public async Task<BoolMessageOup> AddOrUpdateLetterAsync(AddOrUpdateLetterInp input, TP_Account account)
        {
            var oup = new BoolMessageOup();

            if (input.PKID <= 0)
            {
                //新增
                //校验模板数量限制
                var existLetters = await letterTemplate.GetAllListAsync
                    (x => x.Valid == true
                    && x.AccountID == account.PKID
                    && x.CompanyID == account.CompanyID);

                if (account.TemplateLimiteCount > 0 && existLetters.Count() < account.TemplateLimiteCount)
                {
                    var now = DateTime.Now;
                    var letter = new TP_LetterTemplate();
                    letter.AccountID = account.PKID;
                    letter.CompanyID = account.CompanyID;
                    letter.TemplateName = input.TemplateName;
                    letter.TemplateContent = input.TemplateContent;
                    letter.CreatedTime = now;
                    letter.LastModifiedTime = now;
                    letter.Valid = true;
                    unitOfWork.RegisterNew(letter);
                    oup.BoolResult = await unitOfWork.CommitAsync();
                    if (!oup.BoolResult)
                    {
                        oup.Message = "新增失败,请重试";
                    }
                    else
                    {
                        oup.Message = "新增成功";
                    }
                    return oup;
                }
                oup.BoolResult = false;
                oup.Message = "模板数量超出限制";
                return oup;
            }
            //修改
            var existLetter = await letterTemplate.FirstOrDefaultAsync(x => x.PKID == input.PKID);
            if (!existLetter.IsNullEntity())
            {
                oup.BoolResult = false;
                oup.Message = "修改失败";
                return oup;
            }
            existLetter.TemplateName = input.TemplateName;
            existLetter.TemplateContent = input.TemplateContent;
            existLetter.LastModifiedTime = DateTime.Now;
            unitOfWork.RegisterUpdate(existLetter);
            oup.BoolResult = await unitOfWork.CommitAsync();
            if (!oup.BoolResult)
            {
                oup.Message = "修改失败,请重试";
            }
            else
            {
                oup.Message = "修改成功";
            }
            return oup;
        }

        public PagenationOutput<CompanyPagenationOup> GetAllCompanyByPagenation(PagenationInput input)
        {
            PagenationOutput<CompanyPagenationOup> data = new PagenationOutput<CompanyPagenationOup>();
            var total = 0;
            var pageCount = 0;
            var query = company.GetAll(x => x.Valid == true).WhereIf(x => x.CompanyName.Contains(input.KeyWords), !string.IsNullOrEmpty(input.KeyWords));
            var list = company.GetByPagenation<long>
                (input.PageIndex,
                input.PageSize,
                out total,
                out pageCount,
                query,
                x => x.PKID,
                input.IsAsc).ToList();
            data.PageIndex = input.PageIndex;
            data.PageCount = pageCount;
            data.Total = total;
            data.DataList = list.MapToList<TP_Company, CompanyPagenationOup>();
            return data;
        }

        public PagenationOutput<CompanyPagenationOup> GetCheckCompanyByPagenation(PagenationInput input)
        {
            PagenationOutput<CompanyPagenationOup> data = new PagenationOutput<CompanyPagenationOup>();
            var total = 0;
            var pageCount = 0;
            var status = CommonEnum.TP_Company_CheckStatus.待审核.GetHashCode();
            var query = company.GetAll(x => x.Valid == true && x.CheckedStatus == status)
                .WhereIf(x => x.CompanyName.Contains(input.KeyWords), !string.IsNullOrEmpty(input.KeyWords));
            var list = company.GetByPagenation<long>
                (input.PageIndex,
                input.PageSize,
                out total,
                out pageCount,
                query,
                x => x.PKID,
                input.IsAsc).ToList();
            data.PageIndex = input.PageIndex;
            data.PageCount = pageCount;
            data.Total = total;
            data.DataList = list.MapToList<TP_Company, CompanyPagenationOup>();
            return data;
        }

        public PagenationOutput<CompanyPagenationOup> GetNewCompanyByPagenation(PagenationInput input, int days)
        {
            PagenationOutput<CompanyPagenationOup> data = new PagenationOutput<CompanyPagenationOup>();
            var total = 0;
            var pageCount = 0;
            var day = Convert.ToInt32(days);
            var exDay = DateTime.Now.AddDays(-day);
            var query = company.GetAll(x => x.Valid == true && x.CreatedTime >= exDay)
                .WhereIf(x => x.CompanyName.Contains(input.KeyWords), !string.IsNullOrEmpty(input.KeyWords));
            var list = company.GetByPagenation<long>
                (input.PageIndex,
                input.PageSize,
                out total,
                out pageCount,
                query,
                x => x.PKID,
                input.IsAsc).ToList();
            data.PageIndex = input.PageIndex;
            data.PageCount = pageCount;
            data.Total = total;
            data.DataList = list.MapToList<TP_Company, CompanyPagenationOup>();
            return data;
        }

        public async Task<CompanyDetailsOup> GetCompanyDetailsAsync(long PKID)
        {
            var oup = new CompanyDetailsOup();
            oup = (await company.FirstOrDefaultAsync(x => x.PKID == PKID))
                  .MapTo<TP_Company,CompanyDetailsOup>();

            oup.AccountInfo = await GetAccountListAsync(PKID);
            oup.ContractInfo = await GetContractListAsync(PKID);

            return oup;
        }

        public async Task<BoolMessageOup> AddOrUpdateCompanyAsync(AddOrUpdateCompanyInp input)
        {
            var oup = new BoolMessageOup();
            var now = DateTime.Now;
            if (input.PKID <= 0)
            {
                //新增
                var newData = input.MapTo<AddOrUpdateCompanyInp, TP_Company>();
                newData.CreatedTime = now;
                newData.LastModifiedTime = now;
                newData.Valid = true;
                unitOfWork.RegisterNew(newData);
                oup.BoolResult = await unitOfWork.CommitAsync();
                if (!oup.BoolResult)
                {
                    oup.Message = "新增失败";
                }
                else
                {
                    oup.Message = "新增成功";
                }
                return oup;
            }
            //修改
            var existData = await company.FirstOrDefaultAsync(x => x.PKID == input.PKID);
            if (!existData.IsNullEntity())
            {
                oup.BoolResult = false;
                oup.Message = "入参错误";
                return oup;
            }

            existData = input.MapTo<TP_Company>(existData);
            existData.LastModifiedTime = now;
            unitOfWork.RegisterUpdate(existData);
            oup.BoolResult = await unitOfWork.CommitAsync();
            if (!oup.BoolResult)
            {
                oup.Message = "修改失败";
            }
            else
            {
                oup.Message = "修改成功";
            }
            return oup;
        }

        public async Task<BoolMessageOup> AddOrUpdateAccountAsync(AddOrUpdateAccountInp input)
        {
            var oup = new BoolMessageOup();
            var now = DateTime.Now;
            if (input.PKID <= 0)
            {
                //新增
                var newData = input.MapTo<AddOrUpdateAccountInp, TP_Account>();
                newData.Domain = "@" + newData.Email.Split('@')[1];
                newData.CreatedTime = now;
                newData.LastModifiedTime = now;
                newData.Valid = true;
                unitOfWork.RegisterNew(newData);
                oup.BoolResult = await unitOfWork.CommitAsync();
                if (!oup.BoolResult)
                {
                    oup.Message = "新增失败";
                }
                else
                {
                    oup.Message = "新增成功";
                }
                return oup;
            }
            //修改
            var existData = await account.FirstOrDefaultAsync(x => x.PKID == input.PKID);
            if (!existData.IsNullEntity())
            {
                oup.BoolResult = false;
                oup.Message = "入参错误";
                return oup;
            }

            existData = input.MapTo<TP_Account>(existData);
            existData.LastModifiedTime = now;
            unitOfWork.RegisterUpdate(existData);
            oup.BoolResult = await unitOfWork.CommitAsync();
            if (!oup.BoolResult)
            {
                oup.Message = "修改失败";
            }
            else
            {
                oup.Message = "修改成功";
            }
            return oup;
        }

        public async Task<BoolMessageOup> AddOrUpdateContractAsync(AddOrUpdateContractInp input)
        {
            var oup = new BoolMessageOup();
            var now = DateTime.Now;
            if (input.PKID <= 0)
            {
                //新增
                var newData = input.MapTo<AddOrUpdateContractInp, TP_CompanyContract>();
                newData.CreatedTime = now;
                newData.LastModifiedTime = now;
                newData.Valid = true;
                unitOfWork.RegisterNew(newData);
                oup.BoolResult = await unitOfWork.CommitAsync();
                if (!oup.BoolResult)
                {
                    oup.Message = "新增失败";
                }
                else
                {
                    oup.Message = "新增成功";
                }
                return oup;
            }
            //修改
            var existData = await companyContract.FirstOrDefaultAsync(x => x.PKID == input.PKID);
            if (!existData.IsNullEntity())
            {
                oup.BoolResult = false;
                oup.Message = "入参错误";
                return oup;
            }

            existData = input.MapTo<TP_CompanyContract>(existData);
            existData.LastModifiedTime = now;
            unitOfWork.RegisterUpdate(existData);
            oup.BoolResult = await unitOfWork.CommitAsync();
            if (!oup.BoolResult)
            {
                oup.Message = "修改失败";
            }
            else
            {
                oup.Message = "修改成功";
            }
            return oup;
        }

        public async Task<List<AgentCompanyOup>> GetAgentCompanyListAsync(long companyID)
        {
            var oup = (await agentCompany.GetAllListAsync(x => x.BasicCompanyID == companyID && x.Valid == true))
                .MapToList<TP_AgentCompany, AgentCompanyOup>();
            if (oup.Count <= 0)
            {
                return null;
            }
            return oup;
        }

        public PagenationOutput<AgentCompanyListOup> GetAgentCompanyByPagenation(PagenationInput input, long companyID, long accountID)
        {
            PagenationOutput<AgentCompanyListOup> data = new PagenationOutput<AgentCompanyListOup>();
            var total = 0;
            var pageCount = 0;
            var query = agentCompany.GetAll(x => x.Valid == true && x.BasicCompanyID == companyID)
                .WhereIf(x => x.CompanyName.Contains(input.KeyWords), !string.IsNullOrEmpty(input.KeyWords));
            var list = agentCompany.GetByPagenation<long>
                (input.PageIndex,
                input.PageSize,
                out total,
                out pageCount,
                query,
                x => x.PKID,
                input.IsAsc).ToList();
            data.PageIndex = input.PageIndex;
            data.PageCount = pageCount;
            data.Total = total;
            data.DataList = list.MapToList<TP_AgentCompany, AgentCompanyListOup>();
            if (data.DataList.Count > 0)
            {
                data.DataList.ForEach(async item =>
                {
                    item.JobNumber = await job.CountAsync(
                        x => x.Valid == true
                        && x.AgentJob == true
                        && x.BasicCompanyID == accountID
                        && x.JobCompanyID == item.PKID);
                });
            }
            return data;
        }

        public async Task<BoolMessageOup> AddOrUpdateAgentCompanyAsync(AddOrUpdateAgentCompanyInp input, long companyID, long accountID)
        {
            var oup = new BoolMessageOup();
            var now = DateTime.Now;
            if (input.PKID <= 0)
            {
                //新增
                var newData = input.MapTo<AddOrUpdateAgentCompanyInp, TP_AgentCompany>();
                newData.BasicCompanyID = companyID;
                newData.AccountID = accountID;
                newData.CreatedTime = now;
                newData.LastModifiedTime = now;
                newData.Valid = true;
                unitOfWork.RegisterNew(newData);
                oup.BoolResult = await unitOfWork.CommitAsync();
                if (!oup.BoolResult)
                {
                    oup.Message = "新增失败";
                }
                else
                {
                    oup.Message = "新增成功";
                }
                return oup;
            }
            //修改
            var existData = await agentCompany.FirstOrDefaultAsync(x => x.PKID == input.PKID);
            if (!existData.IsNullEntity())
            {
                oup.BoolResult = false;
                oup.Message = "入参错误";
                return oup;
            }

            existData = input.MapTo<TP_AgentCompany>(existData);
            existData.LastModifiedTime = now;
            unitOfWork.RegisterUpdate(existData);
            oup.BoolResult = await unitOfWork.CommitAsync();
            if (!oup.BoolResult)
            {
                oup.Message = "修改失败";
            }
            else
            {
                oup.Message = "修改成功";
            }
            return oup;
        }

        public async Task<AgentCompanySingleOup> GetAgentCompanyByIDAsync(long PKID)
        {
            var oup = (await agentCompany.FirstOrDefaultAsync(x => x.PKID == PKID && x.Valid == true))
                .MapTo<TP_AgentCompany, AgentCompanySingleOup>();
            if (!oup.IsNullEntity())
            {
                return null;
            }
            return oup;
        }
    }
}
