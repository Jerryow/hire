using MrMatch.Application.System.Inp;
using MrMatch.Application.System.Oup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.System
{
    public interface ISystemService
    {
        #region system_user
        /// <summary>
        /// 分页获取系统用户的数据
        /// </summary>
        /// <param name="pagenationInput"></param>
        /// <returns></returns>
        PagenationOutput<SystemUserListOup> GetSystemUserByPagenation(PagenationInput input);

        /// <summary>
        /// 新增系统用户
        /// </summary>
        /// <param name="addSystemUserInp"></param>
        /// <returns></returns>
        Task<BoolMessageOup> AddOrUpdateSystemUserAsync(AddOrUpdateSystemUserInp input);

        /// <summary>
        /// 注销系统用户
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        Task<BoolMessageOup> DestroySystemUserAsync(long PKID);

        /// <summary>
        /// 根据ID获取系统用户
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        Task<SystemUserOup> GetSystemUserByIDAsync(long PKID);

        /// <summary>
        /// 根据ID获取系统用户
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        SystemUserOup GetSystemUser(long PKID);
        #endregion

        #region system_notice
        /// <summary>
        /// 分页获取系统推送消息的数据
        /// </summary>
        /// <param name="pagenationInput"></param>
        /// <returns></returns>
        PagenationOutput<NoticeListOup> GetNoticeByPagenation(PagenationInput input);

        /// <summary>
        /// 新增系统推送消息
        /// </summary>
        /// <param name="addSystemUserInp"></param>
        /// <returns></returns>
        Task<BoolMessageOup> AddOrUpdateNoticeAsync(AddOrUpdateNoticeInp input);

        /// <summary>
        /// 删除系统推送消息
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        Task<BoolMessageOup> DestroyNoticeAsync(long PKID);

        /// <summary>
        /// 根据ID获取系统推送消息
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        Task<NoticeOup> GetNoticeByIDAsync(long PKID);

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        Task<BoolMessageOup> SendNoticeAsync(long PKID);
        #endregion

        #region system_config
        /// <summary>
        /// 获取所有系统配置数据
        /// </summary>
        /// <returns></returns>
        Task<List<SiteConfigListOup>> GetAllSiteConfigAsync();

        /// <summary>
        /// 新增系统配置数据
        /// </summary>
        /// <param name="addSystemUserInp"></param>
        /// <returns></returns>
        Task<BoolMessageOup> AddOrUpdateSiteConfigAsync(AddOrUpdateSiteConfigInp input);

        /// <summary>
        /// 根据Code获取系统配置数据
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<SiteConfigOup> GetSiteConfigByCodeAsync(string code);

        /// <summary>
        /// 根据Code获取系统配置数据
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        Task<SiteConfigOup> GetSiteConfigByIDAsync(long PKID);

        /// <summary>
        /// 根据Code获取系统配置数据
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<string> GetSiteConfigValueByCodeAsync(string code);
        #endregion

        #region system_messageconfig
        /// <summary>
        /// 分页获取第三方消息发送数据
        /// </summary>
        /// <param name="pagenationInput"></param>
        /// <returns></returns>
        PagenationOutput<MessageConfigListOup> GetMessageConfigByPagenation(PagenationInput input);

        /// <summary>
        /// 新增第三方消息发送数据
        /// </summary>
        /// <param name="addSystemUserInp"></param>
        /// <returns></returns>
        Task<BoolMessageOup> AddOrUpdateMessageConfigAsync(AddOrUpdateMsgConfigInp input);

        /// <summary>
        /// 根据ID获取第三方消息发送数据
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        Task<MessageConfigOup> GetMessageConfigByIDAsync(long PKID);
        #endregion

        #region system_messagetemplate
        /// <summary>
        /// 分页获取消息模板数据
        /// </summary>
        /// <param name="pagenationInput"></param>
        /// <returns></returns>
        PagenationOutput<MessageTemplateListOup> GetMessageTemplateByPagenation(PagenationInput input);

        /// <summary>
        /// 新增消息模板数据
        /// </summary>
        /// <param name="addSystemUserInp"></param>
        /// <returns></returns>
        Task<BoolMessageOup> AddOrUpdateMessageTemplateAsync(AddOrUpdateMsgTemplateInp input);

        /// <summary>
        /// 根据ID获取消息模板
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        Task<MessageTemplateOup> GetMessageTemplateByIDAsync(long PKID);
        #endregion
    }
}
