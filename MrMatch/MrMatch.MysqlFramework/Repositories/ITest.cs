using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MrMatch.Domain.EntityBase.Repository;
using MrMatch.Domain.Models;

namespace MrMatch.MysqlFramework.Repositories
{
    //*********此类测试专用************
    public interface ITest: IRepositoryTest<TP_SystemUser>
    {
        string GetName();
    }
}
