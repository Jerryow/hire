using MrMatch.Application.System;
using MrMatch.Application.System.Oup;
using MrMatch.Common.Encrypt;
using MrMatch.Common.Redis;
using MrMatch.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MrMatch.WxApi.Api.Base
{
    [ApiFilters]
    [BasicAuthorize]
    public class SecurityBaseController : ApiControllerBase
    {
        
    }
}