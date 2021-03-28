namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class TP_AccessToken : Entity
    {


        [StringLength(128)]
        public string AppID { get; set; }

        [StringLength(256)]
        public string Secret { get; set; }

        [StringLength(500)]
        public string AccessToken { get; set; }

        public int? ExpiresIn { get; set; }

        public DateTime? ExpiresTime { get; set; }
    }
}
