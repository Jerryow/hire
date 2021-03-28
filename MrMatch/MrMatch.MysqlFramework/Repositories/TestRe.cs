using MrMatch.MysqlFramework.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MrMatch.Domain.Models;
using MrMatch.MysqlFramework.BaseContext;

namespace MrMatch.MysqlFramework.Repositories
{
    //*********此类测试专用************
    public class TestRe : EFRepositoriesBaseTest<TP_SystemUser>, ITest
    {
        public TestRe(IDbContext dbContext) : base(dbContext) { }
        public string GetName()
        {
            return query.FirstOrDefault().LoginName;
        }
    }
}
