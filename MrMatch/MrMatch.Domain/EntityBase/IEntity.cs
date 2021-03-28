using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Domain.EntityBase
{


    public interface IEntity<TPrimaryKey>
    {
        TPrimaryKey PKID { get; set; }
    }
}
