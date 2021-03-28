using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.Auth
{
    public interface IAuthService
    {
        Task<BoolMessageOup> JobActivityVerifyAsync(bool status, long CompanyID);
    }
}
