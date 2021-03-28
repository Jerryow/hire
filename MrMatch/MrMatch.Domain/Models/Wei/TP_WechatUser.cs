namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class TP_WechatUser : Entity
    {
        

        [StringLength(128)]
        public string AppID { get; set; }

        [StringLength(128)]
        public string UnionID { get; set; }

        [StringLength(128)]
        public string OpenID { get; set; }

        [StringLength(50)]
        public string NickName { get; set; }

        [StringLength(500)]
        public string AvatarUrl { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(100)]
        public string Province { get; set; }

        [StringLength(100)]
        public string Country { get; set; }

        [StringLength(100)]
        public string Language { get; set; }

        [StringLength(3000)]
        public string Remark { get; set; }
    }
}
