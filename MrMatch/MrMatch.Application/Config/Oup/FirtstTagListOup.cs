using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Application.Config.Oup
{
    public class FirtstTagListOup
    {
        public int PKID { get; set; }

        public string Tags { get; set; }

        public List<TagListOup> ChildrenTag { get; set; }
    }
}
