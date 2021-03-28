using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MrMatch.WebSocket.Models
{
    public class SendMessage
    {
        public int PKID { get; set; }

        public int Code { get; set; }
        public string Message { get; set; }
    }
}