using MrMatch.Admin.Api.Base;
using MrMatch.Application;
using MrMatch.Application.System;
using MrMatch.Application.System.Inp;
using MrMatch.Common.Encrypt;
using MrMatch.Common.LogHelper;
using MrMatch.Common.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace MrMatch.Admin.Api
{
    /// <summary>
    /// 系统数据/系统配置
    /// </summary>
    public class SystemApiController : SecurityBaseController
    {
        #region DI
        private readonly ILogService log;
        private readonly ISystemService systemService;

        #region websocket
        private readonly ClientWebSocket webSocket = new ClientWebSocket();
        private readonly CancellationToken _cancellation = new CancellationToken();
        #endregion

        public SystemApiController(
            ILogService _log,
            ISystemService _systemService)
        {
            log = _log;
            systemService = _systemService;
        }
        #endregion

        #region system_user
        /// <summary>
        /// 分页获取系统用户的数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="keyWords">搜索关键字</param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetSysUserByPagenation(int pageIndex, int pageSize, string keyWords)
        {
            try
            {
                var pagenation = new PagenationInput(pageIndex, pageSize, keyWords, false);

                var data = systemService.GetSystemUserByPagenation(pagenation);
                //log.LogDebug(Newtonsoft.Json.JsonConvert.SerializeObject(data));
                return Succcess("成功.", data);
            }
            catch (Exception ex)
            {
                log.LogError("[分页获取系统用户的数据]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 获取单个系统用户
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetUserById(long PKID)
        {
            try
            {
                var user = await systemService.GetSystemUserByIDAsync(PKID);
                return Succcess("成功", user);
            }
            catch (Exception ex)
            {
                log.LogError("[获取单个系统用户]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }

        }

        /// <summary>
        /// 新增系统用户
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> AddUser(AddOrUpdateSystemUserInp form)
        {
            try
            {
                var res = await systemService.AddOrUpdateSystemUserAsync(form);
                if (res.BoolResult)
                {
                    return Succcess(res.Message);
                }
                return Fail(res.Message);
            }
            catch (Exception ex)
            {
                log.LogError("[新增系统用户]" + ex.Message, ex);
                return Fail("新增系统用户失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 修改系统用户
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> UpdateUser(AddOrUpdateSystemUserInp form)
        {
            try
            {
                var res = await systemService.AddOrUpdateSystemUserAsync(form);
                if (res.BoolResult)
                {
                    return Succcess(res.Message);
                }
                return Fail(res.Message);
            }
            catch (Exception ex)
            {
                log.LogError("[修改系统用户]" + ex.Message, ex);
                return Fail("修改系统用户失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 注销系统用户
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> DestroyUser(long PKID)
        {
            try
            {
                var res = await systemService.DestroySystemUserAsync(PKID);
                if (res.BoolResult)
                {
                    return Succcess(res.Message);
                }
                return Fail(res.Message);
            }
            catch (Exception ex)
            {
                log.LogError("[注销系统用户]" + ex.Message, ex);
                return Fail("注销系统用户失败,请刷新重试.");
            }
        }
        #endregion

        #region siteconfig
        /// <summary>
        /// 获取系统配置数据
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetSiteConfigList()
        {
            try
            {
                var res = await systemService.GetAllSiteConfigAsync();
                if (res.Count <= 0)
                {
                    return SucccessNull("未找到数据", null);
                }
                return Succcess("成功", res);
            }
            catch (Exception ex)
            {
                log.LogError("[获取系统配置数据]" + ex.Message, ex);
                return Fail("获取系统配置数据失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 根据编号获取系统配置数据
        /// </summary>
        /// <param name="configCode"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetSiteConfigByCode(string configCode)
        {
            try
            {
                var res = await systemService.GetSiteConfigByCodeAsync(configCode);
                
                return Succcess("成功", res);
            }
            catch (Exception ex)
            {
                log.LogError("[根据编号获取系统配置数据]" + ex.Message, ex);
                return Fail("根据编号获取系统配置数据失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 修改系统配置
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> UpdateSiteConfig(AddOrUpdateSiteConfigInp form)
        {
            try
            {
                var res = await systemService.AddOrUpdateSiteConfigAsync(form);
                if (res.BoolResult)
                {
                    return Succcess(res.Message);
                }
                return Fail(res.Message);
            }
            catch (Exception ex)
            {
                log.LogError("[修改系统配置]" + ex.Message, ex);
                return Fail("修改系统配置失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 新增系统配置
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> AddSiteConfig(AddOrUpdateSiteConfigInp form)
        {
            try
            {
                var res = await systemService.AddOrUpdateSiteConfigAsync(form);
                if (res.BoolResult)
                {
                    return Succcess(res.Message);
                }
                return Fail(res.Message);
            }
            catch (Exception ex)
            {
                log.LogError("[新增系统配置]" + ex.Message, ex);
                return Fail("新增系统配置失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 存入缓存
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> SetSiteConfigCache(long PKID)
        {
            try
            {
                var res = await systemService.GetSiteConfigByIDAsync(PKID);
                Application.Cache.SetCacheHelper.SetSiteConfig(res.ConfigCode, res.ConfigValue);
                return Succcess("成功");
            }
            catch (Exception ex)
            {
                log.LogError("[根据编号获取系统配置数据]" + ex.Message, ex);
                return Fail("根据编号获取系统配置数据失败,请刷新重试.");
            }
        }
        #endregion

        #region messageconfig
        /// <summary>
        /// 分页获取第三方消息发送数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="keyWords">搜索关键字</param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetMessageConfigByPagenation(int pageIndex, int pageSize, string keyWords)
        {
            try
            {
                var pagenation = new PagenationInput(pageIndex, pageSize, keyWords, false);

                var data = systemService.GetMessageConfigByPagenation(pagenation);
                //log.LogDebug(Newtonsoft.Json.JsonConvert.SerializeObject(data));
                return Succcess("成功.", data);
            }
            catch (Exception ex)
            {
                log.LogError("[分页获取第三方消息发送数据]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 修改第三方消息发送数据
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> UpdateMessageConfig(AddOrUpdateMsgConfigInp form)
        {
            try
            {
                var res = await systemService.AddOrUpdateMessageConfigAsync(form);
                if (res.BoolResult)
                {
                    return Succcess(res.Message);
                }
                return Fail(res.Message);
            }
            catch (Exception ex)
            {
                log.LogError("[修改系统配置]" + ex.Message, ex);
                return Fail("修改系统配置失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 新增第三方消息发送数据
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> AddMessageConfig(AddOrUpdateMsgConfigInp form)
        {
            try
            {
                var res = await systemService.AddOrUpdateMessageConfigAsync(form);
                if (res.BoolResult)
                {
                    return Succcess(res.Message);
                }
                return Fail(res.Message);
            }
            catch (Exception ex)
            {
                log.LogError("[修改系统配置]" + ex.Message, ex);
                return Fail("修改系统配置失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 根据ID获取第三方消息发送数据
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetMessageConfigById(long PKID)
        {
            try 
            { 
                var data = await systemService.GetMessageConfigByIDAsync(PKID);
                //log.LogDebug(Newtonsoft.Json.JsonConvert.SerializeObject(data));
                return Succcess("成功.", data);
            }
            catch (Exception ex)
            {
                log.LogError("[分页获取第三方消息发送数据]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }
        }
        #endregion

        #region messagetemplate
        /// <summary>
        /// 分页获取消息模板数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="keyWords">搜索关键字</param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetMessageTemplatePagenation(int pageIndex, int pageSize, string keyWords)
        {
            try
            {
                var pagenation = new PagenationInput(pageIndex, pageSize, keyWords, false);

                var data = systemService.GetMessageTemplateByPagenation(pagenation);
                //log.LogDebug(Newtonsoft.Json.JsonConvert.SerializeObject(data));
                return Succcess("成功.", data);
            }
            catch (Exception ex)
            {
                log.LogError("[分页获取第三方消息发送数据]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 修改消息模板数据
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> UpdateMessageTemplate(AddOrUpdateMsgTemplateInp form)
        {
            try
            {
                var res = await systemService.AddOrUpdateMessageTemplateAsync(form);
                if (res.BoolResult)
                {
                    return Succcess(res.Message);
                }
                return Fail(res.Message);
            }
            catch (Exception ex)
            {
                log.LogError("[修改系统配置]" + ex.Message, ex);
                return Fail("修改系统配置失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 新增消息模板数据
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> AddMessageTemplate(AddOrUpdateMsgTemplateInp form)
        {
            try
            {
                var res = await systemService.AddOrUpdateMessageTemplateAsync(form);
                if (res.BoolResult)
                {
                    return Succcess(res.Message);
                }
                return Fail(res.Message);
            }
            catch (Exception ex)
            {
                log.LogError("[修改系统配置]" + ex.Message, ex);
                return Fail("修改系统配置失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 根据ID获取消息模板数据
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetMessageTemplateById(long PKID)
        {
            try
            {
                var data = await systemService.GetMessageTemplateByIDAsync(PKID);
                //log.LogDebug(Newtonsoft.Json.JsonConvert.SerializeObject(data));
                return Succcess("成功.", data);
            }
            catch (Exception ex)
            {
                log.LogError("[分页获取第三方消息发送数据]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 存入缓存
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> SetTemplateCache(long PKID)
        {
            try
            {
                var res = await systemService.GetMessageTemplateByIDAsync(PKID);
                Application.Cache.SetCacheHelper.SetMessageTemplate(res.TemplateCode, res.TemplateContent);
                return Succcess("成功");
            }
            catch (Exception ex)
            {
                log.LogError("[根据编号获取系统配置数据]" + ex.Message, ex);
                return Fail("根据编号获取系统配置数据失败,请刷新重试.");
            }
        }
        #endregion

        #region systemnotice
        /// <summary>
        /// 分页获取系统推送消息数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="keyWords">搜索关键字</param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetSystemNoticePagenation(int pageIndex, int pageSize, string keyWords)
        {
            try
            {
                var pagenation = new PagenationInput(pageIndex, pageSize, keyWords, false);

                var data = systemService.GetNoticeByPagenation(pagenation);
                //log.LogDebug(Newtonsoft.Json.JsonConvert.SerializeObject(data));
                return Succcess("成功.", data);
            }
            catch (Exception ex)
            {
                log.LogError("[分页获取第三方消息发送数据]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 修改系统推送消息数据
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> UpdateSystemNotice(AddOrUpdateNoticeInp form)
        {
            try
            {
                var res = await systemService.AddOrUpdateNoticeAsync(form);
                if (res.BoolResult)
                {
                    return Succcess(res.Message);
                }
                return Fail(res.Message);
            }
            catch (Exception ex)
            {
                log.LogError("[修改系统配置]" + ex.Message, ex);
                return Fail("修改系统配置失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 新增系统推送消息数据
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> AddSystemNotice(AddOrUpdateNoticeInp form)
        {
            try
            {
                var res = await systemService.AddOrUpdateNoticeAsync(form);
                if (res.BoolResult)
                {
                    return Succcess(res.Message);
                }
                return Fail(res.Message);
            }
            catch (Exception ex)
            {
                log.LogError("[修改系统配置]" + ex.Message, ex);
                return Fail("修改系统配置失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 根据ID获取系统推送消息数据
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetSystemNoticeById(long PKID)
        {
            try
            {
                var data = await systemService.GetNoticeByIDAsync(PKID);
                //log.LogDebug(Newtonsoft.Json.JsonConvert.SerializeObject(data));
                return Succcess("成功.", data);
            }
            catch (Exception ex)
            {
                log.LogError("[分页获取第三方消息发送数据]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }
        }

        /// <summary>
        /// 后台客户端发送全局消息
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> ClientNotice(long PKID)
        {
            try
            {
                var res = await systemService.SendNoticeAsync(PKID);
                if (!res.BoolResult)
                {
                    return Fail(res.Message);
                }
                //log.LogDebug(Newtonsoft.Json.JsonConvert.SerializeObject(data));

                SendMsg("message");
                return Succcess(res.Message);
            }
            catch (Exception ex)
            {
                log.LogError("[分页获取第三方消息发送数据]" + ex.Message, ex);
                return Fail("获取失败,请刷新重试.");
            }
        }

        /// <summary>
        /// websocket发送
        /// </summary>
        /// <param name="sendMessage"></param>
        private async void SendMsg(object sendMessage)
        {
            try
            {
                var domain = System.Configuration.ConfigurationManager.AppSettings["websocketDomain"];
                await webSocket.ConnectAsync(new Uri(domain), _cancellation);
                var sendBytes = TypeTools.ObjectToBytes(sendMessage);//发送的数据
                var bsend = new ArraySegment<byte>(sendBytes);
                await webSocket.SendAsync(bsend, WebSocketMessageType.Binary, true, _cancellation);
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "1", _cancellation);
                webSocket.Dispose();//记得一定要释放不然服务端还产生很多连接
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}
