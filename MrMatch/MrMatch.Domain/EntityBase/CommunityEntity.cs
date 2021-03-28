using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Domain.EntityBase
{


    public abstract class CommunityEntity : CommunityEntity<long>
    { 
    }

    public abstract class CommunityEntity<TPrimaryKey> : ICommunityEntity<TPrimaryKey>
    {
        public CommunityEntity() { }

        public CommunityEntity(TPrimaryKey id) { }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TPrimaryKey id { get; set; }
    }
}
