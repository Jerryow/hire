using MrMatch.Domain.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.LoginOrRegist.Inp
{
    public class QRLoginInp : EntityDto
    {
        public string ReturnUrl { get; set; }
    }
}
