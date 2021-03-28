using MrMatch.Domain.EntityBase.Repository;
using MrMatch.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.Cache
{
    public class InitCacheHelper
    {
        private readonly IRepository<TP_Country> country;
        private readonly IRepository<TP_District> district;
        private readonly IRepository<TP_Function> function;
        private readonly IRepository<TP_Skills> skills;
        private readonly IRepository<TP_Tags> tags;
        private readonly IRepository<TP_FunctionSkillsRelation> relation;
        public InitCacheHelper(
            IRepository<TP_Country> _country,
            IRepository<TP_District> _district,
            IRepository<TP_Function> _function,
            IRepository<TP_Skills> _skills,
            IRepository<TP_Tags> _tags,
            IRepository<TP_FunctionSkillsRelation> _relation)
        {
            country = _country;
            district = _district;
            function = _function;
            skills = _skills;
            tags = _tags;
            relation = _relation;
        }

        
    }
}
