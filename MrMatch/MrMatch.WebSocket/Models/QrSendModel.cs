using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MrMatch.WebSocket.Models
{
    public class QrSendModel
    {
        public string UserID { get; set; }
        public string TypeCode { get; set; }

        public string Message { get; set; }
    }
}