using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Domain.EntityBase
{


    public abstract class Entity : Entity<long>
    { 
    }

    public abstract class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        public Entity() { }

        public Entity(TPrimaryKey id) { }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TPrimaryKey PKID { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LastModifiedTime { get; set; }
        /// <summary>
        /// 是否有效(逻辑删除统一用这个)
        /// </summary>
        public bool Valid { get; set; }
    }
}
