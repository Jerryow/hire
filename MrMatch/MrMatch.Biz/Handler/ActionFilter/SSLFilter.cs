using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO.Compression;
using System.IO;

namespace MrMatch.Biz.Handler.ActionFilter
{
    public class SSLFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsSecureConnection)
            {
                var url = filterContext.HttpContext.Request.Url.ToString().Replace("http:", "https:");
                filterContext.Result = new RedirectResult(url);
            }
        }

        //压缩
        //public override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    var request = filterContext.HttpContext.Request;
        //    var response = filterContext.HttpContext.Response;
        //    string accept = request.Headers["Accept-Encoding"];
        //    if (!string.IsNullOrWhiteSpace(accept) && accept.ToUpper().Equals("GZIP"))
        //    {
        //        response.AddHeader("Content-Encoding", "gzip");
        //        response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
        //    }
        //}

        //防盗链
        //public override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    string url = filterContext.HttpContext.Request.Path;
        //    if (url.Contains(".jpg"))
        //    {
        //        string urlReferrer = context.Response.Headers["Referer"];//http头的信息,包含当前请求的页面域名
        //        if (string.IsNullOrWhiteSpace(urlReferrer))//直接访问
        //        {
        //            await this.SetForBiddenImg(context);//返回404图片
        //        }
        //        else if (!urlReferrer.Contains("localhost"))//非当前域名
        //        {
        //            await this.SetForBiddenImg(context);//返回404图片
        //        }
        //        else
        //        {
        //            await next(context);
        //        }
        //    }
        //}
        //
        //public async void SetForBiddenImg(ActionExecutingContext context)
        //{
        //    string defaultImgPath = "wwwroot/img/forbidden.jpg";
        //    string path = Path.Combine(Directory.GetCurrentDirectory(), defaultImgPath);
        //
        //    FileStream fs = File.OpenRead(path);
        //    byte[] bs = new byte[fs.Length];
        //    await fs.ReadAsync(bs, 0, bs.Length);
        //    await context.HttpContext.Response.Write(bs, 0, bs.Length);
        //}
    }
}