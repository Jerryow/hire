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
    public class BizClientController : ApiController
    {
        //链接用户池
        private static Dictionary<string, System.Net.WebSockets.WebSocket> _sockets = new Dictionary<string, System.Net.WebSockets.WebSocket>();
        //离线消息池
        //private static Dictionary<long, >

        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetConnection(string clientName)
        {
            HttpContext.Current.AcceptWebSocketRequest(PushData);

            return Request.CreateResponse(HttpStatusCode.SwitchingProtocols);
        }

        public async Task PushData(AspNetWebSocketContext context)
        {
            try
            {

                var socket = context.WebSocket;
                var userid = context.QueryString["clientName"];
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
                    var recvBytes = Encoding.UTF8.GetBytes(recvMsg);

                    if (userid == "Admin")
                    {
                        //抓取系统消息发送

                        if (socket.State == System.Net.WebSockets.WebSocketState.Open)
                        {
                            MrMatch.WebSocket.Models.SendMessage sendMessage = new Models.SendMessage();
                            sendMessage.PKID = 1;
                            sendMessage.Code = MrMatch.Application.CommonEnum.WebsocketEnum.群发消息.GetHashCode();
                            sendMessage.Message = MrMatch.Application.CommonEnum.WebsocketEnum.群发消息.ToString();
                            var sendBuffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(sendMessage)));

                            foreach (var innerSocket in _sockets)//当接收到文本消息时，对当前服务器上所有web socket连接进行广播
                            {
                                if (innerSocket.Value != socket)
                                {
                                    await innerSocket.Value.SendAsync(sendBuffer, WebSocketMessageType.Text, true, CancellationToken.None);
                                }
                            }

                        }

                    }

                    //已读回写
                    if (userid == "Send")
                    {
                        var fromUser = context.QueryString["fromUser"];
                        var fromSocket = _sockets[fromUser];

                        if (socket.State == System.Net.WebSockets.WebSocketState.Open)
                        {
                            MrMatch.WebSocket.Models.SendMessage sendMessage = new Models.SendMessage();
                            sendMessage.PKID = 1;
                            sendMessage.Code = MrMatch.Application.CommonEnum.WebsocketEnum.指定用户消息回写.GetHashCode();
                            sendMessage.Message = MrMatch.Application.CommonEnum.WebsocketEnum.指定用户消息回写.ToString();
                            var sendBuffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(sendMessage)));

                            //通知head刷新count
                            await fromSocket.SendAsync(sendBuffer, WebSocketMessageType.Text, true, CancellationToken.None);

                            //通知消息列表回写成功 并关闭连接
                            MrMatch.WebSocket.Models.SendMessage sendFeedback = new Models.SendMessage();
                            sendFeedback.PKID = 1;
                            sendFeedback.Code = MrMatch.Application.CommonEnum.WebsocketEnum.回写成功.GetHashCode();
                            sendFeedback.Message = MrMatch.Application.CommonEnum.WebsocketEnum.回写成功.ToString();
                            var feedbackBuffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(sendFeedback)));
                            await socket.SendAsync(feedbackBuffer, WebSocketMessageType.Text, true, CancellationToken.None);
                        }

                        if (receivedResult.MessageType == WebSocketMessageType.Close)
                        {
                            await socket.CloseAsync(WebSocketCloseStatus.Empty, string.Empty, CancellationToken.None);//如果client发起close请求，对client进行ack
                            _sockets.Remove(userid);
                            break;
                        }
                    }

                    //心跳检测
                    if (recvMsg == "HeartCheck")
                    {
                        if (socket.State == System.Net.WebSockets.WebSocketState.Open)
                        {
                            MrMatch.WebSocket.Models.SendMessage sendMessage = new Models.SendMessage();
                            sendMessage.PKID = 1;
                            sendMessage.Code = MrMatch.Application.CommonEnum.WebsocketEnum.心跳检测.GetHashCode();
                            sendMessage.Message = MrMatch.Application.CommonEnum.WebsocketEnum.心跳检测.ToString();
                            var sendBuffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(sendMessage)));

                            //通知head刷新count
                            await socket.SendAsync(sendBuffer, WebSocketMessageType.Text, true, CancellationToken.None);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                //Helper.LogHelper.LogError(ex.Message, ex);
            }
        }
    }
}
