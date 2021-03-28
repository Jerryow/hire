using MrMatch.Application.System.Oup;
using MrMatch.Domain.Models;
using MrMatch.Domain.EntityBase.Repository;
using MrMatch.Common.Mapper;
using MrMatch.Application.System.Inp;
using System.Threading.Tasks;
using MrMatch.MysqlFramework.Extensions;
using System;
using System.Configuration;
using System.Collections.Generic;
using MrMatch.Common.Encrypt;
using MrMatch.Common.Redis;
using System.Linq;

namespace MrMatch.Application.System
{
    public class SystemService : ISystemService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<TP_SystemUser> sysUser;
        private readonly IRepository<TP_Notice> notice;
        private readonly IRepository<TP_MessageConfig> msgConfig;
        private readonly IRepository<TP_MessageTemplate> msgTemplate;
        private readonly IRepository<TP_SiteConfig> siteConfig;
        public SystemService(
            IUnitOfWork _unitOfWork,
            IRepository<TP_SystemUser> _sysUser,
            IRepository<TP_Notice> _notice,
            IRepository<TP_MessageConfig> _msgConfig,
            IRepository<TP_MessageTemplate> _msgTemplate,
            IRepository<TP_SiteConfig> _siteConfig)
        {
            unitOfWork = _unitOfWork;
            sysUser = _sysUser;
            notice = _notice;
            msgConfig = _msgConfig;
            msgTemplate = _msgTemplate;
            siteConfig = _siteConfig;
        }

        #region system_user
        public PagenationOutput<SystemUserListOup> GetSystemUserByPagenation(PagenationInput input)
        {
            PagenationOutput<SystemUserListOup> data = new PagenationOutput<SystemUserListOup>();
            var total = 0;
            var pageCount = 0;
            var query = sysUser.GetAll(x => x.Valid == true).WhereIf(x => x.LoginName.Contains(input.KeyWords), !string.IsNullOrEmpty(input.KeyWords));
            var list = sysUser.GetByPagenation<long>
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
            data.DataList = list.MapToList<TP_SystemUser, SystemUserListOup>();
            return data;
        }

        public async Task<BoolMessageOup> AddOrUpdateSystemUserAsync(AddOrUpdateSystemUserInp input)
        {
            var rtn = new BoolMessageOup();

            var now = DateTime.Now;

            //新增
            if (input.PKID <= 0)
            {
                var addUser = input.MapTo<AddOrUpdateSystemUserInp, TP_SystemUser>();
                addUser.Password = Encryption.EncryptString(addUser.LoginName + "-" + addUser.Password);
                addUser.ActiveTime = now;
                addUser.CreatedTime = now;
                addUser.LastModifiedTime = now;
                addUser.Valid = true;
                unitOfWork.RegisterNew(addUser);

                rtn.BoolResult = await unitOfWork.CommitAsync();
                if (rtn.BoolResult)
                {
                    rtn.Message = "新增成功";
                }
                else
                {
                    rtn.Message = "新增失败";
                }
            }
            else
            {
                var updateUser = await sysUser.GetAsync(input.PKID);
                if (!updateUser.IsNullEntity())
                {
                    rtn.BoolResult = false;
                    rtn.Message = "找不到目标数据";
                    return rtn;
                }

                updateUser = input.MapTo<TP_SystemUser>(updateUser);
                updateUser.Password = Encryption.EncryptString(updateUser.LoginName + "-" + updateUser.Password);
                updateUser.LastModifiedTime = now;
                unitOfWork.RegisterUpdate(updateUser);
                rtn.BoolResult = await unitOfWork.CommitAsync();
                if (rtn.BoolResult)
                {
                    rtn.Message = "修改成功";
                }
                else
                {
                    rtn.Message = "修改失败";
                }
            }
            return rtn;
        }

        public async Task<BoolMessageOup> DestroySystemUserAsync(long PKID)
        {
            var rtn = new BoolMessageOup();

            //判断是否存在数据
            var destroyUser = await sysUser.GetAsync(PKID);
            if (!destroyUser.IsNullEntity())
            {
                rtn.Message = "找不到目标数据";
                rtn.BoolResult = false;
                return rtn;
            }

            destroyUser.OnActive = false;
            unitOfWork.RegisterUpdate(destroyUser);
            rtn.BoolResult = await unitOfWork.CommitAsync();
            if (rtn.BoolResult)
            {
                rtn.Message = "注销成功";
            }
            else
            {
                rtn.Message = "注销失败";
            }
            return rtn;
        }

        public async Task<SystemUserOup> GetSystemUserByIDAsync(long PKID)
        {
            var user = await sysUser.GetAsync(PKID);
            if (!user.IsNullEntity())
            {
                return null;
            }

            var oup = user.MapTo<TP_SystemUser, SystemUserOup>();
            return oup;
        }

        public SystemUserOup GetSystemUser(long PKID)
        {
            var user = sysUser.Get(PKID);
            if (!user.IsNullEntity())
            {
                return null;
            }

            var oup = user.MapTo<TP_SystemUser, SystemUserOup>();
            return oup;
        }

        #endregion

        #region system_notice
        public PagenationOutput<NoticeListOup> GetNoticeByPagenation(PagenationInput input)
        {
            PagenationOutput<NoticeListOup> data = new PagenationOutput<NoticeListOup>();
            var total = 0;
            var pageCount = 0;
            var query = notice.GetAll(x => x.Valid == true).WhereIf(x => x.MessageSubject.Contains(input.KeyWords), !string.IsNullOrEmpty(input.KeyWords));
            var list = notice.GetByPagenation<long>
                (input.PageIndex,
                input.PageSize,
                out total,
                out pageCount,
                query,
                x => x.PKID,
                input.IsAsc);
            data.PageIndex = input.PageIndex;
            data.PageCount = pageCount;
            data.Total = total;
            data.DataList = list.MapToList<TP_Notice, NoticeListOup>();
            return data;
        }

        public async Task<BoolMessageOup> AddOrUpdateNoticeAsync(AddOrUpdateNoticeInp input)
        {
            var rtn = new BoolMessageOup();
            var now = DateTime.Now;


            //新增
            if (input.PKID <= 0)
            {

                var addNotice = input.MapTo<AddOrUpdateNoticeInp, TP_Notice>();
                //初始新增默认false
                addNotice.IsFinish = false;
                addNotice.CreatedTime = now;
                addNotice.LastModifiedTime = now;
                addNotice.Valid = true;
                unitOfWork.RegisterNew(addNotice);

                rtn.BoolResult = await unitOfWork.CommitAsync();
                if (rtn.BoolResult)
                {
                    rtn.Message = "新增成功";
                }
                else
                {
                    rtn.Message = "新增失败";
                }
            }
            else
            {
                var updateNotice = await notice.GetAsync(input.PKID);
                if (!updateNotice.IsNullEntity())
                {
                    rtn.BoolResult = false;
                    rtn.Message = "找不到目标数据";
                    return rtn;
                }

                //已发送的消息不允许编辑修改
                if (updateNotice.IsFinish)
                {
                    rtn.BoolResult = false;
                    rtn.Message = "已发送的消息不允许编辑";
                    return rtn;
                }

                updateNotice = input.MapTo<TP_Notice>(updateNotice);
                updateNotice.LastModifiedTime = now;
                unitOfWork.RegisterUpdate(updateNotice);
                rtn.BoolResult = await unitOfWork.CommitAsync();
                if (rtn.BoolResult)
                {
                    rtn.Message = "修改成功";
                }
                else
                {
                    rtn.Message = "修改失败";
                }
            }
            return rtn;
        }

        public async Task<BoolMessageOup> DestroyNoticeAsync(long PKID)
        {
            var rtn = new BoolMessageOup();

            //判断是否存在数据
            var destroyNotice = await notice.GetAsync(PKID);
            if (!destroyNotice.IsNullEntity())
            {
                rtn.Message = "找不到目标数据";
                rtn.BoolResult = false;
                return rtn;
            }

            destroyNotice.Valid = false;
            unitOfWork.RegisterUpdate(destroyNotice);
            rtn.BoolResult = await unitOfWork.CommitAsync();
            if (rtn.BoolResult)
            {
                rtn.Message = "删除成功";
            }
            else
            {
                rtn.Message = "删除失败";
            }
            return rtn;
        }

        public async Task<NoticeOup> GetNoticeByIDAsync(long PKID)
        {
            var noticeData = await notice.GetAsync(PKID);
            if (!noticeData.IsNullEntity())
            {
                return null;
            }

            var oup = noticeData.MapTo<TP_Notice, NoticeOup>();
            return oup;
        }

        public async Task<BoolMessageOup> SendNoticeAsync(long PKID)
        {
            var rtn = new BoolMessageOup();

            var now = DateTime.Now;

            var noticeData = await notice.GetAsync(PKID);
            if (!noticeData.IsNullEntity())
            {
                rtn.Message = "找不到目标数据";
                rtn.BoolResult = false;
                return rtn;
            }

            noticeData.IsFinish = true;
            noticeData.LastModifiedTime = now;
            //判断是指定用户还是所有用户
            if (noticeData.MessageType == Convert.ToInt32(CommonEnum.TP_Notice_MessageType.指定用户))
            {
                var ids = noticeData.UserIDs.Split(',');
                for (int i = 0; i < ids.Length; i++)
                {
                    TP_NoticeUser newData = new TP_NoticeUser();
                    newData.MessageID = PKID;
                    newData.UserID = Convert.ToInt64(ids[i]);
                    newData.CreatedTime = now;
                    newData.LastModifiedTime = now;
                    newData.Valid = true;
                    unitOfWork.RegisterNew(newData);
                }
            }
            unitOfWork.RegisterUpdate(noticeData);
            rtn.BoolResult = await unitOfWork.CommitAsync();
            if (rtn.BoolResult)
            {
                rtn.Message = "发送成功";
            }
            else
            {
                rtn.Message = "发送失败";
            }

            return rtn;
        }

        #endregion

        #region system_config
        public async Task<List<SiteConfigListOup>> GetAllSiteConfigAsync()
        {
            var configs = await siteConfig.GetAllListAsync();
            if (configs.Count <= 0)
            {
                return null;
            }

            var oup = configs.MapToList<TP_SiteConfig, SiteConfigListOup>();
            return oup;
        }

        public async Task<BoolMessageOup> AddOrUpdateSiteConfigAsync(AddOrUpdateSiteConfigInp input)
        {
            var rtn = new BoolMessageOup();
            var now = DateTime.Now;

            //新增
            if (input.PKID <= 0)
            {
                var addConfig = input.MapTo<AddOrUpdateSiteConfigInp, TP_SiteConfig>();
                addConfig.OnInternal = true;
                addConfig.CreatedTime = now;
                addConfig.LastModifiedTime = now;
                addConfig.Valid = true;
                unitOfWork.RegisterNew(addConfig);

                rtn.BoolResult = await unitOfWork.CommitAsync();
                if (rtn.BoolResult)
                {
                    rtn.Message = "新增成功";
                }
                else
                {
                    rtn.Message = "新增失败";
                }
            }
            else
            {
                var updateConfig = await siteConfig.GetAsync(input.PKID);
                if (!updateConfig.IsNullEntity())
                {
                    rtn.BoolResult = false;
                    rtn.Message = "找不到目标数据";
                    return rtn;
                }

                updateConfig = input.MapTo<TP_SiteConfig>(updateConfig);
                updateConfig.LastModifiedTime = now;
                unitOfWork.RegisterUpdate(updateConfig);
                rtn.BoolResult = await unitOfWork.CommitAsync();
                if (rtn.BoolResult)
                {
                    rtn.Message = "修改成功";
                }
                else
                {
                    rtn.Message = "修改失败";
                }
            }
            return rtn;
        }

        public async Task<SiteConfigOup> GetSiteConfigByCodeAsync(string code)
        {
            var config = await siteConfig.FirstOrDefaultAsync(x => x.ConfigCode == code);
            if (!config.IsNullEntity())
            {
                return null;
            }

            var oup = config.MapTo<TP_SiteConfig, SiteConfigOup>();
            return oup;
        }
        public async Task<SiteConfigOup> GetSiteConfigByIDAsync(long PKID)
        {
            var config = await siteConfig.FirstOrDefaultAsync(x => x.PKID == PKID);
            if (!config.IsNullEntity())
            {
                return null;
            }

            var oup = config.MapTo<TP_SiteConfig, SiteConfigOup>();
            return oup;
        }
        public async Task<string> GetSiteConfigValueByCodeAsync(string code)
        {
            var config = Cache.GetCacheHelper.GetSiteConfig(code);
            if (string.IsNullOrEmpty(config))
            {
               var data = await siteConfig.FirstOrDefaultAsync(x => x.ConfigCode == code);
                if (!data.IsNullEntity())
                {
                    config = "";
                }
                else
                {
                    config = data.ConfigValue;
                }
            }
            return config;
        }
        #endregion

        #region system_messageconfig
        public PagenationOutput<MessageConfigListOup> GetMessageConfigByPagenation(PagenationInput input)
        {
            PagenationOutput<MessageConfigListOup> data = new PagenationOutput<MessageConfigListOup>();
            var total = 0;
            var pageCount = 0;
            var query = msgConfig.GetAll(x => x.Valid == true).WhereIf(x => x.ProviderName.Contains(input.KeyWords), !string.IsNullOrEmpty(input.KeyWords));
            var list = msgConfig.GetByPagenation<long>
                (input.PageIndex,
                input.PageSize,
                out total,
                out pageCount,
                query,
                x => x.PKID,
                input.IsAsc);
            data.PageIndex = input.PageIndex;
            data.PageCount = pageCount;
            data.Total = total;
            data.DataList = list.MapToList<TP_MessageConfig, MessageConfigListOup>();
            return data;
        }

        public async Task<BoolMessageOup> AddOrUpdateMessageConfigAsync(AddOrUpdateMsgConfigInp input)
        {
            var rtn = new BoolMessageOup();

            var now = DateTime.Now;

            //新增
            if (input.PKID <= 0)
            {
                var addConfig = input.MapTo<AddOrUpdateMsgConfigInp, TP_MessageConfig>();
                addConfig.Remark = "";
                addConfig.CreatedTime = now;
                addConfig.LastModifiedTime = now;
                addConfig.Valid = true;
                unitOfWork.RegisterNew(addConfig);

                rtn.BoolResult = await unitOfWork.CommitAsync();
                if (rtn.BoolResult)
                {
                    rtn.Message = "新增成功";
                }
                else
                {
                    rtn.Message = "新增失败";
                }
            }
            else
            {
                var updateConfig = await msgConfig.GetAsync(input.PKID);
                if (!updateConfig.IsNullEntity())
                {
                    rtn.BoolResult = false;
                    rtn.Message = "找不到目标数据";
                    return rtn;
                }

                updateConfig = input.MapTo<TP_MessageConfig>(updateConfig);
                updateConfig.LastModifiedTime = now;
                unitOfWork.RegisterUpdate(updateConfig);
                rtn.BoolResult = await unitOfWork.CommitAsync();
                if (rtn.BoolResult)
                {
                    rtn.Message = "修改成功";
                }
                else
                {
                    rtn.Message = "修改失败";
                }
            }
            return rtn;
        }

        public async Task<MessageConfigOup> GetMessageConfigByIDAsync(long PKID)
        {
            var config = await msgConfig.GetAsync(PKID);
            if (!config.IsNullEntity())
            {
                return null;
            }

            var oup = config.MapTo<TP_MessageConfig, MessageConfigOup>();
            return oup;
        }
        #endregion

        #region system_messagetemplate
        public PagenationOutput<MessageTemplateListOup> GetMessageTemplateByPagenation(PagenationInput input)
        {
            PagenationOutput<MessageTemplateListOup> data = new PagenationOutput<MessageTemplateListOup>();
            var total = 0;
            var pageCount = 0;
            var query = msgTemplate.GetAll(x => x.Valid == true).WhereIf(x => x.TemplateTitle.Contains(input.KeyWords), !string.IsNullOrEmpty(input.KeyWords));
            var list = msgTemplate.GetByPagenation<long>
                (input.PageIndex,
                input.PageSize,
                out total,
                out pageCount,
                query,
                x => x.PKID,
                input.IsAsc);
            data.PageIndex = input.PageIndex;
            data.PageCount = pageCount;
            data.Total = total;
            data.DataList = list.MapToList<TP_MessageTemplate, MessageTemplateListOup>();
            return data;
        }

        public async Task<BoolMessageOup> AddOrUpdateMessageTemplateAsync(AddOrUpdateMsgTemplateInp input)
        {
            var rtn = new BoolMessageOup();

            var now = DateTime.Now;

            //新增
            if (input.PKID <= 0)
            {
                var addTemplate = input.MapTo<AddOrUpdateMsgTemplateInp, TP_MessageTemplate>();
                addTemplate.Remark = "";
                addTemplate.CreatedTime = now;
                addTemplate.LastModifiedTime = now;
                addTemplate.Valid = true;
                unitOfWork.RegisterNew(addTemplate);

                rtn.BoolResult = await unitOfWork.CommitAsync();
                if (rtn.BoolResult)
                {
                    rtn.Message = "新增成功";
                }
                else
                {
                    rtn.Message = "新增失败";
                }
            }
            else
            {
                var updateTemplate = await msgTemplate.GetAsync(input.PKID);
                if (!updateTemplate.IsNullEntity())
                {
                    rtn.BoolResult = false;
                    rtn.Message = "找不到目标数据";
                    return rtn;
                }

                updateTemplate = input.MapTo<TP_MessageTemplate>(updateTemplate);
                updateTemplate.LastModifiedTime = now;
                unitOfWork.RegisterUpdate(updateTemplate);
                rtn.BoolResult = await unitOfWork.CommitAsync();
                if (rtn.BoolResult)
                {
                    rtn.Message = "修改成功";
                }
                else
                {
                    rtn.Message = "修改失败";
                }
            }
            return rtn;
        }

        public async Task<MessageTemplateOup> GetMessageTemplateByIDAsync(long PKID)
        {
            var template = await msgTemplate.GetAsync(PKID);
            if (!template.IsNullEntity())
            {
                return null;
            }

            var oup = template.MapTo<TP_MessageTemplate, MessageTemplateOup>();
            return oup;
        }

        #endregion
    }
}
