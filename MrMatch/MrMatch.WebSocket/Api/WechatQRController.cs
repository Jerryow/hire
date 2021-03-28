using MrMatch.WebSocket.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.WebSockets;

namespace MrMatch.WebSocket.Api
{
    public class WechatQRController : ApiController
    {
        //链接用户池
        private static Dictionary<string, System.Net.WebSockets.WebSocket> _sockets = new Dictionary<string, System.Net.WebSockets.WebSocket>();

        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetQRConnection()
        {
            HttpContext.Current.AcceptWebSocketRequest(PushData);

            return Request.CreateResponse(HttpStatusCode.SwitchingProtocols);
        }

        public async Task PushData(AspNetWebSocketContext context)
        {
            try
            {
                
                var socket = context.WebSocket;
                var userid = Guid.NewGuid().ToString();
                if (!_sockets.ContainsKey(userid))
                {
                    //用户连接池没有则添加
                    _sockets.Add(userid, socket);
                }
                else
                {
                    //如果当前socket对象不一致, 更新并保持一致
                    if (socket != _sockets[userid])
                    {
                        _sockets[userid] = socket;
                    }
                }
                while (true)
                {
                    var buffer = new ArraySegment<byte>(new byte[1024]);
                    var receivedResult = await socket.ReceiveAsync(buffer, CancellationToken.None);//对web socket进行异步接收数据
                    if (receivedResult.MessageType == WebSocketMessageType.Close)
                    {
                        await socket.CloseAsync(WebSocketCloseStatus.Empty, string.Empty, CancellationToken.None);//如果client发起close请求，对client进行ack
                        _sockets.Remove(userid);
                        break;
                    }
                    string recvMsg = Encoding.UTF8.GetString(buffer.Array, 0, receivedResult.Count);
                    var recvObj = Newtonsoft.Json.JsonConvert.DeserializeObject<QrRecieveModel>(recvMsg);
                    var sendData = new QrSendModel();
                    sendData.TypeCode = recvObj.TypeCode;
                    switch (recvObj.TypeCode)
                    {
                        case "1000":
                            //首次加载创建二维码
                            sendData.Message = userid;
                            buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(sendData)));
                            await _sockets[userid].SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
                            break;
                        case "1001":
                            //扫码成功
                            var webUser = recvObj.Message;
                            if (!_sockets.ContainsKey(webUser))
                            {
                                sendData.TypeCode = "2000";
                                sendData.Message = "二维码过期";
                                buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(sendData)));
                                await _sockets[userid].SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
                            }
                            else
                            {
                                long uid = 0;
                                long.TryParse(recvObj.UserID, out uid);
                                if (uid <= 0)
                                {
                                    sendData = new QrSendModel();
                                    sendData.TypeCode = "2000";
                                    sendData.Message = "扫码失败,请重试";
                                    sendData.UserID = recvObj.UserID;
                                    buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(sendData)));
                                    await _sockets[webUser].SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
                                }
                                else
                                {
                                    sendData = new QrSendModel();
                                    sendData.TypeCode = "1002";
                                    sendData.Message = "扫码成功";
                                    sendData.UserID = recvObj.UserID;
                                    buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(sendData)));
                                    await _sockets[webUser].SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);

                                    sendData = new QrSendModel();
                                    sendData.TypeCode = "1002";
                                    sendData.Message = "登陆成功";
                                    sendData.UserID = recvObj.UserID;
                                    buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(sendData)));
                                    await _sockets[userid].SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
                                }
                            }
                            break;
                        default:
                            //扫码登陆
                            break;
                    }

                    //buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(sendData)));

                    //if (_sockets.ContainsKey(descUser))//判断客户端是否在线
                    //{
                    //    System.Net.WebSockets.WebSocket destSocket = _sockets[descUser];//目的客户端
                    //    if (destSocket != null && destSocket.State == WebSocketState.Open)
                    //        await destSocket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);

                    //    //给发送用户返回成功信息
                    //    if (recvObj.TypeCode == "1001")
                    //    {
                    //        //通知小程序
                    //        destSocket = _sockets[userid];
                    //        sendData.TypeCode = "2001";
                    //        sendData.Message = "扫码成功。";
                    //        sendData.UserID = recvObj.UserID;
                    //        buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(sendData)));
                    //        if (destSocket != null && destSocket.State == WebSocketState.Open)
                    //            await destSocket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
                    //        //通知PC简历
                    //        destSocket = _sockets[Common.Encrypt.Encryption.DecryptString(recvObj.Message)];
                    //        sendData.TypeCode = "1002";
                    //        sendData.UserID = recvObj.UserID;
                    //        sendData.Message = "登陆成功";
                    //        if (destSocket != null && destSocket.State == WebSocketState.Open)
                    //            await destSocket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);

                    //    }


                    //    if (recvObj.TypeCode == "1002")
                    //    {
                    //        destSocket = _sockets[userid];//目的客户端
                    //        sendData.TypeCode = "2002";
                    //        sendData.Message = "登录成功。";
                    //        buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(sendData)));
                    //        if (destSocket != null && destSocket.State == WebSocketState.Open)
                    //            await destSocket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
                    //    }

                    //}
                    //else
                    //{
                    //    //整体异常处理
                    //    //if (CONNECT_POOL.ContainsKey(descUser)) CONNECT_POOL.Remove(descUser);

                    //    // 目标客户离线，给发起人回复
                    //    if (recvObj.TypeCode == "1001" || recvObj.TypeCode == "1002")
                    //    {
                    //        sendData.TypeCode = "2000";
                    //        sendData.Message = "二维码过期。";
                    //        buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(sendData)));
                    //        System.Net.WebSockets.WebSocket destSocket = _sockets[userid];//目的客户端
                    //        if (destSocket != null && destSocket.State == WebSocketState.Open)
                    //        {
                    //            await destSocket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
                    //        }
                    //    }
                    //    else
                    //    {
                    //        if (_sockets.ContainsKey(descUser)) _sockets.Remove(descUser);
                    //    }

                    //}
                }
            }
            catch (Exception ex)
            {

                //Helper.LogHelper.LogError(ex.Message, ex);
            }
        }
    }
}
