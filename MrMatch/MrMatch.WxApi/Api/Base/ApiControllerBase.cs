using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MrMatch.WxApi.Api.Base
{
    public class ApiControllerBase : ApiController
    {

        #region 成功
        /// <summary>
        /// 成功消息
        /// </summary>
        /// <param name="msg">消息</param>
        /// <returns></returns>
        protected IHttpActionResult Succcess(string msg)
        {
            return Json<object>(new { code = "1", msg = msg });
        }
        /// <summary>
        /// 成功消息+数据
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        protected IHttpActionResult Succcess(string msg, object data)
        {
            return Json<object>(new { code = "1", msg = msg, data = data });
        }
       
        /// <summary>
        /// 成功消息+数据
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        protected IHttpActionResult SucccessNull(string msg, object data)
        {
            return Json<object>(new { code = "2", msg = msg, data = data });
        }
        #endregion
        #region 失败
        /// <summary>
        /// 失败消息Result
        /// </summary>
        /// <param name="msg">内容</param>
        /// <returns></returns>
        protected IHttpActionResult Fail(string msg)
        {
            return Json<object>(new { code = "0", msg = msg });
        }


        /// <summary>
        /// 失败消息+数据
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        protected IHttpActionResult Fail(string msg, object data)
        {
            return Json<object>(new { code = "0", msg = msg, data = data });
        }
        #endregion

        #region Serialize

        protected static string Serialize<T>(T data)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(data);
        }

        protected static T Deserialize<T>(string data)
        {
            if (String.IsNullOrEmpty(data)) return default(T);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(data);
        }

        protected static IList<T> DeserializeRows<T>(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<IList<T>>(data);
        }
        #endregion
    }
}