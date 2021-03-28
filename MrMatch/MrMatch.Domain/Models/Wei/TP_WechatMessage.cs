namespace MrMatch.Domain.Models
{
    using MrMatch.Domain.EntityBase;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class TP_WechatMessage : Entity
    {
        

        [StringLength(50)]
        public string EventType { get; set; }

        [StringLength(50)]
        public string MessageType { get; set; }

        [StringLength(50)]
        public string MessageName { get; set; }

        [StringLength(50)]
        public string MessageCode { get; set; }

        [StringLength(500)]
        public string MessageLink { get; set; }

        [StringLength(2000)]
        public string ImgUrl { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool? OnActive { get; set; }
    }
}
